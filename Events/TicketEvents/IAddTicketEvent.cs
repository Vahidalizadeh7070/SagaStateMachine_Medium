using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.TicketEvents
{
    public interface IAddTicketEvent
    {
        public Guid TicketId { get; }
        public string Title { get; }
        public string Email { get; }
        public DateTime RequireDate { get; }
        public int Age { get; }
        public string Location { get; }
        public string TicketNumber { get; }
    }
}
