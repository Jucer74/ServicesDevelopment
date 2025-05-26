using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MoneyBankService.Infrastructure.Context;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        // Puedes construir la configuración para leer un appsettings.Design.json (o usar uno existente)
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        // Aquí defines una cadena de conexión que funcione localmente.
        // Suponiendo que en tu docker-compose MySQL expone el contenedor en el puerto 3307 del host.
        var connectionString = "Server=127.0.0.1;Port=3307;Database=moneybankdb;User=root;Password=juan234ego;";

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseMySql(
            connectionString,
            new MySqlServerVersion(new Version(8, 0, 42)),
            mySqlOptions => mySqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)
        );

        return new AppDbContext(optionsBuilder.Options);
    }
}
