// See https://aka.ms/new-console-template for more information
using FileGenerator;
using System.Diagnostics;

Console.WriteLine("Hello, TextGenerator!");
Console.WriteLine($"============================================");
Console.WriteLine($"How many data do you want to generate in GB?");
var userInput = Console.ReadLine();
if (!int.TryParse(userInput, out var dataSize))
{
    Console.WriteLine($"Invalid number, closing app!");
    return;
}

if(dataSize <= 0 ||  dataSize > 100)
{
    Console.WriteLine($"Value {dataSize} is too large, closing app!");
    return;
}

GlobalTimer.Stopwatch = Stopwatch.StartNew();
var generator = new TextGenerator();
generator.Generate(dataSize);
GlobalTimer.Stopwatch.Stop();

Console.WriteLine($"============================================");
Console.WriteLine($"Total processing time: {GlobalTimer.Stopwatch.Elapsed.TotalSeconds}s.");

Console.ReadKey();


