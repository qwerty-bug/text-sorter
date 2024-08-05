using Common;
using System.Text;

namespace TextSorter
{
    public class Sorter
    {
        public void SortAndSaveData()
        {
            Console.WriteLine($"{GlobalTimer.StopWatch.Elapsed.TotalSeconds}s, Start reading file.");

            var repo = new FileRepository();
            var files = repo.SplitIntoChunks(DataConfig.SampleDataFile);
            Console.WriteLine($"{GlobalTimer.StopWatch.Elapsed.TotalSeconds}s, Main file splitted into {files.Count()} tempFiles.");

            var tasks = new List<Task>();
            foreach (var file in files)
            {
                tasks.Add(
                    Task.Run(
                        () => Worker.Sort(file)
                    )
                    .ContinueWith(async sortedTask =>
                    {
                        await repo.Save(sortedTask.Result, $"sorted{file}");
                    }));
            }

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine($"{GlobalTimer.StopWatch.Elapsed.Seconds}s, Temp sorted data saved.");

            //var text = File.ReadAllLines(files.First(), Encoding.UTF8).ToList();

            //Worker./*SortText*/(text);
            //Console.WriteLine($"{GlobalTimer.StopWatch.Elapsed.Seconds}s, Data sorted successfully.");

            //repo.Save(text, DataConfig.SortedDataFile);
            //Console.WriteLine($"{GlobalTimer.StopWatch.Elapsed.Seconds}s, Data saved to output file.");
        }
    }
}
