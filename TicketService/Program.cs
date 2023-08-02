using MassTransit;
using Microsoft.EntityFrameworkCore;
using TicketService.Consumers;
using TicketService.Models;
using TicketService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Connection string
var connectionString = builder.Configuration.GetConnectionString("DbConnection");
builder.Services.AddDbContextPool<AppDbContext>(db => db.UseSqlServer(connectionString));

// Registe MassTransit
builder.Services.AddMassTransit(cfg =>
{
    cfg.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
    {
        cfg.ReceiveEndpoint(MessageBrokers.RabbitMQQueues.SagaBusQueue, ep =>
        {
            ep.PrefetchCount = 10;
            // Get Consumer
            ep.ConfigureConsumer<GetValueConsumer>(provider);
            // Cancel Consumer
            ep.ConfigureConsumer<GenerateTicketCancelConsumer>(provider);
        });
    }));

    cfg.AddConsumer<GetValueConsumer>();
    cfg.AddConsumer<GenerateTicketCancelConsumer>();
});

// Register Ticket Service
builder.Services.AddScoped<ITicketServices, TicketServices>();

// Register AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
