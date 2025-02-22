using _1.API.Mapper;
using _2.Domain;
using _3.Data;
using _3.Data.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDiscountData, DiscountMySqlData>();
builder.Services.AddScoped<IDiscountDomain, DiscountDomain>();

builder.Services.AddAutoMapper(typeof(RequestToModel), typeof(ModelToRequest), typeof(ModelToResponse));

var connectionString = builder.Configuration.GetConnectionString("DiscountDB");

builder.Services.AddDbContext<DiscountDBContext>(
    dbContextOptions =>
    {
        dbContextOptions.UseMySql(connectionString,
            ServerVersion.AutoDetect(connectionString)
        );
    });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<DiscountDBContext>())
{
    context.Database.EnsureCreated();
}
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