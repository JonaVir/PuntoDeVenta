using BospOne.PuntoDeVenta.Application;
using BospOne.PuntoDeVenta.Domain.Entities;
using BospOne.PuntoDeVenta.Persistence;
using BospOne.PuntoDeVenta.WebApi.Extensions;
using BospOne.PuntoDeVenta.WebApi.Middleware;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddIdentityService(builder.Configuration);
builder.Services.AddPoliciesServices();
builder.Services.AddHttpContextAccessor();
// builder.Services.AddControllers();

IEdmModel GetEdmModel()
{
    var edm = new ODataConventionModelBuilder();

    edm.EntitySet<Product>("Products");
    edm.EntitySet<Supplier>("Suppliers");
    return edm.GetEdmModel();
}

builder.Services.AddControllers()
    .AddOData(opt => opt
    .Select()
    .Filter()
    .OrderBy()
    .Expand()
    .Count()
    .SetMaxTop(100)
    .AddRouteComponents("odata",GetEdmModel())
);

builder.Services.AddSwaggerDocumentation();
builder.Services.AddCors(o => o.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*")
    .AllowAnyMethod()
    .AllowAnyHeader();
}));

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.useSwaggerDocumentation();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("corsapp");

await app.SeedDataAuthentication();

app.MapControllers();
app.Run();





