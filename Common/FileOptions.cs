namespace Common
{
    public static class FileOptions
    {
        public const string SampleDataFile = "SampleData.txt";
        public const string SortedDataFile = "SortedData.txt";
        public static string GetUnsortedTempDataFileName(int id) => $"TempData{id}.txt";
        public static string GetSortedTempDataFileName(int id) => $"SortedTempData{id}.txt";
        public const int Size1GB = 1073741824; //1024*1024*100
        public const int Size500MB = 524288000; //1024*1024*500
        public const int Size300MB = 314572800; //1024*1024*500
        public const int Size200MB = 209715200; //1024*1024*100
        public const int Size100MB = 104857600; //1024*1024*100
        public const int Size50MB = 52428800; //1024*1024*50
        public const int BufferSize32MB = 33554432; //1024*1024*10
        public const int BufferSize10MB = 10485760; //1024*1024*10
        public const int BufferSize16MB = 16777216; //1024*1024*8
        public const int BufferSize8MB = 8388608; //1024*1024*8
        public const int BufferSize4MB = 4194304; //1024*1024*4
        public const int BufferSize2MB = 2097152; //1024*1024*2
        public const int BufferSize1MB = 1048576; //1024*1024
        public const int BufferSize64KB = 65536; //1024*1024

    }
}
