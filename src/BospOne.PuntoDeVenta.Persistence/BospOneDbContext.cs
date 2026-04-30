using BospOne.PuntoDeVenta.Domain.Entities;
using BospOne.PuntoDeVenta.Persistence.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BospOne.PuntoDeVenta.Persistence
{
    public class BospOneDbContext : IdentityDbContext<AppUser>
    {
        #region Fields and properties
        public DbSet<Supplier>? suppliers { get; set; }
        public DbSet<Product>? products { get; set; }
        public DbSet<Receipt> Receipts { get; set; }

        private static readonly string adminId = "6f1b8c7e-2d7a-4c3e-9c2f-1a9c5f4b8e21";
        private static readonly string userId = "b3a4d9f2-8e6c-4a1c-9a5b-2d7f8c6e4b19";
        #endregion

        #region Builders
        public BospOneDbContext(DbContextOptions<BospOneDbContext> options) : base(options)
        {

        }
        #endregion

        #region Methods
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Configuración de nombres de tablas
            builder.Entity<Supplier>().ToTable("Suppliers");
            builder.Entity<Product>().ToTable("Products");
            builder.Entity<Receipt>().ToTable("Receipts");
            builder.Entity<ReceiptEvidence>().ToTable("ReceiptEvidences");
            builder.Entity<ProductSupplier>().ToTable("ProductsSuppliers");

            //Relaciones entre tablas

            builder.Entity<Supplier>()                
                .HasMany(r => r.Receipts)
                .WithOne(s => s.Supplier)
                .HasForeignKey(s => s.SupplierID);

            builder.Entity<Supplier>()
                .HasMany(p => p.Products)
                .WithMany(p => p.Suppliers)
                .UsingEntity<ProductSupplier>(
                    j => j
                        .HasOne(ps => ps.Product)
                        .WithMany(p => p.ProductSuppliers)
                        .HasForeignKey(ps => ps.ProductID),
                    j => j
                        .HasOne(ps => ps.Supplier)
                        .WithMany(s => s.ProductSuppliers)
                        .HasForeignKey(ps => ps.SupplierID),
                    j =>
                    {
                        j.HasKey(t => new { t.SupplierID, t.ProductID });
                    }
                );

            builder.Entity<Product>()
                .HasMany(r => r.Receipts)
                .WithOne(p => p.Product)
                .HasForeignKey(p => p.ProductID);

            builder.Entity<Product>()
                .HasMany(p => p.Suppliers)
                .WithMany(p => p.Products)
                .UsingEntity<ProductSupplier>(
                    j => j
                        .HasOne(ps => ps.Supplier)
                        .WithMany(s => s.ProductSuppliers)
                        .HasForeignKey(ps => ps.SupplierID),
                    j => j
                        .HasOne(ps => ps.Product)
                        .WithMany(p => p.ProductSuppliers)
                        .HasForeignKey(ps => ps.ProductID),
                    j =>
                    {
                        j.HasKey(t => new { t.SupplierID, t.ProductID });
                    }
                );

            builder.Entity<Receipt>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UserID).IsRequired();
                entity.HasOne<AppUser>()
                    .WithMany()
                    .HasForeignKey(e => e.UserID);
            });
                
            LoadSecurityData(builder);
        }

        private void LoadSecurityData(ModelBuilder modelBuilder)
        {         
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = adminId,
                    Name = CustomRoles.ADMIN,
                    NormalizedName = CustomRoles.ADMIN
                }
            );

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = userId,
                    Name = CustomRoles.USER,
                    NormalizedName = CustomRoles.USER
                }
            );

            modelBuilder.Entity<IdentityRoleClaim<string>>().HasData(

                new IdentityRoleClaim<string>
                {
                    Id = 1,
                    ClaimType = CustomClaims.POLICIES,
                    ClaimValue = PolicyMaster.PRODUCT_READ,
                    RoleId = adminId
                },
                new IdentityRoleClaim<string>
                {
                    Id = 2,
                    ClaimType = CustomClaims.POLICIES,
                    ClaimValue = PolicyMaster.PRODUCT_WRITE,
                    RoleId = adminId
                },
                new IdentityRoleClaim<string>
                {
                    Id = 3,
                    ClaimType = CustomClaims.POLICIES,
                    ClaimValue = PolicyMaster.PRODUCT_UPDATE,
                    RoleId = adminId
                },
                new IdentityRoleClaim<string>
                {
                    Id = 4,
                    ClaimType = CustomClaims.POLICIES,
                    ClaimValue = PolicyMaster.PRODUCT_DELETE,
                    RoleId = adminId
                },
                new IdentityRoleClaim<string>
                {
                    Id = 5,
                    ClaimType = CustomClaims.POLICIES,
                    ClaimValue = PolicyMaster.SUPPLIER_READ,
                    RoleId = adminId
                },
                new IdentityRoleClaim<string>
                {
                    Id = 6,
                    ClaimType = CustomClaims.POLICIES,
                    ClaimValue = PolicyMaster.SUPPLIER_WRITE,
                    RoleId = adminId
                },
                new IdentityRoleClaim<string>
                {
                    Id = 7,
                    ClaimType = CustomClaims.POLICIES,
                    ClaimValue = PolicyMaster.SUPPLIER_UPDATE,
                    RoleId = adminId
                },
                new IdentityRoleClaim<string>
                {
                    Id = 8,
                    ClaimType = CustomClaims.POLICIES,
                    ClaimValue = PolicyMaster.SUPPLIER_DELETE,
                    RoleId = adminId
                },
                new IdentityRoleClaim<string>
                {
                    Id = 9,
                    ClaimType = CustomClaims.POLICIES,
                    ClaimValue = PolicyMaster.RECEIPT_READ,
                    RoleId = adminId
                },
                new IdentityRoleClaim<string>
                {
                    Id = 10,
                    ClaimType = CustomClaims.POLICIES,
                    ClaimValue = PolicyMaster.RECEIPT_WRITE,
                    RoleId = adminId
                }, new IdentityRoleClaim<string>
                {
                    Id = 11,
                    ClaimType = CustomClaims.POLICIES,
                    ClaimValue = PolicyMaster.RECEIPT_READ,
                    RoleId = userId
                },
                new IdentityRoleClaim<string>
                {
                    Id = 12,
                    ClaimType = CustomClaims.POLICIES,
                    ClaimValue = PolicyMaster.RECEIPT_WRITE,
                    RoleId = userId
                }
            );
        }
        #endregion
    }
}
