using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace CASportStore.Core.SharedKernel
{
    // This can be modified to BaseEntity<TId> to support multiple key types (e.g. Guid)
    public abstract class BaseEntity
    {
        [BindNever]
        public int Id { get; set; }

        public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();
    }
}