// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using TextAnalyzer.PerformanceBenchmark.Benchmarks;

var summary = BenchmarkRunner.Run<WordsCountBenchmark>();