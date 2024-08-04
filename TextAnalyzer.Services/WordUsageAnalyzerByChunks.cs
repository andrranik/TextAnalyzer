using TextAnalyzer.Services.Models;

namespace TextAnalyzer.Services;

public sealed class WordUsageAnalyzerByChunks(WordUsageAnalyzerSettings settings) : WordUsageAnalyzer(settings)
{
    private static char[] _delimiters =
    [
        ' ', ',', '.', '!', '?', ';', ':', '-', '_', '(', ')', '[', ']', '{', '}', '\'', '\"', '\n', '\r', '\t'
    ];
    
    public override List<WordsCountResult> AnalyzeFiles()
    {
        Parallel.ForEach(GetChunks(), CountWordsInText);
        return GetNMaxItems();
    }
    
    private const int BufferSize = 1000;

    private IEnumerable<string> GetChunks()
    {
        foreach (var filePath in Directory.EnumerateFiles(settings.DirectoryPath, settings.FileSearchPattern,
                     SearchOption.TopDirectoryOnly))
        {
            using var reader = new StreamReader(filePath);
            var buffer = new char[BufferSize];
            int bytesRead;
            while ((bytesRead = reader.ReadBlock(buffer, 0, buffer.Length)) > 0)
            {
                var lastDelimiterIndex = FindLastDelimiter(buffer, bytesRead);

                // Обработка прочитанных данных, если необходимо
                var chunk = new string(buffer, 0, lastDelimiterIndex + 1);
                // Переместить позицию чтения на следующий символ после разделителя
                reader.BaseStream.Seek(lastDelimiterIndex + 1 - bytesRead, SeekOrigin.Current);
                reader.DiscardBufferedData();
                yield return chunk;
            }
        }
    }

    private static int FindLastDelimiter(IReadOnlyList<char> buffer, int length)
    {
        for (var i = length - 1; i >= 0; i--)
            if (Array.Exists(_delimiters, delimiter => delimiter == buffer[i]))
                return i;

        return length - 1; // Если разделитель не найден, вернуть последний индекс буфера
    }
}