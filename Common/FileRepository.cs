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
                using StreamWriter writer = new StreamWriter(fileName, true, Encoding.UTF8, Options.BufferSize64MB);
                writer.Write(data);
                FileCleaner.Add(fileName);
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }
    }
}
