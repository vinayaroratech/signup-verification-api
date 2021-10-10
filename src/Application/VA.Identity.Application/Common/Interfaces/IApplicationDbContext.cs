using System.Threading;
using System.Threading.Tasks;

namespace VA.Identity.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
