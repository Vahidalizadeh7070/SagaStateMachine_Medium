using AutoMapper;
using AutoMapper.Internal;
using Events.TicketEvents;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketService.DTO;
using TicketService.Models;
using TicketService.Services;

namespace TicketService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketServices _ticketServices;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        public TicketController(ITicketServices ticketServices, IMapper mapper, IBus bus)
        {
            _ticketServices = ticketServices;
            _mapper = mapper;
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddTicketDTO addTicketDTO)
        {
            var mapModel = _mapper.Map<Ticket>(addTicketDTO);

            var res = await _ticketServices.AddTicket(mapModel);

            if (res is not null)
            {
                // map model to the DTO and pass the DTO object to the bus queue
                var mapResult = _mapper.Map<ResponseTicketDTO>(res);
                // Send to the Bus
                var endPoint = await _bus.GetSendEndpoint(new Uri("queue:" + MessageBrokers.RabbitMQQueues.SagaBusQueue));
                await endPoint.Send<IGETValueEvent>(new
                {
                    TicketId = Guid.Parse(mapResult.TicketId),
                    Title = mapResult.Title,
                    Email = mapResult.Email,
                    RequireDate = mapResult.RequireDate,
                    Age = mapResult.Age,
                    Location = mapResult.Location
                });
                return StatusCode(StatusCodes.Status201Created);
            }
            return BadRequest();
        }
    }
}
