using Common;
using System.Text;
using TextSorter.ExternalSort;

namespace TextSorter
{
    public static class Worker
    {
        //return list of created files
        public static List<string> SplitIntoChunks(string baseFile)
        {
            int chunkId = 0;
            var fileSize = 0;
            //var chunkNames = new List<string>();
            var sortTasks = new List<Task<string>>();
            var lines = new List<string>();

            var fileStream = File.OpenRead(baseFile);
            using (StreamReader reader = new StreamReader(fileStream, Encoding.UTF8, bufferSize: DataConfig.BufferSize128KB))
            {
                var line = string.Empty;
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    if (line is null)
                        break;

                    lines.Add(line);

                    fileSize += Encoding.UTF8.GetByteCount(line);
                    if (fileSize >= DataConfig.Size100MB)
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
            SortText(lines, chunkId);
            var repo = new FileRepository();
            return repo.Persist(chunkId, lines); // TODO save async
        }

        public static List<string> Sort(string fileName)
        {
            var text = File.ReadAllLines(fileName).ToList();

            text = SortText(text, 11111);

            return text;
        }

        public static LineDetails SortText(List<LineDetails> lines)
        {
            lines
                .Sort((line1, line2) =>
                {
                    var first = line1.Value.Split('.');
                    var firstNumber = first[0];
                    var firstText = first[1];

                    var second = line2.Value.Split('.');
                    var secondNumber = second[0];
                    var secondText = second[1];

                    var result = string.Compare(firstText, secondText, StringComparison.Ordinal);
                    if (result != 0)
                    {
                        return result;
                    }

                    return int.Parse(firstNumber) > int.Parse(secondNumber) ? 1 : -1;
                });

            return lines.First();
        }

        public static List<string> SortText(List<string> text, int chunkId)
        {
            text.Sort((line1, line2) =>
            {
                var first = line1.Split('.');
                var firstNumber = first[0];
                var firstText = first[1];

                var second = line2.Split('.');
                var secondNumber = second[0];
                var secondText = second[1];

                var result = string.Compare(firstText, secondText, StringComparison.Ordinal);
                if (result != 0)
                {
                    return result;
                }

                return int.Parse(firstNumber) > int.Parse(secondNumber) ? 1 : -1;
            });

            Logger.Log($"Chunk: {chunkId} of data sorted");

            return text;
        }
    }
}
