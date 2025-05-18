using Microsoft.EntityFrameworkCore;
using MSProductAPI.Data;
using MSProductAPI.Repositories.Interfaces;
using MSProductAPI.Repositories;
using MSProductAPI.Services.Interfaces;
using MSProductAPI.Services;
using MSProductAPI.Messaging.Interfaces;
using MSProductAPI.Messaging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ProductDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProductApiConnectionString"));
});


builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddSingleton<IRabbitMQPublisher, RabbitMQPublisher>();


builder.Services.AddControllers();
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
