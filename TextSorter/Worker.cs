using Common;
using System.Text;

namespace TextSorter
{
    public static class Worker
    {
        public static List<string> SplitIntoChunks(string baseFile)
        {
            int chunkId = 0;
            var fileSize = 0;
            var sortTasks = new List<Task<string>>();
            var lines = new List<string>();

            var fileStream = File.OpenRead(baseFile);
            using (StreamReader reader = new StreamReader(fileStream, Encoding.UTF8, bufferSize: Common.FileOptions.BufferSize32MB))
            {
                var line = string.Empty;
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    if (line is null)
                        break;

                    lines.Add(line);

                    fileSize += Encoding.UTF8.GetByteCount(line);
                    if (fileSize >= Common.FileOptions.Size50MB)
                    {
                        var tempList = new List<string>(lines);
                        var id = chunkId;
                        sortTasks.Add(
                            Task.Run(() => SortAndSave(id, tempList)));

                        lines = new List<string>();
                        fileSize = 0;

                        chunkId++;
                    }
                }
            }

            //remaining text
            if (lines.Count > 0)
            {
                sortTasks.Add(
                    Task.Run(() => SortAndSave(chunkId, lines)));
            }

            Task.WaitAll(sortTasks.ToArray());

            return sortTasks.Select( t => t.Result).ToList();
        }

        private static string SortAndSave(int chunkId, List<string> lines)
        {
            Logger.Log($"Chunk {chunkId} start sorting");
            SortText(lines);

            Logger.Log($"Chunk: {chunkId} of data sorted");

            var fileName = Common.FileOptions.GetSortedTempDataFileName(chunkId);
            using StreamWriter writer = new StreamWriter(fileName, false, Encoding.UTF8, bufferSize: Common.FileOptions.BufferSize8MB);
            foreach (string line in lines)
            {
                writer.WriteLine(line);
            }

            Logger.Log($"File {fileName} saved");
            return fileName;
        }

        public static List<string> SortText(List<string> text)
        {
            text.Sort(Sort2Lines);
            return text;
        }

        public static int Sort2Lines(string line1, string line2)
        {
            var sepPos1 = line1.IndexOf('.', StringComparison.Ordinal);
            var span1 = line1.AsSpan();
            var firstS = span1.Slice(sepPos1 + 2);

            var sepPos2 = line2.IndexOf('.', StringComparison.Ordinal);
            var span2 = line2.AsSpan();
            var secondS = span2.Slice(sepPos2 + 2);

            var result = firstS.CompareTo(secondS, StringComparison.Ordinal);
            if (result != 0)
            {
                return result;
            }

            var firstN = span1.Slice(0, sepPos1);
            var secondN = span2.Slice(0, sepPos2);
            return int.Parse(firstN) > int.Parse(secondN) ? 1 : -1;
        }
    }
}
