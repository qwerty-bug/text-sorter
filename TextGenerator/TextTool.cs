using System.Text;

namespace FileGenerator
{
    public static class TextTool
    {
        private const int _100MB = 104857600;

        const string Text = "But I must explain to you how all this mistaken idea of denouncing pleasure and praising pain was";

        static readonly HashSet<string> Words = new HashSet<string>();

        static readonly int WordsSize;

        static TextTool()
        {
            var words = Text.Split(' ');

            var random = new Random(200);
            foreach (var word in words)
            {
                Words.Add(word.Trim(',').Trim('.'));
            }

            WordsSize = Words.Count;
        }

        public static string GetChunk()
        {
            string textLine = string.Empty;
            int fileSize = 0;

            StringBuilder text = new StringBuilder();
            while (true)
            {
                textLine = GetLine();
                text.AppendLine(textLine);
                fileSize += Encoding.UTF8.GetByteCount(textLine);

                if (fileSize >= _100MB)
                    break;
            }

            return text.ToString();
        }

        public static string GetLine()
        {
            var rdm = new Random();
            StringBuilder strBld = new StringBuilder(rdm.Next(0, int.MaxValue).ToString())
                .Append('.');
            foreach (var i in Enumerable.Range(1, 50))
            {
                strBld.Append(' ').Append(Words.ElementAt(rdm.Next(WordsSize)));
            }

            return strBld.ToString();
        }
    }
}
