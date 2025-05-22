using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using ServiceOrderApplication.Data;
using ServiceOrderApplication.Services;
using Serilog;
using ServiceOrderApplication.Profiles;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Host.UseSerilog();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    var serverVersion = ServerVersion.AutoDetect(connectionString);

    options.UseMySql(connectionString, serverVersion);
});
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<IClientService, ClientService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()){
    app.MapScalarApiReference();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();
app.UseAuthorization();
app.Run();
