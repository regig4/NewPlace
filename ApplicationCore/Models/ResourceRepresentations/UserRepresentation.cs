using ApplicationCore.Models;

namespace NewPlace.ResourceRepresentations
{
    public class UserRepresentation : Representation<User>
    {
        public string Password {get; set;}
        public string Token { get; set; }
    }
}
