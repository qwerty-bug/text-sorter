namespace TextSorter.ExternalSort
{
    public class LineDetails
    {
        public required StreamReader Reader { get; set; }
        public string? Value { get; set; }
        public int ReaderId { get; set; }
    }
}
