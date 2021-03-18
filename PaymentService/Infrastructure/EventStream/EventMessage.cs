using PaymentService.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.Infrastructure.EventStream
{
    public class EventMessage
    {
        public Guid EntityId { get; set; }
        public string EntityType { get; set; }
        public string EventType { get; set; }
        public dynamic Event { get; set; }
    }
}
