using CASportStore.Core.SharedKernel;

namespace CASportStore.Core.Interfaces
{
    public interface IHandle<T> where T : BaseDomainEvent
    {
        void Handle(T domainEvent);
    }
}