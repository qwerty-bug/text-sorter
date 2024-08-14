// See https://aka.ms/new-console-template for more information
using Common;
using TextSorter;

Logger.Log($"Hello, Text Sorter!");
Logger.Log($"============================================");
Logger.Log($"Input data loaded from file: '{Options.SampleDataFile}'.");
long size = new FileInfo(Options.SampleDataFile).Length / Options.Size1GB;
Logger.Log($"Input file size: {size}GB");
Logger.Log($"Chunk size: {Options.ChunkSize / (Options.Size1MB)}MB");
Logger.Log($"External sort concurrent file limit: {Options.ExternalSortOpenedFilesLimit}");
Logger.Log($"External sort async job limit: {Options.ExternalSortAsyncJobsLimit}");
Logger.Log($"Stream Buffer size: {Options.BufferSize64MB/ Options.Size1MB}MB");
Logger.Log($"--------------------------------------------");

GlobalTimer.StartNew();
var sorter = new Sorter();
sorter.Process();

GlobalTimer.StopWatch.Stop();

Logger.Log($"============================================");
Logger.Log("");
Logger.Log($"><><><><>><><><><><><>><><><><><><>><><><><><><><");
Logger.Log("");
Logger.Log($"STATS:");
Logger.Log($" Total processing time: {GlobalTimer.StopWatch.Elapsed.TotalSeconds:0.00}s [{GlobalTimer.StopWatch.Elapsed.ToString(@"hh\:mm\:ss\.ff")}]");
Logger.Log($" Average:               {GlobalTimer.StopWatch.Elapsed.TotalSeconds / size:0.00}s per 1GB");
Logger.Log($" Output file:           '{Options.SortedOutputDataFile}'");
Logger.Log($" Output file size:      {new FileInfo(Options.SortedOutputDataFile).Length / Options.Size1GB}GB");
Logger.Log("");
Logger.Log($"><><><><>><><><><><><>><><><><><><>><><><><><><><");
Logger.Log("");
Logger.Log($"Press any key to close application...");

Console.ReadKey();