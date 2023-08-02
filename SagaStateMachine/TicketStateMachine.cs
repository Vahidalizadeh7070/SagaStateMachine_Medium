using Events.SendEmailEvents;
using Events.TicketEvents;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaStateMachine
{
    public class TicketStateMachine : MassTransitStateMachine<TicketStateData>
    {
        // 4 states are going to happen
        public State AddTicket { get; private set; }
        public State CancelTicket { get; private set; }
        public State CancelSendEmail { get; private set; }
        public State SendEmail { get; private set; }

        // 4 events are going to happen

        public Event<IAddTicketEvent> AddTicketEvent { get; private set; }
        public Event<ICancelGenerateTicketEvent> CancelGenerateTicketEvent { get; private set; }
        public Event<ICancelSendEmailEvent> CancelSendEmailEvent { get; private set; }
        public Event<ISendEmailEvent> SendEmailEvent { get; private set; }

        public TicketStateMachine()
        {
            InstanceState(s => s.CurrentState);
            Event(() => AddTicketEvent, a => a.CorrelateById(m => m.Message.TicketId));
            Event(() => CancelGenerateTicketEvent, a => a.CorrelateById(m => m.Message.TicketId));
            Event(() => CancelSendEmailEvent, a => a.CorrelateById(m => m.Message.TicketId));
            Event(() => SendEmailEvent, a => a.CorrelateById(m => m.Message.TicketId));

            // A message comming from ticket service 
            // it could be the initially state
            Initially(
                When(AddTicketEvent).Then(context =>
                {
                    context.Saga.TicketId = context.Message.TicketId;
                    context.Saga.Title = context.Message.Title;
                    context.Saga.Email = context.Saga.Email;
                    context.Saga.TicketNumber = context.Message.TicketNumber;
                    context.Saga.Age = context.Message.Age;
                    context.Saga.Location = context.Saga.Location;
                }).TransitionTo(AddTicket).Publish(context => new GenerateTicketEvent(context.Saga)));

            // During AddTicketEvent some other events might occured 
            During(AddTicket, 
                When(SendEmailEvent)
                .Then(context =>
                {
                    // These values could be different 
                    context.Saga.TicketId = context.Message.TicketId;
                    context.Saga.Title = context.Message.Title;
                    context.Saga.Email = context.Saga.Email;
                    context.Saga.TicketNumber = context.Message.TicketNumber;
                    context.Saga.Age = context.Message.Age;
                    context.Saga.Location = context.Saga.Location;
                }).TransitionTo(SendEmail));

            During(AddTicket,
                When(CancelGenerateTicketEvent)
                .Then(context =>
                {
                    // These values could be different 
                    context.Saga.TicketId = context.Message.TicketId;
                    context.Saga.Title = context.Message.Title;
                    context.Saga.Email = context.Saga.Email;
                    context.Saga.TicketNumber = context.Message.TicketNumber;
                    context.Saga.Age = context.Message.Age;
                    context.Saga.Location = context.Saga.Location;
                }).TransitionTo(CancelTicket));

            // During SendEmailEvent some other events might occured 
            During(SendEmail,
                When(CancelSendEmailEvent)
                .Then(context =>
                {
                    // These values could be different 
                    context.Saga.TicketId = context.Message.TicketId;
                    context.Saga.Title = context.Message.Title;
                    context.Saga.Email = context.Saga.Email;
                    context.Saga.TicketNumber = context.Message.TicketNumber;
                    context.Saga.Age = context.Message.Age;
                    context.Saga.Location = context.Saga.Location;
                }).TransitionTo(CancelSendEmail));
        }

    }
}
