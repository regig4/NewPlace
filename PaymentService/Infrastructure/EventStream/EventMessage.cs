using System;

namespace PaymentService.Infrastructure.EventStream
{
    public class EventMessage
    {
        public Guid EntityId { get; set; }
        public string EntityAssembly { get; set; }
        public string EntityType { get; set; }
        public string EntityFullType { get; set; }
        public string EventType { get; set; }
        public dynamic Event { get; set; }
    }
}
