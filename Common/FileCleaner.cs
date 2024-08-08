namespace Common
{
    public static class FileCleaner
    {
        private static List<string> files = new List<string>();

        public static void Add(string fileName)
        {
            files.Add(fileName);
        }

        public static void CleanFiles()
        {
            files.Remove(FileOptions.SortedDataFile); //leave output file
            foreach (string fileName in files)
            {
                File.Delete(fileName);
            }
        }
    }
}
