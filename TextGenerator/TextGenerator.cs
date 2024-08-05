using Common;

namespace DataGenerator
{
    public class TextGenerator
    {
        private const int NumberOfTasks = 10;

        public void Generate(int dataGBLimit)
        {
            if (File.Exists(DataConfig.SampleDataFile))
            {
                File.Delete(DataConfig.SampleDataFile);
            }

            Console.WriteLine($"{GlobalTimer.StopWatch.Elapsed.Seconds}s, Start generating {dataGBLimit}GB of text data ...");

            var repo = new FileRepository();
            for (int i = 0; i < dataGBLimit; i++)
            {
                var tasks = new Task[NumberOfTasks];
                for (int t = 0; t < NumberOfTasks; t++)
                {
                    tasks[t] = Task
                        .Run(() =>
                        {
                            var result = TextTool.GetChunk();
                            Console.WriteLine($"{GlobalTimer.StopWatch.Elapsed.Seconds}s, Created data for {i + 1}GB of {dataGBLimit}GB...");
                            return result;
                        })
                        .ContinueWith(dataTask =>
                        {
                            repo.Save(dataTask.Result, DataConfig.SampleDataFile);
                            Console.WriteLine($"{GlobalTimer.StopWatch.Elapsed.Seconds}s, Saved data for {i + 1}GB of {dataGBLimit}GB...");
                        });
                }
                Task.WaitAll(tasks);
                Console.WriteLine($"{GlobalTimer.StopWatch.Elapsed.Seconds}s, Text {i + 1}GB of {dataGBLimit}GB saved successfully.");
            }

            Console.WriteLine($"All data saved successfully.");
        }
    }
}
