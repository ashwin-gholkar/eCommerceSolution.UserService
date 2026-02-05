using System.Text.Json.Serialization;
using eCommerce.API.Middlewares;
using eCommerce.Core;
using eCommerce.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

//Add infrastructure services
builder.Services.AddInfrastructure();
builder.Services.AddCore();


//Add controllers to the service collection
builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(
        new JsonStringEnumConverter());
});
//builder.Services.AddAutoMapper(typeof(eCommerce.Core.Mappers.ApplicationUserMappingProfile).Assembly);

var app = builder.Build();


app.UseExceptionHandlingMiddleware();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//controller routes
app.MapControllers();
app.Run();
