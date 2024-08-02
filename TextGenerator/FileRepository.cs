using System.Text;

namespace FileGenerator
{
    public class FileRepository
    {
        public const string FilePath = "sampleData.txt";

        private readonly SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

        public void Save(string data)
        {
            semaphoreSlim.Wait();
            try
            {
                using (StreamWriter writer = new StreamWriter(FileRepository.FilePath, true, Encoding.UTF8))
                {
                    writer.WriteLine(data);
                }
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }
    }
}
