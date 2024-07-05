using Microsoft.AspNetCore.Mvc;
using MediatR;
using TicketSystem.Application.Tickets.Commands.CreateTicket;
using TicketSystem.Application.Tickets.Queries.GetTickets;
using TicketSystem.Application.Tickets.Commands.HandleTicket;
using TicketSystem.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketSystem.Application.Tickets.Commends.CreateTicket;

namespace TicketSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TicketsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateTicketCommand command)
        {
            var ticketId = await _mediator.Send(command);
            return Ok(ticketId);
        }

        [HttpGet]
        public async Task<ActionResult<List<Ticket>>> Get()
        {
            var tickets = await _mediator.Send(new GetTicketsQuery());
            return Ok(tickets);
        }

        [HttpPut("{id}/handle")]
        public async Task<ActionResult> Handle(int id)
        {
            await _mediator.Send(new HandleTicketCommand { Id = id });
            return NoContent();
        }
    }
}
