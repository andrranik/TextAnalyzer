namespace TextAnalyzer.Services.Models;

public class WordsCountResult(string word, int count)
{
    public string Word { get; set; } = word;
    public int Count { get; set; } = count;
}