using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using TextSorter;

namespace TextPlaygroundBenchmark
{
    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net80)]
    public class WorkerBenchmark
    {
        private const int TestChunkId = 199;
        private readonly List<string> UnsortedRows;

        public WorkerBenchmark()
        {
            UnsortedRows = DataHelper.LoadUnsortedRows();
        }

        [Benchmark(Baseline = true)]
        public List<string> Sort()
        {
            var text = Worker.SortText(UnsortedRows, TestChunkId);
            return text;
        }

        [Benchmark]
        public List<string> Sort3()
        {
            var text = Worker.SortText3(UnsortedRows, TestChunkId);
            return text;
        }
    }
}
