
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using ProductService.Api.Extensions;
using ProductService.Api.Middleware;
using ProductService.Infrastructure.Context;
using System.Configuration;

namespace ProductService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}