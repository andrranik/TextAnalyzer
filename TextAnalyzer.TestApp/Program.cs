using System.Text;

namespace TextAnalyzer.TestApp;

internal class Program
{
    private static void Main(string[] args)
    {
        GenerateHugeTxtFile();
    }

    private static void GenerateHugeTxtFile()
    {
        var filePath = "random_words.txt";
        long targetSize = 100 * 1024 * 1024; // 100 MB
        var random = new Random();
        char[] separators =
            { ' ', ',', '.', '!', '?', ';', ':', '-', '_', '(', ')', '[', ']', '{', '}', '\'', '\"', '\n', '\r', '\t' };

        string[] wordPool =
        {
            "apple", "banana", "grape", "orange", "lemon", "lime", "pear", "peach", "plum", "cherry",
            "berry", "melon", "kiwi", "mango", "papaya", "guava", "fig", "date", "olive", "coconut",
            "apricot", "avocado", "blackberry", "blueberry", "cranberry", "currant", "elderberry", "gooseberry",
            "raspberry", "strawberry",
            "tangerine", "watermelon", "pomegranate", "persimmon", "quince", "pineapple", "nectarine", "mandarin",
            "lychee", "jackfruit",
            "kiwano", "kumquat", "loquat", "longan", "passionfruit", "rhubarb", "soursop", "starfruit", "ugli", "yuzu",
            "ackee", "bilberry", "boysenberry", "cantaloupe", "casaba", "cherimoya", "damson", "durian", "feijoa",
            "jambul",
            "jostaberry", "jujube", "langsat", "loganberry", "mulberry", "salak", "sapodilla", "tamarillo", "tayberry",
            "whitecurrant",
            "bittermelon", "breadfruit", "calabash", "canistel", "chayote", "miraclefruit", "pomelo", "pricklypear",
            "pulasan", "rambutan",
            "redcurrant", "santol", "tamarind", "velvetapple", "whiteberry", "yellowberry", "ziziphus", "acerola",
            "cempedak", "cupuaçu",
            "honeyberry", "jabuticaba", "mangosteen", "medlar", "pitanga", "rosehip", "rowanberry", "safou", "salal",
            "saskatoonberry"
        };

        using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        using (var writer = new StreamWriter(fs))
        {
            long currentSize = 0;

            while (currentSize < targetSize)
            {
                var word = wordPool[random.Next(wordPool.Length)];
                var separator = separators[random.Next(separators.Length)];
                var output = word + separator;

                writer.Write(output);
                currentSize += Encoding.UTF8.GetByteCount(output);
            }
        }

        Console.WriteLine("File generated successfully.");
    }
}