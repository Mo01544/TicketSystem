using Microsoft.AspNetCore.Mvc;
using MediatR;
using TicketSystem.Application.Tickets.Commands.CreateTicket;
using TicketSystem.Application.Tickets.Queries.GetTickets;
using TicketSystem.Application.Tickets.Commands.HandleTicket;
using TicketSystem.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketSystem.Application.Tickets.Commends.CreateTicket;
using Microsoft.AspNetCore.Cors;
using TicketSystem.Application.Models;

namespace TicketSystem.API.Controllers
{
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
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
        public async Task<ActionResult<PaginatedList<Ticket>>> Get(int page = 1, int pageSize = 5)
        {
            var query = new GetTicketsQuery { Page = page, PageSize = pageSize };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPut("{id}/handle")]
        public async Task<ActionResult> Handle(int id)
        {
            await _mediator.Send(new HandleTicketCommand { Id = id });
            return NoContent();
        }
    }
}
