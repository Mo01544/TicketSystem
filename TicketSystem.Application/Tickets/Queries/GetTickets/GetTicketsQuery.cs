using MediatR;
using TicketSystem.Domain.Entities;
using System.Collections.Generic;
using TicketSystem.Application.Models;

namespace TicketSystem.Application.Tickets.Queries.GetTickets
{
    public class GetTicketsQuery : IRequest<PaginatedList<Ticket>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
