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

        public async Task Save(List<string> lines, string fileName)
        {
            semaphoreSlim.Wait();
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName, false, Encoding.UTF8))
                {
                    foreach (string line in lines)
                    {
                        await writer.WriteLineAsync(line);
                    }
                }
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        //return list of created files
        public IEnumerable<string> SplitIntoChunks(string baseFile)
        {
            int id = 0;
            var fileSize = 0;
            var stringBuffer = new StringBuilder();
            var chunkNames = new List<string>();
            using (StreamReader reader = new StreamReader(baseFile, Encoding.UTF8))
            {
                var line = string.Empty;
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    if (line is null)
                        break;

                    stringBuffer.AppendLine(line);

                    fileSize += Encoding.UTF8.GetByteCount(line);
                    if (fileSize >= DataConfig.Size50MB)
                    {
                        chunkNames.Add(Persist(id, stringBuffer));
                        stringBuffer.Clear();
                        fileSize = 0;
                        id++;
                    }

                }
            }

            //last run
            if (stringBuffer.Length > 0)
            {
                chunkNames.Add(Persist(id, stringBuffer));
            }

            return chunkNames;
        }

        public string Persist(int id, StringBuilder stringBuffer)
        {
            var file = DataConfig.UnsortedTempDataFile(id);
            using (StreamWriter writer = new StreamWriter(file, false, Encoding.UTF8))
            {
                writer.Write(stringBuffer.ToString());
            }

            return file;
        }
    }
}
