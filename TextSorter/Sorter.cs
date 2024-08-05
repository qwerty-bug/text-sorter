using Common;
using System.Collections.Immutable;
using System.Text;

namespace TextSorter
{
    public class Sorter
    {
        public void SortAndSaveData()
        {
            Console.WriteLine($"Start reading file: {GlobalTimer.StopWatch.Elapsed.TotalSeconds}s.");

            //var text = File.ReadLines(FileRepository.FilePath, Encoding.UTF8).Take(5).ToList();
            var text = File.ReadAllLines(FileRepository.SampleDataFile, Encoding.UTF8).ToList();
            Console.WriteLine($"{GlobalTimer.StopWatch.Elapsed.Seconds}s, Data loaded from file.");

            SortText(text);
            Console.WriteLine($"{GlobalTimer.StopWatch.Elapsed.Seconds}s, Data sorted successfully.");

            var repo = new FileRepository();
            repo.Save(text, FileRepository.SortedDataFile);
            Console.WriteLine($"{GlobalTimer.StopWatch.Elapsed.Seconds}s, Data saved to output file.");
        }

        public static void SortText(List<string> text)
        {
            text
                .Sort((line1, line2) =>
                {
                    var first = line1.Split('.');
                    var firstNumber = first[0];
                    var firstText = first[1];

                    var second = line2.Split('.');
                    var secondNumber = second[0];
                    var secondText = second[1];

                    var result = string.Compare(firstText, secondText, StringComparison.Ordinal);
                    if (result != 0)
                    {
                        return result;
                    }

                    return string.Compare(firstNumber, secondNumber, StringComparison.Ordinal);
                });
        }
    }
}
