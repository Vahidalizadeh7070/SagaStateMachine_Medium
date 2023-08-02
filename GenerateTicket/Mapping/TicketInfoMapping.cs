using AutoMapper;
using Events.TicketEvents;
using GenerateTicket.Models;

namespace GenerateTicket.Mapping
{
    public class TicketInfoMapping : Profile
    {
        public TicketInfoMapping()
        {
            CreateMap<IGenerateTicketEvent, TicketInfo>();
        }
    }
}
