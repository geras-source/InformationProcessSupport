using InformationProcessSupport.Core;
using InformationProcessSupport.Data;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);
var config = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("config.json", false, true)
                       .Build();
// Add services to the container.

builder.Services.AddControllers();
builder.Services
    .AddApplicationContext(config)
    .AddRepositories()
    .AddCoreServices();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy =>
    policy
        .WithOrigins("https://localhost:44360", "http://localhost:44360")
        .AllowAnyMethod()
        .WithHeaders(HeaderNames.ContentType)
    );

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();