namespace TextSorter.ExternalSort
{
    public class LineDetails
    {
        public required StreamReader Reader { get; set; }
        public string Value { get; set; } = string.Empty;
        public int ReaderId { get; set; }
    }
}
