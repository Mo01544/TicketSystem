using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace TicketSystem.Application.Tickets.Commends.CreateTicket
{
    public class CreateTicketCommand : IRequest<int>
    {
        public string PhoneNumber { get; set; }
        public string Governorate { get; set; }
        public string City { get; set; }
        public string District { get; set; }
    }
}
