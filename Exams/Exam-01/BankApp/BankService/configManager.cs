using Microsoft.Extensions.Configuration;
using System.IO;
public static class ConfigManager
{
    private static IConfiguration _config;

    static ConfigManager()
    {
        _config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
    }
    public static string GetApiUrl()
    {
        return _config["ApiSettings:MainUrl"] ?? throw new Exception("API URL not found in configuration");
    }
}
