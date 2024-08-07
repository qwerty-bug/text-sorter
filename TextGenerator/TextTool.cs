using Common;
using System.Text;

namespace DataGenerator
{
    public static class TextTool
    {
        const string Text = "Green Sweater is Jeans and Dress is Jacket Scarf Red Blue Yellow Black";

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
            StringBuilder textLine;
            int fileSize = 0;

            StringBuilder text = new StringBuilder();
            while (true)
            {
                textLine = GetLine();
                text.Append(textLine);
                fileSize += Encoding.UTF8.GetByteCount(textLine.ToString());

                if (fileSize >= Common.FileOptions.Size100MB)
                    break;

                text.AppendLine();
            }

            return text.ToString();
        }

        public static StringBuilder GetLine()
        {
            var rdm = new Random();
            StringBuilder strBld = new StringBuilder(rdm.Next(0, int.MaxValue).ToString())
                .Append('.');
            var wordsInLine = rdm.Next(5, 15);
            foreach (var i in Enumerable.Range(1, wordsInLine))
            {
                var word = Words.ElementAt(rdm.Next(WordsSize));
                if (i == 1)
                    word = string.Concat(word[0].ToString().ToUpper(), word.AsSpan(1));

                strBld.Append(' ').Append(word);
            }

            return strBld;
        }
    }
}
