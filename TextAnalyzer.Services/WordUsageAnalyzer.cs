using System.Collections.Concurrent;
using TextAnalyzer.Services.Models;

namespace TextAnalyzer.Services;

public abstract class WordUsageAnalyzer(WordUsageAnalyzerSettings settings)
{
    public abstract List<WordsCountResult> AnalyzeFiles();
    
    private readonly ConcurrentDictionary<string, int> _mostUsedWords = new();
    
    private protected static readonly char[] Delimiters = [' ', ',', '.', '!', '?', ';', ':', '-', '_', '(', ')', '[', ']', '{', '}', '\'', '\"', '\n', '\r', '\t'];

    private protected void CountWordsInText(string line)
    {
        var words = line.Split(Delimiters, StringSplitOptions.RemoveEmptyEntries);

        foreach (var word in words)
        {
            if (word.Length <= settings.MinWordLength) 
                continue;
            
            if (!_mostUsedWords.TryAdd(word, 1))
            {
                _mostUsedWords[word]++;
            }
        }
    }
    
    private protected List<WordsCountResult> GetNMaxItems()
    {
        return _mostUsedWords
            .OrderByDescending(kvp => kvp.Value)
            .Take(settings.ResultItemsCount)
            .Select(x => new WordsCountResult(x.Key, x.Value))
            .ToList();
    }
}