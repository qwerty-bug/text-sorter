namespace TextSorter
{
    public class LineData
    {
        public required string Text { get; set; }
        public int Number { get; set; }

        public override int GetHashCode()
        {
            return (Text, Number).GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }
    }
}
