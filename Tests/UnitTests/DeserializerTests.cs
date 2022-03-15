using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
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

            var result = JsonSerializer.Deserialize<List<AdvertisementRepresentation>>(json, new JsonSerializerOptions { ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve });
        }
    }
}
