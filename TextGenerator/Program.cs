// See https://aka.ms/new-console-template for more information
using Common;
using DataGenerator;

Logger.Log($"Hello, TextGenerator!");
Logger.Log($"============================================");
Logger.Log($"How many data do you want to generate in GB?");
var userInput = Console.ReadLine();
if (!int.TryParse(userInput, out var dataSize))
{
    Logger.Log($"Invalid number, closing app!");
    return;
}

if(dataSize <= 0 ||  dataSize > 100)
{
    Logger.Log($"Value {dataSize} is too large, closing app!");
    return;
}

GlobalTimer.StartNew();
var generator = new TextGenerator();
generator.Generate(dataSize);
GlobalTimer.StopWatch.Stop();

Logger.Log($"============================================");
Logger.Log($"Total processing time: {GlobalTimer.StopWatch.Elapsed.TotalSeconds}s.");

Console.ReadKey();


