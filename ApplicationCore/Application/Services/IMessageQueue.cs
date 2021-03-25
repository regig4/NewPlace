using Common.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IMessageQueue
    {
        public Task<IntegrationEvent> WaitForEvent(IntegrationEvent @event, TimeSpan? timeSpan = null);
    }
}
