using Common;
using System.Text;
using TextSorter.ExternalSort;

namespace TextSorter
{
    public class Sorter
    {
        public async Task Process()
        {
            Logger.Log($"Start processing file.");
            Logger.Log("-----------------------------------");

            //var repo = new FileRepository();
            var tempFiles = Worker.SplitIntoChunks(DataConfig.SampleDataFile);
            Logger.Log("-----------------------------------");
            Logger.Log($"{DataConfig.SampleDataFile} file splitted into {tempFiles.Count()} sorted tempFiles.");

            Logger.Log("All temporary sorted data saved.");

            //var sortedTempFiles = tasks.Select(x => x.Result).ToArray();
            var extSort = new KWayMergeSort(tempFiles);
            await extSort.Sort();

            CleanUp(tempFiles);

            Logger.Log($"Data sorted successfully.");
        }

        private void CleanUp(List<string> tempFiles)
        {
            foreach (var file in tempFiles)
                File.Delete(file);
        }
    }
}
