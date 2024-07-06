using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketSystem.Application.Common.Interfaces;
using TicketSystem.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TicketSystem.Application.Models;

namespace TicketSystem.Application.Tickets.Queries.GetTickets
{
    public class GetTicketsQueryHandler : IRequestHandler<GetTicketsQuery, PaginatedList<Ticket>>
    {
        private readonly IApplicationDbContext _context;

        public GetTicketsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<Ticket>> Handle(GetTicketsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Tickets.AsQueryable();
            var count = await query.CountAsync(cancellationToken);
            var items = await query.Skip((request.Page - 1) * request.PageSize)
                                    .Take(request.PageSize)
                                    .ToListAsync(cancellationToken);

            return new PaginatedList<Ticket>(items, count, request.Page, request.PageSize);
        }
    }
}
