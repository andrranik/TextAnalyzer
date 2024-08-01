using Microsoft.Extensions.Configuration;

namespace TextAnalyzer.Shell.Utils;

public static class ConfigurationHelper
{
    private static readonly IConfiguration Configuration;

    static ConfigurationHelper()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", true, true);

        Configuration = builder.Build();
    }

    internal static string GetStorageFolderPath()
    {
        return ReturnSettingValueOfThrowException("DirectoryPath");
    }

    internal static int GetMinWordLength()
    {
        return int.Parse(ReturnSettingValueOfThrowException("MinimalWordLength"));
    }

    internal static string GetFileSearchPattern()
    {
        return ReturnSettingValueOfThrowException("FileSearchPattern");
    }

    internal static int GetResultItemsCount()
    {
        return int.Parse(ReturnSettingValueOfThrowException("ResultItemsCount"));
    }

    internal static string GetMode()
    {
        return ReturnSettingValueOfThrowException("Mode");
    }

    private static string ReturnSettingValueOfThrowException(string settingName)
    {
        return Configuration[settingName] ??
               throw new ApplicationException($"The property \"{settingName}\" could not be found.");
    }
}