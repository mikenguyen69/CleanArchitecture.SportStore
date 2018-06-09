using CASportStore.Core.Events;
using CASportStore.Core.SharedKernel;

namespace CASportStore.Core.Entities
{
    public class ToDoItem : BaseEntity
    {
        public string Title { get; set; } 
        public string Description { get; set; }
        public bool IsDone { get; private set; } = false;

        public void MarkComplete()
        {
            IsDone = true;
            Events.Add(new ToDoItemCompletedEvent(this));
        }
    }
}