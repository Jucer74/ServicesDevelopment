using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace BankApp.Services{
public class ConfigurationService
{
    private readonly IConfiguration _configuration;

    public ConfigurationService()
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
    }

    public string GetConnectionString()
    {
        return _configuration.GetConnectionString("DefaultConnection");
    }

    public string GetApiUrl()
    {
        return _configuration["ApiSettings:BankApiUrl"];
    }
}
}