using Ardalis.GuardClauses;
using CASportStore.Core.Events;
using CASportStore.Core.Interfaces;

namespace CASportStore.Core.Services
{
    public class ToDoItemService : IHandle<ToDoItemCompletedEvent>
    {
        public void Handle(ToDoItemCompletedEvent domainEvent)
        {
            Guard.Against.Null(domainEvent, nameof(domainEvent));

            // Do Nothing
        }
    }
}
