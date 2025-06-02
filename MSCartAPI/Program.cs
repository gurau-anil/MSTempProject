using Microsoft.EntityFrameworkCore;
using MSCartAPI.Data;
using MSCartAPI.Repositories.Interfaces;
using MSCartAPI.Repositories;
using MSCartAPI.Services.Interfaces;
using MSCartAPI.Services;
using MSCartAPI.Messaging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<CartDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("CartApiConnectionString"));
});


builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddHttpClient<IProductInfoService, ProductInfoService>();

builder.Services.AddScoped<IRabbitMQPublisher, RabbitMQPublisher>();



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
