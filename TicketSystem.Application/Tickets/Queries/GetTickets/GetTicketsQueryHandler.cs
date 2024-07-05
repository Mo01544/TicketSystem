using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketSystem.Application.Common.Interfaces;
using TicketSystem.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TicketSystem.Application.Tickets.Queries.GetTickets
{
    public class GetTicketsQueryHandler : IRequestHandler<GetTicketsQuery, List<Ticket>>
    {
        private readonly IApplicationDbContext _context;

        public GetTicketsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Ticket>> Handle(GetTicketsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Tickets.ToListAsync(cancellationToken);
        }
    }
}
