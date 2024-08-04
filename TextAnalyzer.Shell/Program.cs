using TextAnalyzer.Services;
using TextAnalyzer.Services.Models;
using TextAnalyzer.Shell.Utils;

var settings = new WordUsageAnalyzerSettings
{
    DirectoryPath = ConfigurationHelper.GetStorageFolderPath(),
    MinWordLength = ConfigurationHelper.GetMinWordLength(),
    FileSearchPattern = ConfigurationHelper.GetFileSearchPattern(),
    ResultItemsCount = ConfigurationHelper.GetResultItemsCount(),
};

WordUsageAnalyzer analyzer = ConfigurationHelper.GetMode() switch
{
    "Chunks" => new WordUsageAnalyzerByChunks(settings),
    "Lines" => new WordUsageAnalyzerByLines(settings),
    "Files" => new WordUsageAnalyzerByFiles(settings),
    _ => throw new ArgumentOutOfRangeException("Mode name are incorrect")
};

var result = analyzer.AnalyzeFiles();
foreach (var wordsCountResult in result)
{
    Console.WriteLine($"{wordsCountResult.Word} - {wordsCountResult.Count} times.");
}

Console.WriteLine("Done!");
Console.ReadKey();