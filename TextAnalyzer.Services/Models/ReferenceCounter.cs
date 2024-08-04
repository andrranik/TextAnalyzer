namespace TextAnalyzer.Services.Models;

public class ReferenceCounter()
{
    public volatile int Result = 0;

    public override string ToString()
    {
        return Result.ToString();
    }
}