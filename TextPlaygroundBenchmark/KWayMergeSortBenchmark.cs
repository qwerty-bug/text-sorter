﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using TextSorter.ExternalSort;

namespace TextPlaygroundBenchmark
{
    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net80)]
    public class KWayMergeSortBenchmark
    {
        private readonly List<SubArrayProperties> UnsortedRows;

        public KWayMergeSortBenchmark()
        {
            UnsortedRows = Enumerable.Range(1,10)
                .Select(i => new SubArrayProperties("SampleData100.txt", i))
                .ToList();
        }

        [Benchmark(Baseline = true)]
        public List<SubArrayProperties> Sort()
        {
            var sortedLines = KWayMergeSort.SortLines(UnsortedRows);
            return sortedLines;
        }
    }
}
