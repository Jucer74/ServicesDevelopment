using Microsoft.Extensions.Configuration;

public static class ConfigManager
{
    private static IConfigurationRoot configuration;

    

    static ConfigManager()
    {
        configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // Asegura la ubicación correcta
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
    }

    public static string GetApiUrl()
    {
        return configuration["ApiUrl"];
    }
}