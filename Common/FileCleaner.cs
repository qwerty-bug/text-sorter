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
            files.Remove(FileOptions.SortedOutputDataFile); //leave output file
            foreach (string fileName in files)
            {
                File.Delete(fileName);
            }
        }

        public static void CleanOutputFile()
        {
            File.Delete(FileOptions.SortedOutputDataFile);
            if (File.Exists(FileOptions.SortedOutputDataFile))
            {
                Logger.Log($"ERROR: Cannot delete file '{FileOptions.SortedOutputDataFile}', remove file manually and restart the application.");
                Logger.Log($"ERROR: Closing application...");
                Environment.Exit(0);
            }
        }
    }
}
