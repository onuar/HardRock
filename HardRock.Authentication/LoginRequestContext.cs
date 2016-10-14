using System.Collections.Generic;

namespace HardRock.Authentication
{
    public class LoginRequestContext
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Dictionary<string, object> AdditionalInformation { get; set; }
    }
}