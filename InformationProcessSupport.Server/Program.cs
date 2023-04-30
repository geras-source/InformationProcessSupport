using InformationProcessSupport.Core;
using InformationProcessSupport.Data;

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();