using FluentAssertions;
using TextSorter;

namespace TextPlaygroundTests
{
    public class TextSorterWorkerTests
    {
        [Test]
        public void SortTextShouldCorrectSort()
        {
            var expectedText = new List<string>
            {
                "632184622. Scarf Blue Green Green Scarf",
                "1633935166. Scarf Blue Green Green Scarf",
                "1162780053. Scarf Blue Green Green Scarf Red Jeans Black is Yellow Black"
            };
            var textToSort = new List<string>
            {
                "1162780053. Scarf Blue Green Green Scarf Red Jeans Black is Yellow Black",
                "1633935166. Scarf Blue Green Green Scarf",
                "632184622. Scarf Blue Green Green Scarf"
            };

            var result = Worker.SortText3(textToSort, 1);

            result
                .Should()
                .BeEquivalentTo(expectedText, c => c.WithStrictOrdering());
        }
    }
}