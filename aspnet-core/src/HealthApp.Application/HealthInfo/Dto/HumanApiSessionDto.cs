using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HealthApp.HealthInfo.Dto
{
    public class HumanApiSessionDto : HumanApiBaseTokenDto
    {
        [JsonPropertyName("human_id")]
        public string HumanId { get; set; }
        [JsonPropertyName("session_token")]
        public string SessionToken { get; set; }
        [JsonPropertyName("id_token")]
        public string IdToken { set { SessionToken = value; } }
        [JsonPropertyName("id_token_expires_in")]
        public int IdTokenExpires { set {  ExpireInSeconds = value; } }
    }
}
