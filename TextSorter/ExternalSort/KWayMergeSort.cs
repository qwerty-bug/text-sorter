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

        public async Task<List<LineDetails>> InitializeReaders()
        {
            var readers = new StreamReader[sortedFiles.Count];
            var lines = new List<LineDetails>(sortedFiles.Count);
            for (int i = 0; i < sortedFiles.Count; i++)
            {
                var sortedFileStream = File.OpenRead(sortedFiles[i]);
                readers[i] = new StreamReader(sortedFileStream, Encoding.UTF8, bufferSize: DataConfig.BufferSize128KB);
                lines.Add(new LineDetails
                {
                    Reader = readers[i],
                    ReaderId = i,
                    Value = await readers[i].ReadLineAsync()
                });
            }

            return lines;
        }

        public async Task Sort()
        {
            var currentLines = await InitializeReaders();

            var output = File.OpenWrite(DataConfig.SortedDataFile);
            await using var outputWriter = new StreamWriter(output, bufferSize: DataConfig.BufferSize128KB);

            while (true)
            {
                if (currentLines.Count == 0)
                {
                    Logger.Log($"Completed sorting");
                    break;
                }

                currentLines
                    .Sort((line1, line2) =>
                        {
                            var first = line1.Value.Split('.');
                            var firstNumber = first[0];
                            var firstText = first[1];

                            var second = line2.Value.Split('.');
                            var secondNumber = second[0];
                            var secondText = second[1];

                            var result = string.Compare(firstText, secondText, StringComparison.Ordinal);
                            if (result != 0)
                            {
                                return result;
                            }

                            return int.Parse(firstNumber) > int.Parse(secondNumber) ? 1 : -1;
                        });

                var minLine = currentLines.First();
                await outputWriter.WriteLineAsync(minLine.Value);

                var newVal = await minLine.Reader.ReadLineAsync();
                if(newVal is null)
                {
                    currentLines.Remove(minLine);
                    minLine.Reader.Dispose();
                    continue;
                }

                minLine.Value = newVal;
            }

            Logger.Log($"Output saved to: {DataConfig.SortedDataFile} file.");
        }
    }
}
