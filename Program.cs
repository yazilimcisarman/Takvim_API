using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Takvim_API.Data.MongoDbContext;
using Takvim_API.Models;
using Takvim_API.Repositories.Abstract;
using Takvim_API.Repositories.Concrete;
using Takvim_API.Services.Abstarct;
using Takvim_API.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection(nameof(MongoDBSettings)));

builder.Services.AddSingleton<MongoDbContext>(serviceProvider =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    var client = new MongoClient(settings.ConnectionString);
    var database = client.GetDatabase(settings.DatabaseName);
    return new MongoDbContext(database);
});
//Services - Repository
builder.Services.AddScoped<ITakvimRepository, TakvimRepository>();
builder.Services.AddScoped<ITakvimServices, TakvimServices>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
