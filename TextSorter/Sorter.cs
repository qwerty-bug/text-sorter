using Common;
using TextSorter.ExternalSort;

namespace TextSorter
{
    public class Sorter
    {
        public void Process()
        {
            FileCleaner.CleanOutputFile();

            Logger.Log($"Start processing file..");
            Logger.Log("-----------------------------------");

            var tempFiles = Worker.SplitIntoChunks(Common.FileOptions.SampleDataFile);

            var extSort = new KWayMergeSort();
            extSort.Process(-1, tempFiles);

            FileCleaner.CleanFiles();

            Logger.Log($"Data sorted successfully.");
        }
    }
}
