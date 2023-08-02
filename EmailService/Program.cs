using EmailService.Consumers;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Register MassTransit 
builder.Services.AddMassTransit(cfg =>
{
    cfg.AddBus(provider => MessageBrokers.RabbitMQ.ConfigureBus(provider));
    cfg.AddConsumer<SendEmailConsumer>();
});

var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
