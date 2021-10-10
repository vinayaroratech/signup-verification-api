using VA.Identity.Domain.Common;
using System.Threading.Tasks;

namespace VA.Identity.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
