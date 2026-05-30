using BospOne.PuntoDeVenta.Application.Core;
using MediatR;
using Microsoft.AspNetCore.Identity;
using BospOne.PuntoDeVenta.Persistence.Models;

namespace BospOne.PuntoDeVenta.Application.Users.UpdateUser
{
    public class ChangeRoleCommandHandler : IRequestHandler<UpdateUserCommandRequest, Result<bool>>
    {
        #region Fields and properties
        private readonly UserManager<AppUser> UserManager;
        private readonly RoleManager<IdentityRole> RoleManager;
        #endregion

        #region Builders
        public ChangeRoleCommandHandler(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }
        #endregion

        #region Methods
        public async Task<Result<bool>> Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
        {
            var userID = request.UserId.ToString();
            var user = await UserManager.FindByIdAsync(userID);

            if (user is null)
                return Result<bool>.Failure("Usuario no encontrado");

            if (!await RoleManager.RoleExistsAsync(request.user.NewRole!))
                return Result<bool>.Failure("No existe el rol especificado");

            //Modificación de información basica del usuario
            user.NombreCompleto = request.user.NombreCompleto;
            user.UserName = request.user.UserName;
            user.Cargo = request.user.Cargo;

            //Modificación de numero telefonico
            if(!string.IsNullOrWhiteSpace(request.user.PhoneNumber))
            {
                var PhoneNumberToken = await UserManager.GenerateChangePhoneNumberTokenAsync(user, request.user.PhoneNumber!);
                var PhoneNumberResult = await UserManager.ChangePhoneNumberAsync(user, request.user.PhoneNumber!, PhoneNumberToken);

                if (!PhoneNumberResult.Succeeded)
                    return Result<bool>.Failure("Error al actualizar el numero telefonico");
            }

            //modificación de email
            if(!string.IsNullOrEmpty(request.user.Email))
            {
                var emailToken = await UserManager.GenerateChangeEmailTokenAsync(user, request.user.Email!);
                var emailResult = await UserManager.ChangeEmailAsync(user, request.user.Email!, emailToken);

                if (!emailResult.Succeeded)
                    return Result<bool>.Failure("No se pudo actualizar el email del usuario");
            }

            //Modificación de password
            if(!string.IsNullOrWhiteSpace(request.user.NewPassword))
            {
                var token = await UserManager.GeneratePasswordResetTokenAsync(user);
                var passwordResult = await UserManager.ResetPasswordAsync(user,token,request.user.NewPassword!); 

                if(!passwordResult.Succeeded)
                {
                    var errors = string.Join(",", passwordResult.Errors.Select(e => e.Description));
                    return Result<bool>.Failure(errors);
                }
            }

            if(!string.IsNullOrWhiteSpace(request.user.NewRole))
            {
                if (!await RoleManager.RoleExistsAsync(request.user.NewRole))
                    return Result<bool>.Failure("El rol especificado no existe");

                var currentRoles = await UserManager.GetRolesAsync(user);

                if(!currentRoles.Contains(request.user.NewRole))
                {
                    var removeResult = await UserManager.RemoveFromRolesAsync(user, currentRoles);

                    if (!removeResult.Succeeded)
                        return Result<bool>.Failure("Error al remover roles");

                    var addResult = await UserManager.AddToRoleAsync(user,request.user.NewRole);

                    if (!addResult.Succeeded)
                        return Result<bool>.Failure("Error al asignar nuevo rol");
                }
            }

            var UpdateResult = await UserManager.UpdateAsync(user);

            if (!UpdateResult.Succeeded)
            {
                var errors = string.Join(",", UpdateResult.Errors.Select(e => e.Description));
                return Result<bool>.Failure(errors);
            }

            return Result<bool>.Success(true);
        }
        
        #endregion
    }
}
