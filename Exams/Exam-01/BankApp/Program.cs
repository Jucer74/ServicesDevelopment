using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using UI;

namespace BankApp
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var httpClient = new HttpClient();
            var baseUrl = configuration["BankApiSettings:BaseUrl"];
            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new InvalidOperationException("BaseUrl no está configurado en appsettings.json.");
            }
            var bankService = new BL.BankService(httpClient, baseUrl);

            var uiProgram = new UI.Program();
            await uiProgram.Run(bankService);
        }
    }
}