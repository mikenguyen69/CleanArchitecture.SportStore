using CASportStore.Core.SharedKernel;

namespace CASportStore.Core.Interfaces
{
    public interface IDomainEventDispatcher
    {
        void Dispatch(BaseDomainEvent domainEvent);
    }
}