using BospOne.PuntoDeVenta.Domain.Entities;

namespace BospOne.PuntoDeVenta.WebApi.Extensions
{
    public static class PoliciesConfiguration
    {
        public static IServiceCollection AddPoliciesServices(this IServiceCollection services)
        {
            services.AddAuthorization(opt =>
            {
                opt.AddPolicy(PolicyMaster.PRODUCT_READ, policy =>
                    policy.RequireAssertion(
                        context => context.User.HasClaim(
                            c => c.Type == CustomClaims.POLICIES && c.Value == PolicyMaster.PRODUCT_READ
                        )
                    )
                );
            });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy(PolicyMaster.PRODUCT_WRITE, policy =>
                    policy.RequireAssertion(
                        context => context.User.HasClaim(
                            c => c.Type == CustomClaims.POLICIES && c.Value == PolicyMaster.PRODUCT_WRITE
                        )
                    )
                );
            });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy(PolicyMaster.PRODUCT_UPDATE, policy =>
                    policy.RequireAssertion(
                        context => context.User.HasClaim(
                            c => c.Type == CustomClaims.POLICIES && c.Value == PolicyMaster.PRODUCT_UPDATE
                        )
                    )
                );
            });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy(PolicyMaster.PRODUCT_DELETE, policy =>
                    policy.RequireAssertion(
                        context => context.User.HasClaim(
                            c => c.Type == CustomClaims.POLICIES && c.Value == PolicyMaster.PRODUCT_DELETE
                        )
                    )
                );
            });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy(PolicyMaster.SUPPLIER_READ, policy =>
                    policy.RequireAssertion(
                        context => context.User.HasClaim(
                            c => c.Type == CustomClaims.POLICIES && c.Value == PolicyMaster.SUPPLIER_READ
                        )
                    )
                );
            });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy(PolicyMaster.SUPPLIER_WRITE, policy =>
                    policy.RequireAssertion(
                        context => context.User.HasClaim(
                            c => c.Type == CustomClaims.POLICIES && c.Value == PolicyMaster.SUPPLIER_WRITE
                        )
                    )
                );
            });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy(PolicyMaster.SUPPLIER_UPDATE, policy =>
                    policy.RequireAssertion(
                        context => context.User.HasClaim(
                            c => c.Type == CustomClaims.POLICIES && c.Value == PolicyMaster.SUPPLIER_UPDATE
                        )
                    )
                );
            });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy(PolicyMaster.SUPPLIER_DELETE, policy =>
                    policy.RequireAssertion(
                        context => context.User.HasClaim(
                            c => c.Type == CustomClaims.POLICIES && c.Value == PolicyMaster.SUPPLIER_DELETE
                        )
                    )
                );
            });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy(PolicyMaster.RECEIPT_READ, policy =>
                    policy.RequireAssertion(
                        context => context.User.HasClaim(
                            c => c.Type == CustomClaims.POLICIES && c.Value == PolicyMaster.RECEIPT_READ
                        )
                    )
                );
            });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy(PolicyMaster.RECEIPT_WRITE, policy =>
                    policy.RequireAssertion(
                        context => context.User.HasClaim(
                            c => c.Type == CustomClaims.POLICIES && c.Value == PolicyMaster.RECEIPT_WRITE
                        )
                    )
                );
            });

            return services;
        }
    }
}
