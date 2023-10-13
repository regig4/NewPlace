using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Common.ApplicationCore.Domain.Events;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using PaymentService.ApplicationCore.Application.Services;

namespace PaymentService.Infrastructure.EventStream
{
    public class EventStore : IEventStore
    {
        public async Task<List<IDomainEvent>> GetEvents(Guid entityId)
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase database = client.GetDatabase("newPlace");
            IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("payments");
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("id", entityId.ToString());
            BsonDocument document = await collection.Find(filter).FirstOrDefaultAsync();

            if (document == null)
            {
                return new List<IDomainEvent>();
            }

            string eventsJson = document.GetValue("events").ToJson();
            IDomainEvent[] events = JsonSerializer.Deserialize<IDomainEvent[]>(eventsJson);
            return events.ToList();
        }

        public async Task SaveEvents(Guid id, List<IDomainEvent> uncommitedEvents)
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase database = client.GetDatabase("newPlace");
            IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("payments");

            List<IDomainEvent> storedEvents = await GetEvents(id);

            foreach (IDomainEvent e in storedEvents)
            {
                if (!uncommitedEvents.Any(u => u.Id == e.Id))
                {
                    uncommitedEvents.Add(e);
                }
            }

            IOrderedEnumerable<IDomainEvent> eventsToSave = uncommitedEvents.OrderBy(u => u.OccurredOn);

            string jsonToSave = JsonSerializer.Serialize<dynamic>(new { id = id.ToString(), events = eventsToSave });

            using (JsonReader jsonReader = new JsonReader(jsonToSave))
            {
                BsonDeserializationContext context = BsonDeserializationContext.CreateRoot(jsonReader);
                BsonDocument document = collection.DocumentSerializer.Deserialize(context);
                await collection.InsertOneAsync(document);
            }
        }
    }
}
