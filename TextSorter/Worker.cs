using TextSorter.ExternalSort;

namespace TextSorter
{
    public static class Worker
    {
        public static List<string> Sort(string fileName)
        {
            var text = File.ReadAllLines(fileName).ToList();

            text = SortText(text);

            return text;
        }
        public static Comparison<string> comparison = (line1, line2) =>
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
        };

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

        public static List<string> SortText(List<string> text)
        {
            text.Sort(comparison);

            return text;
        }
    }
}
