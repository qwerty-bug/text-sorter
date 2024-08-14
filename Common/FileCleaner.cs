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
            files.Remove(Options.SortedOutputDataFile); //leave output file

            int filesCount = files.Count;
            if (filesCount == 0)
                return;

            foreach (string fileName in files.ToList())
            {
                File.Delete(fileName);
            }
            Logger.Log($"Deleted: {filesCount} temp files successfully.");
        }
    }
}
