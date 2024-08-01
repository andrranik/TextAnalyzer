using TextAnalyzer.Services.Models;

namespace TextAnalyzer.Services;

public sealed class WordUsageAnalyzerByLines(WordUsageAnalyzerSettings settings) : WordUsageAnalyzer(settings)
{
    public override List<WordsCountResult> AnalyzeFiles()
    {
        Parallel.ForEach(Directory
            .EnumerateFiles(settings.DirectoryPath, settings.FileSearchPattern, SearchOption.TopDirectoryOnly)
            .Select(File.ReadLines)
            .SelectMany(x => x), CountWordsInText);

        return GetNMaxItems();
    }
}