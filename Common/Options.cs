namespace Common
{
    public static class Options
    {
        private static string outpuFileName = $"SORTED_DATA_{DateTime.Now:HH_mm_ss}.txt";

        public const string SampleDataFile = "SAMPLE_DATA.txt";
        public static string SortedOutputDataFile { get { return outpuFileName; } }
        public static string GetSortedTempDataFileName(int id) => $"SortedTempData{id}.txt";

        public const int Size1GB = 1073741824; //1024*1024*1024
        public const int Size1MB = 1048576; //1024*1024
        public const int Size100MB = 104857600; //1024*1024*100

        // OPTIONS TO TUNE
        public const int BufferSize64MB = 67108864;
        public const int ChunkSize = 209715200;
        public const int ExternalSortOpenedFilesLimit = 8;
        public const int ExternalSortAsyncJobsLimit = 5;
        /*
         * 536870912 => 512MB
         * 268435456 => 256MB
         * 209715200 => 200MB
         * 134217728 => 128MB
         * 67108864 => 64MB
         * 33554432 => 32MB
         * */

    }
}
