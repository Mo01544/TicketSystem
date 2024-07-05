using MediatR;

namespace TicketSystem.Application.Tickets.Commands.HandleTicket
{
    public class HandleTicketCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
