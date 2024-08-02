// See https://aka.ms/new-console-template for more information
using FileGenerator;
using System.Diagnostics;

Console.WriteLine("Hello, TextGenerator!");
Console.WriteLine($"============================================");

var generator = new TextGenerator();
GlobalTimer.Stopwatch = Stopwatch.StartNew();
generator.Generate(5);
GlobalTimer.Stopwatch.Stop();
Console.WriteLine($"============================================");
Console.WriteLine($"Total processing time: {GlobalTimer.Stopwatch.Elapsed.TotalSeconds}s.");

Console.ReadKey();
//save file with text
//sort text


