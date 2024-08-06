namespace Common
{
    public static class DataConfig
    {
        public const string SampleDataFile = "SampleData.txt";
        public const string SortedDataFile = "SortedData.txt";
        public static string GetUnsortedTempDataFileName(int id) => $"TempData{id}.txt";
        public static string GetSortedTempDataFileName(int id) => $"SortedTempData{id}.txt";
        public const int Size100MB = 104857600; //1024*1024*100
        public const int Size50MB = 52428800; //1024*1024*50
        public const int BufferSize25MB = 10485760; //1024*1024*10

    }
}
