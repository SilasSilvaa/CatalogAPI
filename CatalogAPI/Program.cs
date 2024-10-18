using CatalogAPI.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mySqlConnection = builder.Configuration.GetConnectionString("DafaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
                        options.UseMySql(mySqlConnection, 
                        ServerVersion.AutoDetect(mySqlConnection)));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
