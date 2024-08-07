using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using TextSorter.ExternalSort;

namespace TextPlaygroundBenchmark
{
    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net80)]
    public class KWayMergeSortBenchmark
    {
        private const int TestReaderId = 199;
        private readonly List<SubArrayProperties> UnsortedRows;

        public KWayMergeSortBenchmark()
        {
            UnsortedRows = DataHelper
                .LoadUnsortedRows()
                .Take(100)
                .Select(x => new SubArrayProperties("SampleData100.txt", TestReaderId))
                .ToList();
        }

        [Benchmark(Baseline = true)]
        public List<SubArrayProperties> Sort()
        {
            var sortedLines = KWayMergeSort.SortLines(UnsortedRows);
            return sortedLines;
        }

        [Benchmark]
        public List<SubArrayProperties> Sort3()
        {
            var sortedLines = KWayMergeSort.SortLines3(UnsortedRows);
            return sortedLines;
        }
    }
}
