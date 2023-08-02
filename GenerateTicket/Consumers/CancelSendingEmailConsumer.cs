using Events.SendEmailEvents;
using Events.TicketEvents;
using GenerateTicket.Services;
using MassTransit;

namespace GenerateTicket.Consumers
{
    public class CancelSendingEmailConsumer : IConsumer<ICancelSendEmailEvent>
    {
        private readonly ITicketInfoService _ticketInfoService;
        private readonly ILogger<CancelSendingEmailConsumer> _logger;

        public CancelSendingEmailConsumer(ITicketInfoService ticketInfoService, ILogger<CancelSendingEmailConsumer> logger)
        {
            _ticketInfoService = ticketInfoService;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<ICancelSendEmailEvent> context)
        {
            var data = context.Message;
            if(data is not null)
            {
                var res = _ticketInfoService.RemoveTicketInfo(data.TicketId.ToString());
                if(res is true)
                {
                    await context.Publish<ICancelGenerateTicketEvent>(new
                    {
                        TicketId = data.TicketId,
                        Title = data.Title,
                        Email = data.Email,
                        RequireDate = data.RequireDate,
                        Age = data.Age,
                        Location = data.Location
                    });
                    _logger.LogInformation("The message has been sent to the ICancelGenerateTicketEvent in the TicketService");
                }

                else
                {
                    _logger.LogInformation("Failed!!!");

                }
            }
        }
    }
}
