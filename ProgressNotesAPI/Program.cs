using MassTransit;
using Microsoft.EntityFrameworkCore;
using ProgressNotesAPI;
using ProgressNotesAPI.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProgressNotesDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DBConnectionLocal")));

#region MassTransit
builder.Services.AddMassTransit(x =>
{
    // elided...
    x.AddConsumer<ObservationConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ConfigureEndpoints(context);
    });
});

//builder.Services.AddHostedService<Worker>();

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
app.MapControllers();

app.Run();
