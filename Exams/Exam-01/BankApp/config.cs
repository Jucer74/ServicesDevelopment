using Microsoft.Extensions.Configuration;

public static class Config
{
    private static IConfigurationRoot configuration;
    static Config()
    {
        configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) 
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
    }

    public static string GetApiUrl()
    {
        return configuration["ApiUrl"]!;
    }
}