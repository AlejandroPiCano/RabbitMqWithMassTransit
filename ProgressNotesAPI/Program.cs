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
    x.AddConsumer<ObservationConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["ServicesBus:Server"], "/", h =>
        {
            h.Username(builder.Configuration["ServicesBus:UserName"]);
            h.Password(builder.Configuration["ServicesBus:Password"]);
            //h.Username("guest");
            //h.Password("guest");
        });

        ////Configure Queue endopint
        //cfg.ReceiveEndpoint(builder.Configuration["ServicesBus:Queue"], c => {
        //    c.ConfigureConsumer<ObservationConsumer>(context);
        //});

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
