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

            Logger.Log($"Start generating {dataGBLimit}GB of text data ...");

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
                            Logger.Log($"Created data for {i + 1}GB of {dataGBLimit}GB...");
                            return result;
                        })
                        .ContinueWith(dataTask =>
                        {
                            repo.Save(dataTask.Result, DataConfig.SampleDataFile);
                            Logger.Log($"Saved data for {i + 1}GB of {dataGBLimit}GB...");
                        });
                }
                Task.WaitAll(tasks);
                Logger.Log($"Text {i + 1}GB of {dataGBLimit}GB saved successfully.");
            }

            Logger.Log($"All data saved successfully in: {DataConfig.SampleDataFile}.");
        }
    }
}
