using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using HealthApp.Configuration.Dto;

namespace HealthApp.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : HealthAppAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
