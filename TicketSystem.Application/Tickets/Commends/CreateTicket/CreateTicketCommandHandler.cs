using MediatR;
using TicketSystem.Application.Common.Interfaces;
using TicketSystem.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using TicketSystem.Application.Tickets.Commends.CreateTicket;

namespace TicketSystem.Application.Tickets.Commands.CreateTicket
{
    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateTicketCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = new Ticket
            {
                CreationDate = DateTime.UtcNow,
                PhoneNumber = request.PhoneNumber,
                Governorate = request.Governorate,
                City = request.City,
                District = request.District,
                IsHandled = false
            };

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync(cancellationToken);

            return ticket.Id;
        }
    }
}
