using Common.Dto;

namespace NewPlace.ResourceRepresentations
{
    public class UserRepresentation : Representation<UserDto>
    {
        public string Password {get; set;}
        public string Token { get; set; }
    }
}
