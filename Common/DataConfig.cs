namespace Common
{
    public static class DataConfig
    {
        public const string SampleDataFile = "SorterSampleData.txt";
        public const string SortedDataFile = "SortedData.txt";
        public static string UnsortedTempDataFile(int id) => $"TempData{id}.txt";
        public static string SortedTempDataFile(int id) => $"SortedTempData{id}.txt";
        public const int Size100MB = 104857600; //1024*1024*100
        public const int Size50MB = 52428800; //1024*1024*50
    }
}
