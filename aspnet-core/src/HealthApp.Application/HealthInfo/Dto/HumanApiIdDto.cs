using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HealthApp.HealthInfo.Dto
{
    public class HumanApiIdDto : HumanApiBaseTokenDto
    {
        [JsonPropertyName("id_token")]
        public string IdToken { get; set; }
    }
}
