using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using HealthApp.HealthInfo.Dto;

namespace HealthApp.HealthInfo
{
    public interface IHealthInfoAppService : IAsyncCrudAppService<HealthInfoDto, string, PagedHealthIfnoResultRequestDto, CreateHealthInfoDto, HealthInfoDto>, IApplicationService
    {
       
    }
}
