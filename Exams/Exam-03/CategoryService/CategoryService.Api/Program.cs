
using CategoryService.Api.Extensions;
using CategoryService.Api.Middleware;
using CategoryService.Infrastructure.Context;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using ProductService.Api;

namespace CategoryService.Api
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