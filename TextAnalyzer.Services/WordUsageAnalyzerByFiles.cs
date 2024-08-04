using System.Text.RegularExpressions;
using TextAnalyzer.Services.Models;
using TextAnalyzer.Services.Shared;

namespace TextAnalyzer.Services;

public class WordUsageAnalyzerByFiles(WordUsageAnalyzerSettings settings) : WordUsageAnalyzer(settings)
{
    private Dictionary<string, Dictionary<string, ReferenceCounter>> _listOfWords;
    
    public override List<WordsCountResult> AnalyzeFiles()
    {
        var files = Directory.EnumerateFiles(settings.DirectoryPath, settings.FileSearchPattern,
            SearchOption.AllDirectories).ToList();
        _listOfWords = new Dictionary<string, Dictionary<string, ReferenceCounter>>(files.Count);
        
        Parallel.ForEach(files, ReadFile);
        
        SortedList<int, string> top10 = new SortedList<int, string>(new DuplicateKeyComparer<int>());
        foreach (var dict in _listOfWords)
        {
            foreach (var kvp in dict.Value)
            {
                // Добавляем элемент в отсортированный список
                top10.Add(kvp.Value.Result, kvp.Key);

                // Если в списке больше 10 элементов, удаляем наименьший
                if (top10.Count > 10)
                {
                    top10.RemoveAt(0);
                }
            }
        }

        return top10
            .Select(x => new WordsCountResult(x.Value, x.Key))
            .Reverse().ToList();
    }

    private void ReadFile(string fileName)
    {
        var dictinary = new Dictionary<string, ReferenceCounter>();
        _listOfWords.Add(fileName, dictinary);
        foreach (var line in File.ReadLines(fileName))
        {
            CountWordsInTextUseSeparateDictionary(line, dictinary);
        }
    }

    private void CountWordsInTextUseSeparateDictionary(string line, Dictionary<string, ReferenceCounter> dictionary)
    {
        foreach (Match word in Regex.Matches(line, Pattern))
        {
            if (!dictionary.TryAdd(word.Value, new ReferenceCounter()))
            {
                dictionary[word.Value].Result++;
            }
        }
    }
}