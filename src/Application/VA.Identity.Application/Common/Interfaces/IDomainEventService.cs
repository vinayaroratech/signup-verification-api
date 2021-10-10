using System.Threading.Tasks;
using VA.Identity.Domain.Common;

namespace VA.Identity.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
