using System.Text;

namespace TextGenerator
{
    public static class TextTool
    {
        const string Text = "green sweater is jeans and dress is jacket scarf red blue yellow black";
        private const int MaxNumber = 1000;
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
            StringBuilder strBld = new StringBuilder(rdm.Next(0, MaxNumber).ToString())
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
