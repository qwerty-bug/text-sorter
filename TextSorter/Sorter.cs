﻿using Common;
using TextSorter.ExternalSort;

namespace TextSorter
{
    public class Sorter
    {
        public void Process()
        {
            Logger.Log($"Start processing file..");
            Logger.Log("-----------------------------------");

            var tempFiles = Worker.SplitIntoChunks(Options.SampleDataFile);

            var extSort = new KWayMergeSort();
            extSort.Process(KWayMergeSort.InitialWorkId, tempFiles);

            FileCleaner.CleanFiles();

            Logger.Log($"Data sorted successfully.");
        }
    }
}
