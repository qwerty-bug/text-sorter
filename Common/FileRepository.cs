using System.Text;

namespace Common
{
    public class FileRepository
    {
        private readonly SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1,1);

        public void Save(string data, string fileName)
        {
            semaphoreSlim.Wait();
            try
            {
                using StreamWriter writer = new StreamWriter(fileName, true, Encoding.UTF8, FileOptions.BufferSize8MB);
                writer.Write(data);
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }
        public string Persist(int id, IReadOnlyList<string> text)
        {
            var fileName = FileOptions.GetSortedTempDataFileName(id);
            using (StreamWriter writer = new StreamWriter(fileName, false, Encoding.UTF8, bufferSize: FileOptions.BufferSize8MB))
            {
                foreach (string line in text)
                {
                    writer.WriteLine(line);
                }
            }

            Logger.Log($"File {fileName} saved");
            return fileName;
        }
    }
}
