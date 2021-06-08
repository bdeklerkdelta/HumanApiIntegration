using Abp.Application.Services.Dto;

namespace HealthApp.HealthInfo.Dto
{
    public class PagedHealthIfnoResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

