using System.Threading.Tasks;
using Abp.Application.Services;
using HealthApp.Sessions.Dto;

namespace HealthApp.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
