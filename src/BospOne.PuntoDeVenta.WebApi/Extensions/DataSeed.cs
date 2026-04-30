using BospOne.PuntoDeVenta.Domain.Entities;
using BospOne.PuntoDeVenta.Persistence;
using BospOne.PuntoDeVenta.Persistence.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BospOne.PuntoDeVenta.WebApi.Extensions
{
    public static class DataSeed
    {
        public static async Task SeedDataAuthentication(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var service = scope.ServiceProvider;
            var loggerFactory = service.GetRequiredService<ILoggerFactory>();

            try
            {
                var context = service.GetRequiredService<BospOneDbContext>();
                await context.Database.MigrateAsync();

                var userManager = service.GetRequiredService<UserManager<AppUser>>();

                if(!userManager.Users.Any())
                {
                    var userAdmin = new AppUser
                    {
                        NombreCompleto = "Eduardo Ulises Hernandez Alvarez",
                        UserName = "EduAlv",
                        Cargo = "Administrador",
                        Email = "eduardo.alv@example.com"
                    };
                    await userManager.CreateAsync(userAdmin, "LaTiendita$123!");
                    await userManager.AddToRoleAsync(userAdmin, CustomRoles.ADMIN);
                }

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<BospOneDbContext>();
                logger.LogError(ex.Message);
            }
        }
    }
}
