using System;
using System.Threading.Tasks;
using Common.IntegrationEvents;

namespace ApplicationCore.Services
{
    public interface IMessageQueue
    {
        public Task<IntegrationEvent> WaitForEvent(IntegrationEvent @event, TimeSpan? timeSpan = null);
    }
}
