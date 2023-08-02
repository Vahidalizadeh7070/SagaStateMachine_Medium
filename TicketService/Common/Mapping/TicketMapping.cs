using AutoMapper;
using TicketService.DTO;
using TicketService.Models;

namespace TicketService.Common.Mapping
{
    public class TicketMapping : Profile
    {
        public TicketMapping()
        {
            CreateMap<AddTicketDTO, Ticket>();
            CreateMap<Ticket, ResponseTicketDTO>();
        }
    }
}
