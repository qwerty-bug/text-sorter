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

            var output = File.OpenWrite(SorterFileConfig.SortedDataFile);
            using var outputWriter = new StreamWriter(output, bufferSize: SorterFileConfig.BufferSize25MB);

            while (true)
            {
                if (subarrays.Count == 0)
                {
                    Logger.Log($"Completed sorting");
                    break;
                }

                SortLines3(subarrays);

                var minArray = subarrays.First();
                await outputWriter.WriteLineAsync(minArray.CurrentValue);

                var newVal = minArray.ReadNextLine();
                if (newVal is null)
                {
                    subarrays.Remove(minArray);
                    minArray.Dispose();
                    continue;
                }
            }

            Logger.Log($"Output saved to: {SorterFileConfig.SortedDataFile} file.");
        }

        [Obsolete("Slower than SortText3")]
        public static List<SubArrayProperties> SortLines(List<SubArrayProperties> currentLines)
        {
            currentLines
                .Sort((Comparison<SubArrayProperties>)((line1, line2) =>
                {
                    var first = line1.CurrentValue.Split('.');
                    var firstNumber = first[0];
                    var firstText = first[1];

                    var second = line2.CurrentValue.Split('.');
                    var secondNumber = second[0];
                    var secondText = second[1];

                    var result = string.Compare((string)firstText, (string)secondText, StringComparison.Ordinal);
                    if (result != 0)
                    {
                        return result;
                    }

                    return int.Parse((string)firstNumber) > int.Parse((string)secondNumber) ? 1 : -1;
                }));

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
