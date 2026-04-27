using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BospOne.PuntoDeVenta.Persistence
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<BospOneDbContext>(opt =>
            {
                opt.LogTo(Console.WriteLine, new[] {
                    DbLoggerCategory.Database.Command.Name }, LogLevel.Information).EnableSensitiveDataLogging();
                opt.UseSqlite(configuration.GetConnectionString("BosponeDataBase"));
            });
            return service;
        }
    }
}
