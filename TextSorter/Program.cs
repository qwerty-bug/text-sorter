// See https://aka.ms/new-console-template for more information
using Common;
using TextSorter;

Logger.Log($"Hello, Text Sorter!");
Logger.Log($"============================================");
Logger.Log($"Input data loaded from file: '{Common.FileOptions.SampleDataFile}'.");
long size = new FileInfo(Common.FileOptions.SampleDataFile).Length / Common.FileOptions.Size1GB;
Logger.Log($"Input file size: {size}GB");
Logger.Log($"Chunk size: {Common.FileOptions.ChunkSize / (Common.FileOptions.Size1MB)}MB");
Logger.Log($"--------------------------------------------");

GlobalTimer.StartNew();
var sorter = new Sorter();
sorter.Process();

GlobalTimer.StopWatch.Stop();

Logger.Log($"============================================");
Logger.Log("");
Logger.Log($"><><><><>><><><><><><>><><><><><><>><><");
Logger.Log("");
Logger.Log($"STATS:");
Logger.Log($" Total processing time: {GlobalTimer.StopWatch.Elapsed.TotalSeconds:0.00}s [{GlobalTimer.StopWatch.Elapsed.ToString(@"hh\:mm\:ss\.ff")}]");
Logger.Log($" Average:               {GlobalTimer.StopWatch.Elapsed.TotalSeconds / size:0.00}s per 1GB");
Logger.Log($" Output file:           '{Common.FileOptions.SortedOutputDataFile}'");
Logger.Log($" Output file size:      {new FileInfo(Common.FileOptions.SortedOutputDataFile).Length / Common.FileOptions.Size1GB}GB");
Logger.Log("");
Logger.Log($"><><><><>><><><><><><>><><><><><><>><><");
Logger.Log("");
Logger.Log($"Press any key to close application...");

Console.ReadKey();
