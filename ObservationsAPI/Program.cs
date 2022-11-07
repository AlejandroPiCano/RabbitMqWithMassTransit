using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System;
using ObservationsAPI.Infrastructure;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ObservationsDbContext>
   (o => o.UseSqlServer(builder.Configuration.GetConnectionString("DBConnectionLocal")));


#region MassTransit
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["ServicesBus:Server"], "/", h =>
        {
            h.Username(builder.Configuration["ServicesBus:UserName"]);
            h.Password(builder.Configuration["ServicesBus:Password"]);
            //h.Username("guest");
            //h.Password("guest");
        });

        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddMassTransitHostedService(true);
#endregion


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
