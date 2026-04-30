using BospOne.PuntoDeVenta.Application.Interfaces;
using BospOne.PuntoDeVenta.Domain.Entities;
using BospOne.PuntoDeVenta.Persistence;
using BospOne.PuntoDeVenta.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BospOne.PuntoDeVenta.Infrastructure.Security
{
    public class TokenService : ITokenService
    {
        #region Fields and properties
        private readonly BospOneDbContext Context;
        private readonly IConfiguration Configuration;
        #endregion

        #region Builders
        public TokenService(BospOneDbContext context, IConfiguration configuration)
        {
            Context = context;
            Configuration = configuration;
        }
        #endregion

        #region Methods
        public async Task<string> CreateToken(AppUser user)
        {
            var policies = await Context.Database.SqlQuery<string>($@"
                SELECT
                    aspr.ClaimValue
                FROM AspNetUsers AS a
                    LEFT JOIN AspNetUserRoles AS ar
                        ON a.id = ar.UserId
                    LEFT JOIN AspNetRoleClaims AS aspr
                        ON ar.RoleId = aspr.RoleId
                WHERE a.Id = {user.Id};
                
            ").ToListAsync();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email!)
            };

            foreach(var policy in policies)
            {
                if(policy is not null)
                {
                    claims.Add(new(CustomClaims.POLICIES, policy));
                }
            }

            var creds = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenKey"]!)), SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        #endregion
    }
}
