using Common;
using System.Text;
using TextSorter.ExternalSort;

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
            using (StreamReader reader = new StreamReader(fileStream, Encoding.UTF8, bufferSize: SorterFileConfig.BufferSize25MB))
            {
                var line = string.Empty;
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    if (line is null)
                        break;

                    lines.Add(line);

                    fileSize += Encoding.UTF8.GetByteCount(line);
                    if (fileSize >= SorterFileConfig.Size100MB)
                    {
                        var tempList = new List<string>(lines);
                        var id = chunkId;
                        sortTasks.Add(Task.Run(() =>
                        {
                            return SortAndSave(id, tempList);
                        }));

                        lines = new List<string>();
                        fileSize = 0;
                        chunkId++;
                    }

                }
            }

            //last run
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
            Logger.Log($"Chunk {chunkId} start sorting...");
            SortText3(lines, chunkId);
            var repo = new FileRepository();
            return repo.Persist(chunkId, lines);
        }

        [Obsolete("Slower than SortText3")]
        public static List<string> SortText(List<string> text, int chunkId)
        {
            text.Sort((line1, line2) =>
            {
                var first = line1.Split(".");
                var firstNumber = first[0];
                var firstText = first[1];

                var second = line2.Split(".");
                var secondNumber = second[0];
                var secondText = second[1];

                var result = string.Compare(firstText, secondText, StringComparison.Ordinal);
                if (result != 0)
                {
                    return result;
                }

                return int.Parse(firstNumber) > int.Parse(secondNumber) ? 1 : -1;
            });

            //Logger.Log($"Chunk: {chunkId} of data sorted");

            return text;
        }

        public static List<string> SortText3(List<string> text, int chunkId)
        {
            text.Sort(Sort2Lines);
            Logger.Log($"Chunk: {chunkId} of data sorted");

            return text;
        }

        public static int Sort2Lines(string line1, string line2)
        {
            var sepPos1 = line1.IndexOf('.');
            Span<char> span1 = line1.ToCharArray();
            var firstN = span1.Slice(0, sepPos1);
            var firstS = span1.Slice(sepPos1 + 2);

            var sepPos2 = line2.IndexOf('.');
            Span<char> span2 = line2.ToCharArray();
            var secondN = span2.Slice(0, sepPos2);
            var secondS = span2.Slice(sepPos2 + 2);

            var result = string.Compare(firstS.ToString(), secondS.ToString(), StringComparison.Ordinal);
            if (result != 0)
            {
                return result;
            }

            return int.Parse(firstN) > int.Parse(secondN) ? 1 : -1;
        }
    }
}
