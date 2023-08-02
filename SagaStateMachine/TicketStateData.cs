using Automatonymous;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaStateMachine
{
    public class TicketStateData : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; }
        public DateTime TicketCreatedDate { get; set; }
        public DateTime TicketCancelDate { get; set; }
        public Guid TicketId { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public int Age { get; set; }
        public  string TicketNumber { get; set; }
    }
}
