using FleetManagement.Configuration;
using FleetManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("sqliteConnectionString");

builder.Services.AddTransient<IVehicleService, VehicleService>();

builder.Services.AddDbContext<FleetManagementContext>(options => options.UseSqlite(connectionString));

builder.Services.AddScoped<IFleetManagementContext, FleetManagementContext>();
builder.Services.AddCors(options => { options.AddPolicy(name: "Open", policy => { 
    policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader(); 
    });
});

var app = builder.Build();

if (true) //(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => options.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());

//app.UseMvc();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
