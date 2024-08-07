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

        ////return list of created files
        //public IEnumerable<string> SplitIntoChunks(string baseFile)
        //{
        //    int id = 0;
        //    var fileSize = 0;
        //    var stringBuffer = new StringBuilder();
        //    var chunkNames = new List<string>();
        //    var fileStream = File.OpenRead(baseFile);
        //    using (StreamReader reader = new StreamReader(fileStream, Encoding.UTF8, bufferSize: DataConfig.BufferSize128KB))
        //    {
        //        var line = string.Empty;
        //        while (!reader.EndOfStream)
        //        {
        //            line = reader.ReadLine();
        //            if (line is null)
        //                break;

        //            stringBuffer.AppendLine(line);

        //            fileSize += Encoding.UTF8.GetByteCount(line);
        //            if (fileSize >= DataConfig.Size100MB)
        //            {
        //                Worker.SortText

        //                chunkNames.Add(Persist(id, stringBuffer));
        //                stringBuffer.Clear();
        //                fileSize = 0;
        //                id++;
        //            }

        //        }
        //    }

        //    //last run
        //    if (stringBuffer.Length > 0)
        //    {
        //        chunkNames.Add(Persist(id, stringBuffer));
        //    }

        //    return chunkNames;
        //}

        public string Persist(int id, StringBuilder stringBuffer)
        {
            var file = DataConfig.GetUnsortedTempDataFileName(id);
            using (StreamWriter writer = new StreamWriter(file, false, Encoding.UTF8, bufferSize: DataConfig.BufferSize25MB))
            {
                writer.Write(stringBuffer.ToString());
            }

            return file;
        }

        public string Persist(int id, IReadOnlyList<string> text)
        {
            var fileName = DataConfig.GetSortedTempDataFileName(id);
            using (StreamWriter writer = new StreamWriter(fileName, false, Encoding.UTF8, bufferSize: DataConfig.BufferSize25MB))
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
