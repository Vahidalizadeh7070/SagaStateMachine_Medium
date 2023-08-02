using Events.TicketEvents;
using MassTransit;
using Microsoft.AspNetCore.Mvc.Formatters;
using TicketService.Services;

namespace TicketService.Consumers
{
    public class GenerateTicketCancelConsumer : IConsumer<ICancelGenerateTicketEvent>
    {
        private readonly ITicketServices _ticketServices;
        private readonly ILogger<GenerateTicketCancelConsumer> _logger;

        public GenerateTicketCancelConsumer(ITicketServices ticketServices, ILogger<GenerateTicketCancelConsumer> logger)
        {
            _ticketServices = ticketServices;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<ICancelGenerateTicketEvent> context)
        {
            var data = context.Message;
            if(data is not null)
            {
                var res = _ticketServices.DeleteTicket(data.TicketId.ToString());
                if(res is true)
                {
                    _logger.LogInformation("The Ticket has been removed successufully");
                }
                else
                {
                    _logger.LogInformation("Failed!!!");
                }
            }
        }
    }
}
