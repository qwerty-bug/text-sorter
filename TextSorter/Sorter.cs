using Common;
using System.Text;
using TextSorter.ExternalSort;

namespace TextSorter
{
    public class Sorter
    {
        public void Process()
        {
            Logger.Log($"Start processing file.");
            Logger.Log("-----------------------------------");

            var tempFiles = Worker.SplitIntoChunks(Common.FileOptions.SampleDataFile);
            Logger.Log("-----------------------------------");
            Logger.Log($"{Common.FileOptions.SampleDataFile} file splitted into {tempFiles.Count()} sorted tempFiles.");

            var filesToDelete = new List<string>(tempFiles);
            var extSort = new KWayMergeSort();
            extSort.Process(-1, tempFiles);

            CleanUp(filesToDelete);

            Logger.Log($"Data sorted successfully.");
        }

        private void CleanUp(List<string> tempFiles)
        {
            foreach (var file in tempFiles)
                File.Delete(file);
        }
    }
}
