using Abp.Application.Services;
using HealthApp.MultiTenancy.Dto;

namespace HealthApp.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

