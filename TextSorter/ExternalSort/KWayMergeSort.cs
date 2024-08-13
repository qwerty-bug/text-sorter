using Common;

namespace TextSorter.ExternalSort
{
    public class KWayMergeSort
    {
        private const int LogThreshold = 5000000; // 5M
        public const int InitialWorkId = 0;

        public List<SubArrayProperties> Initialize(IReadOnlyList<string> sortedFiles)
        {
            var subArrays = new List<SubArrayProperties>(sortedFiles.Count);
            for (int i = 0; i < sortedFiles.Count; i++)
            {
                subArrays.Add(new SubArrayProperties(sortedFiles[i], i));
            }

            return subArrays;
        }

        public string Process(int id, List<string> sortedFiles)
        {
            var files = new List<string>(sortedFiles);
            int i = id;
            while(files.Count > Options.ExternalSortConcurrentLimit)
            {
                var toProcessGroups = new List<List<string>>();
                var groupsCount = files.Count / Options.ExternalSortConcurrentLimit +
                                    (files.Count % Options.ExternalSortConcurrentLimit == 0 ? 0 : 1);
                for (int g = 0; g < groupsCount; g++)
                {
                    toProcessGroups.Add(new List<string>(
                        files
                            .Skip(g* Options.ExternalSortConcurrentLimit)
                            .Take(Options.ExternalSortConcurrentLimit).ToList()));
                }

                //var toProcess = files.Take(Options.ExternalSortConcurrentLimit).ToList();
                var jobs = new List<Task<string>>();
                foreach (var group in toProcessGroups)
                {
                    i++;
                    var jobId = i;
                    jobs.Add(
                        Task.Run(() => Process(jobId * 10, group)));

                    if(jobs.Where(x => !x.IsCompleted).Count() >= Options.ExternalSortAsyncJobsLimit)
                    {
                        Task.WaitAll(jobs.ToArray());
                    }
                }

                Task.WaitAll(jobs.ToArray());
                foreach (var job in jobs)
                {
                    files.Add(job.Result);
                }
                files.RemoveAll(x => toProcessGroups.SelectMany(x => x).Contains(x));

                //i++;
            }

            var subarrays = Initialize(files);

            var outputFile = $"{DateTime.Now.GetHashCode()}{Options.SortedOutputDataFile}";
            if(id == InitialWorkId)
                outputFile = Options.SortedOutputDataFile;

            var output = File.OpenWrite(outputFile);
            using var outputWriter = new StreamWriter(output, bufferSize: Options.BufferSize64MB);
            FileCleaner.Add(outputFile);

            Logger.Log($"Id: [{id}], Start external sorting with {subarrays.Count} parts.");
            int counter = 1;
            var tempTimer = GlobalTimer.StopWatch.Elapsed.TotalSeconds;
            var timings = new List<double>();
            while (true)
            {
                if (subarrays.Count == 0)
                {
                    Logger.Log($"Id: [{id}], --");
                    Logger.Log($"Id: [{i}], Sorting completed");
                    break;
                }

                SortLines(subarrays);

                var minArray = subarrays.First();
                outputWriter.WriteLine(minArray.CurrentValue);
                if (counter % LogThreshold == 0)
                {
                    var time = GlobalTimer.StopWatch.Elapsed.TotalSeconds - tempTimer;
                    timings.Add(time);
                    Logger.Log($"Id: [{id}], Processed records: {counter:n0} ({time:0.000}s per 5,000,000)");
                    tempTimer = GlobalTimer.StopWatch.Elapsed.TotalSeconds;
                }

                var newVal = minArray.ReadNextLine();
                if (newVal is null)
                {
                    subarrays.Remove(minArray);
                    minArray.Dispose();
                    Logger.Log($"Id: [{id}], Subarray {minArray.ReaderId} emptied.");
                    continue;
                }

                //counter++;
            }

            var avg = timings.Any() ? timings.Average() : 0;
            Logger.Log($"Id: [{id}], Average time group id: {id} per 5,000,000: {avg:0.000}s");
            Logger.Log($"Id: [{id}], Processed in {GlobalTimer.StopWatch.Elapsed.TotalSeconds - tempTimer}s");
            var outputText = id == -1 ? "Output" : "Temp output";
            Logger.Log($"Id: [{id}], {outputText} saved to: {outputFile} file.");
            Logger.Log($"Id: [{id}], ---------------------");

            return outputFile;
        }

        public static List<SubArrayProperties> SortLines(List<SubArrayProperties> currentLines)
        {
            currentLines
                .Sort((line1, line2) =>
                {
                    return Worker.Sort2Lines(line1.CurrentValue, line2.CurrentValue);
                });

            return currentLines;
        }
    }
}
