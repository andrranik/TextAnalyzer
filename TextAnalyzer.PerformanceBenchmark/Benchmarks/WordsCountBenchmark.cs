using BenchmarkDotNet.Attributes;
using TextAnalyzer.Services;
using TextAnalyzer.Services.Models;

namespace TextAnalyzer.PerformanceBenchmark.Benchmarks;

public class WordsCountBenchmark
{
    private WordUsageAnalyzerByLines _analyzerByLines;
    private WordUsageAnalyzerByChunks _analyzerByChunks;
    
    public WordsCountBenchmark()
    {
        var settings = new WordUsageAnalyzerSettings
        {
            DirectoryPath = "<Path To Directory With Files>",
            MinWordLength = 4,
            FileSearchPattern = "*.txt",
            ResultItemsCount = 10
        };

        _analyzerByLines = new WordUsageAnalyzerByLines(settings);
        _analyzerByChunks = new WordUsageAnalyzerByChunks(settings);
    }

    [Benchmark]
    public List<WordsCountResult> WordsCounter() => _analyzerByLines.AnalyzeFiles();

    [Benchmark]
    public List<WordsCountResult> BufferWordsCount() => _analyzerByChunks.AnalyzeFiles();
}