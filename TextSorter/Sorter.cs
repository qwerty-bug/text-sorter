using Common;
using System.Text;
using TextSorter.ExternalSort;

namespace TextSorter
{
    public class Sorter
    {
        public async Task Process()
        {
            Logger.Log($"Start reading file.");

            var repo = new FileRepository();
            var files = repo.SplitIntoChunks(DataConfig.SampleDataFile);
            Logger.Log($"Main file splitted into {files.Count()} tempFiles.");

            var tasks = new List<Task<string>>();
            repo = new FileRepository(files.Count());
            foreach (var file in files)
            {
                tasks.Add(
                    Task.Run(
                        () =>
                        {
                            var result = Worker.Sort(file);
                            Logger.Log($"File {file} sorted.");
                            return result;
                        }
                    )
                    .ContinueWith(sortedTask =>
                    {
                        var fileName = $"sorted{file}";
                        repo.Save(sortedTask.Result, fileName);
                        repo.Remove(file);
                        Logger.Log($"File 'sorted{file}' saved.");
                        return fileName;
                    }));
            }

            await Task.WhenAll(tasks.ToArray());
            Logger.Log("Temp sorted data saved.");

            var sortedTempFiles = tasks.Select(x => x.Result).ToArray();
            var extSort = new KWayMergeSort(sortedTempFiles);
            await extSort.Sort();

            CleanUp(sortedTempFiles);

            Logger.Log($"Data sorted successfully.");
        }

        private void CleanUp(string[] files)
        {
            foreach (var file in files)
                File.Delete(file);
        }
    }
}
