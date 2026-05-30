using BospOne.PuntoDeVenta.Application.Core;
using BospOne.PuntoDeVenta.Domain.Entities;
using BospOne.PuntoDeVenta.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BospOne.PuntoDeVenta.Application.Suppliers.Create
{
    public class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommandRequest, Result<Guid>>
    {
        #region Fields and properties
        private readonly BospOneDbContext Context;
        #endregion

        #region Builders
        public CreateSupplierCommandHandler(BospOneDbContext context)
        {
            Context = context;
        }
        #endregion

        #region Methods
        public async Task<Result<Guid>> Handle(CreateSupplierCommandRequest request, CancellationToken cancellationToken)
        {
            var LastSupplierCode = await Context.Suppliers!.Select(x => x.Code).Order().LastOrDefaultAsync();
            string code = "";

            if (LastSupplierCode is null)
                code = GenerateSupplierCode("SP00000");
            else
                code = GenerateSupplierCode(LastSupplierCode);

            var supplierID = Guid.NewGuid();

            var supplier = new Supplier()
            {
                Id = supplierID,
                Code = code,
                TradeName = request.supplier!.TradeName,
                Phone = request.supplier.Phone,
                Email = request.supplier.Email,
                CreateAt = DateTimeOffset.UtcNow.DateTime,
                CreatedBy = request.UserID,
                IsActive = true
            };

            Context.Add(supplier);

            if (await Context.SaveChangesAsync(cancellationToken) > 0)
                return Result<Guid>.Success(supplierID);
            else
                return Result<Guid>.Failure("Error al registrar proveedor");
        }

        private string GenerateSupplierCode(string lastCode)
        {
            string prefix = lastCode.Substring(2, 5);
            int index = 0;
            int chunkSize = 5;

            foreach(var num in prefix)
            {
                if (num != '0')
                    break;
                index++;
            }

            int nextNum;
            string lastNum;

            if (index == prefix.Length)
            {
                index -= 1;
                lastNum = prefix.Substring(index);
            }
            else
                lastNum = prefix.Substring(index);

            nextNum = int.Parse(lastNum) + 1;

            prefix = "SP";

            chunkSize -= nextNum.ToString().Length;

            for(var i = 0; i < chunkSize; i++)
            {
                prefix += "0";
            }

            prefix += nextNum.ToString();

            return prefix;
        }
        #endregion
    }
}
