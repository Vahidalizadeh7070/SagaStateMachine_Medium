using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using SagaService.Models;
using SagaStateMachine;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DbConnection");

// Register SagaContext
builder.Services.AddDbContextPool<AppDbContext>(db => db.UseSqlServer(connectionString));
builder.Services.AddControllers();


builder.Services.AddMassTransit(cfg =>
{
    cfg.AddBus(provider=> MessageBrokers.RabbitMQ.ConfigureBus(provider));
    cfg.AddSagaStateMachine<TicketStateMachine, TicketStateData>()
        .EntityFrameworkRepository(r =>
        {
            r.ConcurrencyMode = ConcurrencyMode.Pessimistic; // or use Optimistic, which requires RowVersion

            r.ExistingDbContext<AppDbContext>();
        });
});


var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
