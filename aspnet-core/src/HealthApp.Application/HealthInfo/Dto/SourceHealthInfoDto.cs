using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HealthApp.HealthInfo.Dto
{
    public class SourceHealthInfoDto : EntityDto<string>
    {
        [JsonPropertyName("id")]
        public string Id2 { set { Id = value; } }
        [JsonPropertyName("source")]
        public string Source { get; set; }

        [JsonPropertyName("sourceName")]
        public string SourceName { get; set; }
    }
}
