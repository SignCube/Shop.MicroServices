using Basket.API.Repositories.Abstraction;
using Basket.API.Repositories.Implemention;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//depencency injection
builder.Services.AddStackExchangeRedisCache(options =>

    options.Configuration = builder.Configuration.GetValue<string>("CacheSetting:ConnectionString")
);
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
//dependency injection
builder.Services.AddControllers();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c => c.SwaggerDoc("v1", new OpenApiInfo() { Title = "Basket.API", Version = "v1" })

);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket.API v1"));
}

app.UseAuthorization();

app.MapControllers();

app.Run();
