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
        private readonly string FirstRow;
        private readonly string SecondRow;


        public WorkerBenchmark()
        {
            UnsortedRows = DataHelper.LoadUnsortedRows();
            FirstRow = UnsortedRows.First();
            SecondRow = UnsortedRows.Skip(1).First();
        }

        //[Benchmark(Baseline = true)]
        //public List<string> Sort()
        //{
        //    var text = Worker.SortText(UnsortedRows, TestChunkId);
        //    return text;
        //}


        [Benchmark(Baseline = true)]
        public int Sort()
        {
            var result = Worker.Sort2Lines(FirstRow, SecondRow);
            return result;
        }

        //[Benchmark]
        //public int Sort2()
        //{
        //    var result = Worker.Sort2Lines2(FirstRow, SecondRow);
        //    return result;
        //}
    }
}
