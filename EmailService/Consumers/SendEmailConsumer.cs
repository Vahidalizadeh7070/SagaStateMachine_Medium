using Events.SendEmailEvents;
using MassTransit;

namespace EmailService.Consumers
{
    public class SendEmailConsumer : IConsumer<ISendEmailEvent>
    {
        private readonly ILogger<SendEmailConsumer> _logger;

        public SendEmailConsumer(ILogger<SendEmailConsumer> logger)
        {
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<ISendEmailEvent> context)
        {
            var data = context.Message;
            if (data is not null)
            {
                if (data.Location == "London")
                {
                    await context.Publish<ICancelSendEmailEvent>(new{
                        TicketId = data.TicketId,
                        Title = data.Title,
                        Email = data.Email,
                        RequireDate = data.RequireDate,
                        Age = data.Age,
                        Location = data.Location
                    });
                    _logger.LogInformation("The location is unavailable");
                }
                else
                {
                    _logger.LogInformation("The message has been received ");
                }
            }
        }
    }
}
