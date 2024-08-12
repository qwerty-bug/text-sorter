﻿// See https://aka.ms/new-console-template for more information
using Common;
using System.IO;
using TextSorter;

Logger.Log($"Hello, Text Sorter!");
Logger.Log($"============================================");
Logger.Log($"Data loaded from file: '{Common.FileOptions.SampleDataFile}'.");
long size = new System.IO.FileInfo(Common.FileOptions.SampleDataFile).Length;
Logger.Log($"File size: {size / Common.FileOptions.Size1GB}GB");
Logger.Log($"--------------------------------------------");

GlobalTimer.StartNew();
var sorter = new Sorter();
sorter.Process();

Logger.Log($"============================================");
Logger.Log($"Total processing time: {GlobalTimer.StopWatch.Elapsed.TotalSeconds}s.");
Logger.Log($"Press any key to close application...");

Console.ReadKey();
