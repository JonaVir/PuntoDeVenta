using AutoMapper;
using BospOne.PuntoDeVenta.Application.Products.Read;
using BospOne.PuntoDeVenta.Application.Suppliers.Read;
using BospOne.PuntoDeVenta.Application.Users.GetUsers;
using BospOne.PuntoDeVenta.Domain.Entities;
using BospOne.PuntoDeVenta.Persistence.Models;

namespace BospOne.PuntoDeVenta.Application.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Supplier, SupplierResponse>();
            CreateMap<Product, ProductResponse>();
            CreateMap<AppUser, UserResponse>();
        }
    }
}
