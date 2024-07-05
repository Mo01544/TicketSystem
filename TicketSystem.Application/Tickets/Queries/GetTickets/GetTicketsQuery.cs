using MediatR;
using TicketSystem.Domain.Entities;
using System.Collections.Generic;

namespace TicketSystem.Application.Tickets.Queries.GetTickets
{
    public class GetTicketsQuery : IRequest<List<Ticket>>
    {
    }
}
