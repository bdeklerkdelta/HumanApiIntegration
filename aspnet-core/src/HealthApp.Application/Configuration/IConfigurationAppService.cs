using System.Threading.Tasks;
using HealthApp.Configuration.Dto;

namespace HealthApp.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
