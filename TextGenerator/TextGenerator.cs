﻿using Common;

namespace TextGenerator
{
    public class TextGenerator
    {
        private const int NumberOfTasks = 10;

        public void Generate(int dataGBLimit)
        {
            if (File.Exists(Options.SampleDataFile))
            {
                File.Delete(Options.SampleDataFile);
            }

            Logger.Log($"Start generating {dataGBLimit}GB of text data..");

            var repo = new FileRepository();
            for (int i = 0; i < dataGBLimit; i++)
            {
                var tasks = new Task[NumberOfTasks];
                for (int t = 0; t < NumberOfTasks; t++)
                {
                    var chunk = t;
                    tasks[t] = Task
                        .Run(() =>
                        {
                            var result = TextTool.GetChunk();
                            Logger.Log($"Created part {chunk + 1}/10 for {i+1}GB of total {dataGBLimit}GB");
                            return result;
                        })
                        .ContinueWith(dataTask =>
                        {
                            repo.Save(dataTask.Result, Options.SampleDataFile);
                            Logger.Log($"Saved part {chunk + 1}/10 for {i + 1}GB of total {dataGBLimit}GB");
                        });
                }
                Task.WaitAll(tasks);
                Logger.Log($"Data part {i + 1}GB of {dataGBLimit}GB saved successfully.");
            }

            Logger.Log($"All data saved successfully in: {Options.SampleDataFile}.");
        }
    }
}
