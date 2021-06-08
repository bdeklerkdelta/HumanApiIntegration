
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HealthApp.HealthInfo.Dto
{
    public class HumanApiTokenDto : HumanApiBaseTokenDto
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
    }
}
