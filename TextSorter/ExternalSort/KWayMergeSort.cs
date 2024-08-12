using Common;

namespace TextSorter.ExternalSort
{
    public class KWayMergeSort
    {
        private const int LogThreshold = 5000000; // 5M

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
            int i = 0;
            while(files.Count > 10)
            {
                var toProcess = files.Take(10).ToList();
                files.Add(Process(i, toProcess));
                files.RemoveAll(x => toProcess.Contains(x));
                i++;
            }

            var subarrays = Initialize(files);

            var outputFile = $"{Math.Abs(DateTime.Now.GetHashCode())}{Common.FileOptions.SortedOutputDataFile}";
            if(id == -1)
                outputFile = Common.FileOptions.SortedOutputDataFile;

            var output = File.OpenWrite(outputFile);
            using var outputWriter = new StreamWriter(output, bufferSize: Common.FileOptions.BufferSize32MB);
            FileCleaner.Add(outputFile);

            Logger.Log($"Start external sorting with {subarrays.Count} parts.");
            int counter = 1;
            var tempTimer = GlobalTimer.StopWatch.Elapsed.TotalSeconds;
            var timings = new List<double>();
            while (true)
            {
                if (subarrays.Count == 0)
                {
                    Logger.Log("---------------------");
                    Logger.Log($"Sorting completed");
                    break;
                }

                SortLines(subarrays);

                var minArray = subarrays.First();
                outputWriter.WriteLine(minArray.CurrentValue);
                if (counter % LogThreshold == 0)
                {
                    var time = GlobalTimer.StopWatch.Elapsed.TotalSeconds - tempTimer;
                    timings.Add(time);
                    Logger.Log($"Processed records: {counter:n0} ({time:0.000}s per 5,000,000)");
                    tempTimer = GlobalTimer.StopWatch.Elapsed.TotalSeconds;
                }

                var newVal = minArray.ReadNextLine();
                if (newVal is null)
                {
                    subarrays.Remove(minArray);
                    minArray.Dispose();
                    Logger.Log($"Subarray {minArray.ReaderId} emptied.");
                    continue;
                }

                counter++;
            }

            Logger.Log($"Average time per 5,000,000: {timings.Average():0.000}s");
            Logger.Log($"Output saved to: {outputFile} file.");

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
