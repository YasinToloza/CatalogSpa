using CatalogSpa.API.Models;
using CatalogSpa.API.Repositories;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Mapear clase MongoDBSettings con las entradas del AppSettings.Json
builder.Services.Configure<MongoDBSettings>(
builder.Configuration.GetSection("MongoDBSettings")
);

// La interfaz IMongoDatabase representa una base de datos de MongoDB
// Implementar la interfaz
builder.Services.AddSingleton<IMongoDatabase>(option =>
{
    var settings = builder.Configuration.GetSection("MongoDBSettings").Get<MongoDBSettings>();
    var clientDB = new MongoClient(settings.ConnectionString);
    return clientDB.GetDatabase(settings.DatabaseName);
});

// Inyeccion de dependencias para la IProductRepository en ProductRepository
builder.Services.AddScoped<IProductRepository, ProductRepository>();


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
