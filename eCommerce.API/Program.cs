using eCommerce.Infrastructure;
using eCommerce.Core;
using eCommerce.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

//Add infrastructure services
builder.Services.AddInfrastructure();
builder.Services.AddCore();


//Add controllers to the service collection
builder.Services.AddControllers();

var app = builder.Build();


app.UseExceptionHandlingMiddleware();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//controller routes
app.MapControllers();
app.Run();
