namespace Common
{
    public static class Options
    {
        private static string outpuFileName = $"SORTED_DATA_{DateTime.Now:HH_mm_ss}.txt";

        static Options()
        {

        }

        public const string SampleDataFile = "SAMPLE_DATA.txt";
        public static string SortedOutputDataFile { get { return outpuFileName; } }
        public static string GetSortedTempDataFileName(int id) => $"SortedTempData{id}.txt";
        public const int Size1GB = 1073741824; //1024*1024*1024
        public const int Size1MB = 1048576; //1024*1024
        public const int Size100MB = 104857600; //1024*1024*100
        public const int BufferSize64MB = 67108864; //1024*1024*64

        public const int ChunkSize = 536870912;//100MB  |  536870912; //1024*1024*512
        public const int ExternalSortConcurrentLimit = 15;
    }
}
