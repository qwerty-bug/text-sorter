// See https://aka.ms/new-console-template for more information
using Common;
using TextSorter;

Logger.Log($"Hello, Text Sorter!");
Logger.Log($"============================================");
Logger.Log($"Data loaded from file: '{DataConfig.SampleDataFile}'.");
Logger.Log($"--------------------------------------------");


GlobalTimer.StartNew();
var sorter = new Sorter();
await sorter.Process();
GlobalTimer.StopWatch.Stop();

Logger.Log($"============================================");
Logger.Log($"Total processing time: {GlobalTimer.StopWatch.Elapsed.TotalSeconds}s.");

Console.ReadKey();
