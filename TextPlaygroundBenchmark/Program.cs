// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using TextPlaygroundBenchmark;

//var workerBenchmark = BenchmarkRunner.Run<WorkerBenchmark>();
var kWayMergeSortBenchmark = BenchmarkRunner.Run<KWayMergeSortBenchmark>();