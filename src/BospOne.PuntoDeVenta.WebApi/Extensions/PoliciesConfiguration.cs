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

                opt.AddPolicy(PolicyMaster.PRODUCT_WRITE, policy =>
                    policy.RequireAssertion(
                        context => context.User.HasClaim(
                            c => c.Type == CustomClaims.POLICIES && c.Value == PolicyMaster.PRODUCT_WRITE
                        )
                    )
                );

                opt.AddPolicy(PolicyMaster.PRODUCT_UPDATE, policy =>
                    policy.RequireAssertion(
                        context => context.User.HasClaim(
                            c => c.Type == CustomClaims.POLICIES && c.Value == PolicyMaster.PRODUCT_UPDATE
                        )
                    )
                );

                opt.AddPolicy(PolicyMaster.PRODUCT_DELETE, policy =>
                    policy.RequireAssertion(
                        context => context.User.HasClaim(
                            c => c.Type == CustomClaims.POLICIES && c.Value == PolicyMaster.PRODUCT_DELETE
                        )
                    )
                );

                opt.AddPolicy(PolicyMaster.SUPPLIER_READ, policy =>
                    policy.RequireAssertion(
                        context => context.User.HasClaim(
                            c => c.Type == CustomClaims.POLICIES && c.Value == PolicyMaster.SUPPLIER_READ
                        )
                    )
                );

                opt.AddPolicy(PolicyMaster.SUPPLIER_WRITE, policy =>
                    policy.RequireAssertion(
                        context => context.User.HasClaim(
                            c => c.Type == CustomClaims.POLICIES && c.Value == PolicyMaster.SUPPLIER_WRITE
                        )
                    )
                );

                opt.AddPolicy(PolicyMaster.SUPPLIER_UPDATE, policy =>
                    policy.RequireAssertion(
                        context => context.User.HasClaim(
                            c => c.Type == CustomClaims.POLICIES && c.Value == PolicyMaster.SUPPLIER_UPDATE
                        )
                    )
                );

                opt.AddPolicy(PolicyMaster.SUPPLIER_DELETE, policy =>
                    policy.RequireAssertion(
                        context => context.User.HasClaim(
                            c => c.Type == CustomClaims.POLICIES && c.Value == PolicyMaster.SUPPLIER_DELETE
                        )
                    )
                );

                opt.AddPolicy(PolicyMaster.RECEIPT_READ, policy =>
                    policy.RequireAssertion(
                        context => context.User.HasClaim(
                            c => c.Type == CustomClaims.POLICIES && c.Value == PolicyMaster.RECEIPT_READ
                        )
                    )
                );

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
