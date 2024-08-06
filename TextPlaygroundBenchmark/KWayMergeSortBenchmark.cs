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
        private readonly List<LineDetails> UnsortedRows;

        public KWayMergeSortBenchmark()
        {
            UnsortedRows = DataHelper
                .LoadUnsortedRows()
                .Take(100)
                .Select(x => new LineDetails
                {
                        Value = x,
                        Reader = StreamReader.Null,
                        ReaderId = TestReaderId
                })
                .ToList();
        }

        [Benchmark(Baseline = true)]
        public List<LineDetails> Sort()
        {
            var sortedLines = KWayMergeSort.SortLines(UnsortedRows);
            return sortedLines;
        }

        [Benchmark]
        public List<LineDetails> Sort3()
        {
            var sortedLines = KWayMergeSort.SortLines3(UnsortedRows);
            return sortedLines;
        }
    }
}
