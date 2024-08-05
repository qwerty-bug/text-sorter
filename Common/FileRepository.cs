using System.Text;

namespace Common
{
    public class FileRepository
    {
        public const string SampleDataFile = "100MBSorterSampleData.txt";
        public const string SortedDataFile = "SortedData.txt";

        private readonly SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

        public void Save(string data, string fileName)
        {
            semaphoreSlim.Wait();
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName, true, Encoding.UTF8))
                {
                    writer.WriteLine(data);
                }
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        public void Save(List<string> lines, string fileName)
        {
            semaphoreSlim.Wait();
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName, true, Encoding.UTF8))
                {
                    foreach (string line in lines)
                    {
                        writer.WriteLine(lines);
                    }
                }
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }
    }
}
