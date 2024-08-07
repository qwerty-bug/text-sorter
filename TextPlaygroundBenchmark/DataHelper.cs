namespace TextPlaygroundBenchmark
{
    public static class DataHelper
    {
        public static List<string> UnsortedRows = new()
        {
            "725865884. Dress Scarf Sweater Green Jeans",
            "578713939. And Jeans is Green Scarf Sweater Dress Jeans Black Blue Scarf is Dress Jeans",
            "17555942. Jeans Dress Blue and Red Yellow is Yellow and Sweater",
            "792749564. Red Jacket and Jacket and Red Scarf Sweater Sweater Scarf Dress Blue Dress Sweater",
            "1850816891. And Yellow Blue Scarf is",
            "1949740920. Scarf Jeans Blue Jeans Sweater Scarf Jeans",
            "717941271. Green Red Red is Red Jacket Scarf Yellow is Black Black Yellow Green Dress",
            "1166913098. Jeans Jacket Jacket Jacket is Sweater Dress Yellow Black Jacket Sweater and",
            "41764729. And Green Jacket Blue Blue Yellow",
            "1118901988. And Black Yellow Jacket Yellow Scarf Yellow Dress Jeans Red Jacket Red and Scarf",
            "419087867. Is Jacket Yellow Sweater is Jeans Green Jeans Red Dress Blue",
            "317113081. Yellow is Blue and Jeans Jeans Yellow Yellow and Green and",
            "472336190. Blue Jeans Green Green Yellow Jeans and Scarf Green and is and"
        };

        public static List<string> LoadUnsortedRows()
        {
            var data = File.ReadAllLines("SampleData100.txt").ToList();
            foreach (var i in Enumerable.Range(0, 10))
            {
                foreach (var j in Enumerable.Range(0, data.Count))
                {
                    data.Add($"{data[j]} test aaa{i}");
                }
            }

            return data.ToList();
        }
    }
}
