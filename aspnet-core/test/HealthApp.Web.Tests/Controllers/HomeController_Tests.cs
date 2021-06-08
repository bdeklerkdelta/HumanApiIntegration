using System.Threading.Tasks;
using HealthApp.Models.TokenAuth;
using HealthApp.Web.Controllers;
using Shouldly;
using Xunit;

namespace HealthApp.Web.Tests.Controllers
{
    public class HomeController_Tests: HealthAppWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}