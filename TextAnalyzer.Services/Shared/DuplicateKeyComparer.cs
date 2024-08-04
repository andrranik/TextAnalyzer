namespace TextAnalyzer.Services.Shared;

public class DuplicateKeyComparer<TKey> : IComparer<TKey> where TKey : IComparable
{
    public int Compare(TKey x, TKey y)
    {
        var result = x.CompareTo(y);

        // Возвращаем 1 для разрешения дубликатов
        return result == 0 ? 1 : result;
    }
}