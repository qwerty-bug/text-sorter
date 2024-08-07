using System.Text;

namespace Common
{
    public class FileRepository
    {
        private readonly SemaphoreSlim semaphoreSlim;

        public FileRepository(int maxConcurrentSave = 1)
        {
            semaphoreSlim = new SemaphoreSlim(1, maxConcurrentSave);
        }

        public void Save(string data, string fileName)
        {
            semaphoreSlim.Wait();
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName, true, Encoding.UTF8))
                {
                    writer.Write(data);
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
                using (StreamWriter writer = new StreamWriter(fileName, false, Encoding.UTF8))
                {
                    foreach (string line in lines)
                    {
                        writer.WriteLine(line);
                    }
                }
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        public string Persist(int id, StringBuilder stringBuffer)
        {
            var file = SorterFileConfig.GetUnsortedTempDataFileName(id);
            using (StreamWriter writer = new StreamWriter(file, false, Encoding.UTF8, bufferSize: SorterFileConfig.BufferSize25MB))
            {
                writer.Write(stringBuffer.ToString());
            }

            return file;
        }

        public string Persist(int id, IReadOnlyList<string> text)
        {
            var fileName = SorterFileConfig.GetSortedTempDataFileName(id);
            using (StreamWriter writer = new StreamWriter(fileName, false, Encoding.UTF8, bufferSize: SorterFileConfig.BufferSize25MB))
            {
                foreach (string line in text)
                {
                    writer.WriteLine(line);
                }
            }

            Logger.Log($"File {fileName} saved");
            return fileName;
        }

        public void Remove(string fileName)
        {
            if(File.Exists(fileName))
                File.Delete(fileName);
        }
    }
}
