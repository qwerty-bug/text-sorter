using Common;

namespace TextSorter.ExternalSort
{
    public class KWayMergeSort
    {
        private readonly IReadOnlyList<string> sortedFiles;

        //public KWayMergeSort(IReadOnlyList<string> sortedFiles)
        //{
        //    this.sortedFiles = sortedFiles;
        //}

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
            //if(sortedFiles.Count > 10)
            while(files.Count > 10)
            {
                var toProcess = sortedFiles.Skip(i*10).Take(10).ToList();
                files.RemoveAll(x => toProcess.Contains(x));
                files.Add(Process(i, toProcess));
                i++;
            }

            var subarrays = Initialize(files);

            var outputFile = $"{id}{Common.FileOptions.SortedDataFile}";
            if(id == -1)
                outputFile = Common.FileOptions.SortedDataFile;

            var output = File.OpenWrite(outputFile);
            using var outputWriter = new StreamWriter(output, bufferSize: Common.FileOptions.BufferSize32MB);

            Logger.Log($"Start final sorting with {subarrays.Count} parts.");
            int counter = 1;
            var tempTimer = GlobalTimer.StopWatch.Elapsed.TotalSeconds;
            var timings = new List<double>();
            while (true)
            {
                if (subarrays.Count == 0)
                {
                    Logger.Log("---------------------");
                    Logger.Log($"Completed sorting");
                    break;
                }

                SortLines(subarrays);

                var minArray = subarrays.First();
                outputWriter.WriteLine(minArray.CurrentValue);
                if (counter % 1000000 == 0)
                {
                    var time = GlobalTimer.StopWatch.Elapsed.TotalSeconds - tempTimer;
                    timings.Add(time);
                    Logger.Log($"Processed records: {counter:n0} (1000000 per {time}s)");
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

            Logger.Log($"Average time per 1000000: {timings.Average()}");
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
