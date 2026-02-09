using System.Text.Json.Serialization;
using eCommerce.API.Middlewares;
using eCommerce.Core;
using eCommerce.Infrastructure;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.IdentityModel.Tokens;


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

     
builder.Services.AddAutoMapper(cfg =>
{}, typeof(eCommerce.Core.Mappers.ApplicationUserMappingProfile).Assembly);

//fluent validation
builder.Services.AddFluentValidationAutoValidation();

//add api explore services
builder.Services.AddEndpointsApiExplorer();

//add swagger genneration services
builder.Services.AddSwaggerGen();

//add cors relatedservces
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();


app.UseExceptionHandlingMiddleware();

app.UseRouting();
app.UseSwagger();//add endpoint that can serve the swagger.json file

app.UseSwaggerUI();


app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

//controller routes
app.MapControllers();
app.Run();
