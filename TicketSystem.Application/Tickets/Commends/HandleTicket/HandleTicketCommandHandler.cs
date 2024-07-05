using MediatR;
using TicketSystem.Application.Common.Exceptions;
using TicketSystem.Application.Common.Interfaces;
using TicketSystem.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace TicketSystem.Application.Tickets.Commands.HandleTicket
{
    public class HandleTicketCommandHandler : IRequestHandler<HandleTicketCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public HandleTicketCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(HandleTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = await _context.Tickets.FindAsync(request.Id);

            if (ticket == null)
            {
                throw new NotFoundException(nameof(Ticket), request.Id);
            }

            ticket.IsHandled = true;
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
