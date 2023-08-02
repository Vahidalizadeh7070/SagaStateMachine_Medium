using TicketService.Models;

namespace TicketService.Services
{
    public class TicketServices : ITicketServices
    {
        private readonly AppDbContext _dbContext;

        public TicketServices(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Ticket> AddTicket(Ticket ticket)
        {
            if(ticket is not null)
            {
                await _dbContext.Ticket.AddAsync(ticket);
                await _dbContext.SaveChangesAsync();
            }
            return ticket;
        }

        public bool DeleteTicket(string TicketId)
        {
            var ticketObj = _dbContext.Ticket.FirstOrDefault(t=>t.TicketId == TicketId);
            if(ticketObj is not null)
            {
                _dbContext.Ticket.Remove(ticketObj);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
