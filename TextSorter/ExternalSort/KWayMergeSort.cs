using Common;
using System.Text;

namespace TextSorter.ExternalSort
{
    public class KWayMergeSort
    {
        private readonly IReadOnlyList<string> sortedFiles;

        public KWayMergeSort(IReadOnlyList<string> sortedFiles)
        {
            this.sortedFiles = sortedFiles;
        }

        public List<SubArrayProperties> Initialize()
        {
            var subArrays = new List<SubArrayProperties>(sortedFiles.Count);
            for (int i = 0; i < sortedFiles.Count; i++)
            {
                subArrays.Add(new SubArrayProperties(sortedFiles[i], i));
            }

            return subArrays;
        }

        public async Task Process()
        {
            var subarrays = Initialize();

            var output = File.OpenWrite(Common.FileOptions.SortedDataFile);
            using var outputWriter = new StreamWriter(output, bufferSize: Common.FileOptions.BufferSize32MB);

            Logger.Log($"Start final sorting with {subarrays.Count} parts.");
            int counter = 0;
            while (true)
            {
                if (subarrays.Count == 0)
                {
                    Logger.Log("---------------------");
                    Logger.Log($"Completed sorting");
                    break;
                }

                SortLines3(subarrays);

                var minArray = subarrays.First();
                await outputWriter.WriteLineAsync(minArray.CurrentValue);
                if (counter % 500000 == 0)
                    Logger.Log($"Processed records: {counter:n0}");

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

            Logger.Log($"Output saved to: {Common.FileOptions.SortedDataFile} file.");
        }

        [Obsolete("Slower than SortText3")]
        public static List<SubArrayProperties> SortLines(List<SubArrayProperties> currentLines)
        {
            currentLines
                .Sort((line1, line2) =>
                {
                    var first = line1.CurrentValue.Split('.');
                    var firstNumber = first[0];
                    var firstText = first[1];

                    var second = line2.CurrentValue.Split('.');
                    var secondNumber = second[0];
                    var secondText = second[1];

                    var result = string.Compare(firstText, secondText, StringComparison.Ordinal);
                    if (result != 0)
                    {
                        return result;
                    }

                    return int.Parse(firstNumber) > int.Parse(secondNumber) ? 1 : -1;
                });

            return currentLines;
        }

        public static List<SubArrayProperties> SortLines3(List<SubArrayProperties> currentLines)
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
