using Newtonsoft.Json;
using System;

namespace Api.Integration.Test
{
    public class LoginResponseDto
    {
        [JsonProperty("authenticated")]
        public bool authenticated { get; set; }

        [JsonProperty("createDate")]
        public DateTime createDate { get; set; }

        [JsonProperty("expirationDate")]
        public DateTime expirationDate { get; set; }

        [JsonProperty("accessToken")]
        public string accessToken { get; set; }

        [JsonProperty("userName")]
        public string userName { get; set; }

        [JsonProperty("message")]
        public string message { get; set; }
    }
}
