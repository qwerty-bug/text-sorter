// See https://aka.ms/new-console-template for more information
using Common;
using TextSorter;

Console.WriteLine("Hello, Text Sorter!");
Console.WriteLine($"============================================");
Console.WriteLine($"Data loaded from file: '{DataConfig.SampleDataFile}'.");
Console.WriteLine($"--------------------------------------------");


GlobalTimer.StartNew();
var sorter = new Sorter();
sorter.SortAndSaveData();
GlobalTimer.StopWatch.Stop();

Console.WriteLine($"============================================");
Console.WriteLine($"Total processing time: {GlobalTimer.StopWatch.Elapsed.TotalSeconds}s.");

Console.ReadKey();
