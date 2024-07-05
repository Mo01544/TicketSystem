using Microsoft.EntityFrameworkCore;
using TicketSystem.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace TicketSystem.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Ticket> Tickets { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
