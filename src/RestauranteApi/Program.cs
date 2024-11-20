using RestauranteApi;
using Microsoft.EntityFrameworkCore;
using RestauranteApi.Data;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {

    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Acatoira Restaurante",
        Description = "Acatoira Restaurante",
        Contact = new OpenApiContact() { Name = "Acatoira Restaurante", Email = "alba.catoira@tokiota.com" }
    });
});

builder.Services.AddDbContext<PedidoDbContext>(options =>
{
    var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    options.UseSqlite($"Data Source={Path.Join(path, "RestauranteApi.db")}");
});

var app = builder.Build();

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetService<PedidoDbContext>();
db?.Database.MigrateAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(setup =>
    {
        setup.SwaggerEndpoint("/swagger/v1/swagger.json", "Acatoira Restaurante");
        setup.DefaultModelsExpandDepth(-1);
    });
}

app.MapGroup("/pedido")
    .MapPedidosApi()
    .WithTags("Pedido Endpoints");


await app.RunAsync();


public partial class Program { }

