namespace TextAnalyzer.Services.Models;

public class WordUsageAnalyzerSettings
{
    public required string DirectoryPath { get; init; }
    public int MinWordLength { get; init; }
    public required string FileSearchPattern { get; init; }
    public int ResultItemsCount { get; init; }
}