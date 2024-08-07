// See https://aka.ms/new-console-template for more information
using Common;
using TextSorter;

Logger.Log($"Hello, Text Sorter!");
Logger.Log($"============================================");
Logger.Log($"Data loaded from file: '{Common.FileOptions.SampleDataFile}'.");
Logger.Log($"--------------------------------------------");


GlobalTimer.StartNew();
var sorter = new Sorter();
await sorter.Process();

Logger.Log($"============================================");
Logger.Log($"Total processing time: {GlobalTimer.StopWatch.Elapsed.TotalSeconds}s.");
Logger.Log($"Press any key to close application...");

Console.ReadKey();
