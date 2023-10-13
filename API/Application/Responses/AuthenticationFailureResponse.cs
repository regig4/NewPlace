using System.Collections.Generic;

namespace UserService.Application.Responses
{
    public class AuthenticationFailureResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
