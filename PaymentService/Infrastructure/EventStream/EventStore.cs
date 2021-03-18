using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using PaymentService.ApplicationCore.Application.Services;
using PaymentService.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PaymentService.Infrastructure.EventStream
{
    public class EventStore : IEventStore
    {
        public async Task<List<IDomainEvent>> GetEvents(Guid entityId)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("newPlace");
            var collection = database.GetCollection<BsonDocument>("payments");
            var filter = Builders<BsonDocument>.Filter.Eq("id", entityId.ToString());
            var document = await collection.Find(filter).FirstOrDefaultAsync();

            if (document == null)
                return new List<IDomainEvent>();

            var eventsJson = document.GetValue("events").ToJson();
            var events = JsonSerializer.Deserialize<IDomainEvent[]>(eventsJson);
            return events.ToList();
        }

        public async Task SaveEvents(Guid id, List<IDomainEvent> uncommitedEvents)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("newPlace");
            var collection = database.GetCollection<BsonDocument>("payments");

            var storedEvents = await GetEvents(id);

            foreach (var e in storedEvents)
                if (!uncommitedEvents.Any(u => u.Id == e.Id))
                    uncommitedEvents.Add(e);

            var eventsToSave = uncommitedEvents.OrderBy(u => u.OccurredOn);

            var jsonToSave = JsonSerializer.Serialize<dynamic>(new { id = id.ToString(), events = eventsToSave });

            using (var jsonReader = new JsonReader(jsonToSave))
            {
                var context = BsonDeserializationContext.CreateRoot(jsonReader);
                var document = collection.DocumentSerializer.Deserialize(context);
                await collection.InsertOneAsync(document);
            }
        }
    }
}
