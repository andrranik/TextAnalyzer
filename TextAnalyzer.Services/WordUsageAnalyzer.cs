using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using TextAnalyzer.Services.Models;

namespace TextAnalyzer.Services;

public abstract class WordUsageAnalyzer(WordUsageAnalyzerSettings settings)
{
    public abstract List<WordsCountResult> AnalyzeFiles();
    
    private readonly ConcurrentDictionary<string, ReferenceCounter> _mostUsedWords = new();
    
    private protected readonly string Pattern = $@"\b\w{{{settings.MinWordLength + 1},}}\b";
    
    private protected void CountWordsInText(string line)
    {
        foreach (Match word in Regex.Matches(line, Pattern))
        {
            if (word.Length <= settings.MinWordLength) 
                continue;

            _mostUsedWords.AddOrUpdate(word.Value, new ReferenceCounter(), (key, counter) =>
            {
                counter.Result = counter.Result + 1;
                return counter;
            });
        }
    }
    
    private protected List<WordsCountResult> GetNMaxItems()
    {
        return _mostUsedWords
            .OrderByDescending(kvp => kvp.Value.Result)
            .Take(settings.ResultItemsCount)
            .Select(x => new WordsCountResult(x.Key, x.Value.Result))
            .ToList();
    }
}