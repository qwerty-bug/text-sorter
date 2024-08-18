using FluentAssertions;
using TextSorter;

namespace TextPlaygroundTests
{
    public class TextSorterWorkerTests
    {
        [Test]
        public void SortTextShouldSortTextCorrectly()
        {
            var expectedText = new List<string>
            {
                "1162780053. Black blue green green scarf red jeans black is yellow black",
                "632184622. Green blue yellow green scarf",
                "1633935166. Jeans blue green green scarf",
                "632184622. Scarf blue yellow green scarf"
            };
            var textToSort = new List<string>
            {
                "632184622. Green blue yellow green scarf",
                "632184622. Scarf blue yellow green scarf",
                "1633935166. Jeans blue green green scarf",
                "1162780053. Black blue green green scarf red jeans black is yellow black"
            };

            var result = Worker.SortText(textToSort);

            result
                .Should()
                .BeEquivalentTo(expectedText, c => c.WithStrictOrdering());
        }

        [Test]
        public void SortTextShouldByNumbersWhenTextIsTheSame()
        {
            var expectedText = new List<string>
            {
                "632184622. Scarf blue green green scarf",
                "1633935166. Scarf blue green green scarf",
                "1162780053. Scarf blue green green scarf red jeans black is yellow black"
            };
            var textToSort = new List<string>
            {
                "1162780053. Scarf blue green green scarf red jeans black is yellow black",
                "1633935166. Scarf blue green green scarf",
                "632184622. Scarf blue green green scarf"
            };

            var result = Worker.SortText(textToSort);

            result
                .Should()
                .BeEquivalentTo(expectedText, c => c.WithStrictOrdering());
        }

        [Test]
        public void SortTextShouldHandleDuplicatesInText()
        {
            var expectedText = new List<string>
            {
                "632184622. Scarf blue green green scarf",
                "1633935166. Scarf blue green green scarf",
                "1633935166. Scarf blue green green scarf",
                "1162780053. Scarf blue green green scarf red jeans black is yellow black"
            };
            var textToSort = new List<string>
            {
                "1162780053. Scarf blue green green scarf red jeans black is yellow black",
                "1633935166. Scarf blue green green scarf",
                "1633935166. Scarf blue green green scarf",
                "632184622. Scarf blue green green scarf"
            };

            var result = Worker.SortText(textToSort);

            result
                .Should()
                .BeEquivalentTo(expectedText, c => c.WithStrictOrdering());
        }

        [Test]
        public void SortTextShouldHandleDuplicatesInNumbers()
        {
            var expectedText = new List<string>
            {
                "632184622. Scarf blue green green scarf",
                "1633935166. Scarf blue green green scarf",
                "1633935166. Scarf blue green green scarf",
                "1633935166. Scarf blue green green scarf",
                "1633935166. Scarf blue green green scarf",
                "1633935166. Scarf blue green green scarf",
                "1633935166. Scarf blue green green scarf",
                "1633935166. Scarf blue green green scarf",
                "1633935166. Scarf blue green green scarf",
                "1633935166. Scarf blue green green scarf",
                "1633935169. Scarf blue green green scarf",
            };
            var textToSort = new List<string>
            {
                "1633935166. Scarf blue green green scarf",
                "1633935166. Scarf blue green green scarf",
                "632184622. Scarf blue green green scarf",
                "1633935166. Scarf blue green green scarf",
                "1633935166. Scarf blue green green scarf",
                "1633935166. Scarf blue green green scarf",
                "1633935169. Scarf blue green green scarf",
                "1633935166. Scarf blue green green scarf",
                "1633935166. Scarf blue green green scarf",
                "1633935166. Scarf blue green green scarf",
                "1633935166. Scarf blue green green scarf",
            };

            var result = Worker.SortText(textToSort);

            result
                .Should()
                .BeEquivalentTo(expectedText, c => c.WithStrictOrdering());
        }
    }
}