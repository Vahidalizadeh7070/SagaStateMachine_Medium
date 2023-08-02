using Events.TicketEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaStateMachine
{
    // This class is responsible to pass the context messages to the another event which here is IGenerateTicketEvent
    public class GenerateTicketEvent : IGenerateTicketEvent
    {
        private readonly TicketStateData _ticketStateData;

        public GenerateTicketEvent(TicketStateData ticketStateData)
        {
            _ticketStateData = ticketStateData;
        }
        public Guid TicketId => _ticketStateData.TicketId;

        public string Title => _ticketStateData.Title;

        public string Email => _ticketStateData.Email;

        public DateTime RequireDate => _ticketStateData.TicketCreatedDate;

        public int Age => _ticketStateData.Age;

        public string Location => _ticketStateData.Location;

        public string TicketNumber => _ticketStateData.TicketNumber;
    }
}
