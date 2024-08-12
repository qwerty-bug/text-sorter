namespace Common
{
    public static class FileOptions
    {
        public const string SampleDataFile = "SampleData.txt";
        public const string SortedOutputDataFile = "SortedData.txt";
        public static string GetSortedTempDataFileName(int id) => $"SortedTempData{id}.txt";
        public const int Size1GB = 1073741824; //1024*1024*100
        public const int Size1MB = 1048576; //1024*1024
        public const int ChunkSize = 209715200; //1024*1024*200
        public const int Size100MB = 104857600; //1024*1024*100
        public const int BufferSize32MB = 33554432; //1024*1024*10

    }
}
