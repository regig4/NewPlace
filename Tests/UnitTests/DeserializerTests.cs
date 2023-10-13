using System.Collections.Generic;
using System.Text.Json;
using NewPlace.ResourceRepresentations;
using Xunit;

namespace Tests.UnitTests
{
    public class DeserializerTests
    {
        [Fact]
        public void DeserializeReferenceHandlerPreserveTest()
        {
            string json = @"{
              ""$id"": ""1"",
              ""$values"": []
            }";

            List<AdvertisementRepresentation> result = JsonSerializer.Deserialize<List<AdvertisementRepresentation>>(json, new JsonSerializerOptions { ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve });
        }
    }
}
