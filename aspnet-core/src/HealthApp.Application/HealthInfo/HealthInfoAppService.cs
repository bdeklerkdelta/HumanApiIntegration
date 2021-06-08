using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Abp.Runtime.Caching;
using Abp.Runtime.Session;
using HealthApp.Authorization;
using HealthApp.Authorization.Roles;
using HealthApp.Authorization.Users;
using HealthApp.HealthInfo.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace HealthApp.HealthInfo
{
    public class HealthInfoAppService : AsyncCrudAppService<HealthInfo, HealthInfoDto, string, PagedHealthIfnoResultRequestDto, CreateHealthInfoDto, HealthInfoDto>, IHealthInfoAppService
    {
        private readonly IRepository<HealthInfo, string> _healthInfoRepository;
        private readonly IRepository<SourceHealthInfo, string> _sourceHealthInfoRepository;
        private readonly UserManager _userManager;
        private readonly ICacheManager _cacheManager;

        private const string Access = "access";
        private const string Id = "id";
        private const string Session = "session";


        public HealthInfoAppService(IRepository<HealthInfo, string> healthInfoRepository, UserManager userManager, ICacheManager cacheManager, IRepository<SourceHealthInfo, string> sourceHealthInfoRepository)
            : base(healthInfoRepository)
        {
            _healthInfoRepository = healthInfoRepository;
            _userManager = userManager;
            _cacheManager = cacheManager;
            _sourceHealthInfoRepository = sourceHealthInfoRepository;
        }

        public async Task<HumanApiSessionDto> GetHumanApiSessionAsync()
        {
            var user = await _userManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            if (user == null)
            {
                throw new Exception("There is no current user!");
            }

            if (user.HumanId != null)
            {
                var token = await GetToken<HumanApiSessionDto>(Id);

                if (token.ExpireInSeconds == 0)
                {
                    return await GetToken<HumanApiSessionDto>(Session);
                }
                else
                {
                    return token;
                }
            }
            else
            {
                var token = await GetToken<HumanApiSessionDto>(Session);

                user.HumanId = token.HumanId;

                await _userManager.UpdateAsync(user);

                return token;
            }
        }

        public async Task InsertHumanApiData() 
        {
            var token = await GetApiToken();

            var user = await _userManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            if (user == null)
            {
                throw new Exception("There is no current user!");
            }

            try
            {
                await InsertActivitySummaries(token, user);
                await GetSources(token, user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task InsertActivitySummaries(string token, User user)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var result = await client.GetAsync("https://api.humanapi.co/v1/human/activities/summaries");
                string resultContent = await result.Content.ReadAsStringAsync();
                var newData = JsonSerializer.Deserialize<List<HealthInfoDto>>(resultContent);

                var newHealthInfo = ObjectMapper.Map<List<HealthInfo>>(newData);

                newHealthInfo.ForEach(x => x.UserId = (int)user.Id);

                foreach (var healthInfo in newHealthInfo)
                {
                    _healthInfoRepository.InsertOrUpdate(healthInfo);
                }
            }
        }

        private async Task<List<SourceHealthInfoDto>> GetSources(string token, User user)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var result = await client.GetAsync("https://api.humanapi.co/v1/human/sources");
                string resultContent = await result.Content.ReadAsStringAsync();
                var newData = JsonSerializer.Deserialize<List<SourceHealthInfoDto>>(resultContent);

                return newData;
            }
        }

        private async Task<string> GetApiToken()
        {
            var cacheToken = await _cacheManager.GetCache("TokenCache")
                .TryGetValueAsync(AbpSession.UserId.ToString());

            if (cacheToken.Value != null)
            {
                return cacheToken.Value.ToString();
            }
            else
            {
                var newToken = await GetToken<HumanApiTokenDto>(Access);

                _cacheManager.GetCache("TokenCache").Set(AbpSession.UserId.ToString(), newToken.AccessToken, TimeSpan.FromSeconds(newToken.ExpireInSeconds));

                return newToken.AccessToken;
            }
        }

        private async Task<T> GetToken<T>(string tokenType)
        {
            var user = await _userManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            if (user == null)
            {
                throw new Exception("There is no current user!");
            }
            var dto = new HumanApiBaseTokenDto();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://auth.humanapi.co");
                var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("client_id", "pull_from_config"),
                new KeyValuePair<string, string>("client_secret", "pull_from_config"),
                new KeyValuePair<string, string>("type", tokenType),
                new KeyValuePair<string, string>("client_user_id", user.Id.ToString()),
                new KeyValuePair<string, string>("client_user_email", user.EmailAddress),
            });
                var result = await client.PostAsync("/v1/connect/token", content);
                string resultContent = await result.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(resultContent);
            }
        }

        public override async Task<HealthInfoDto> CreateAsync(CreateHealthInfoDto input)
        {
            //var healthInfo = ObjectMapper.Map<HealthInfo>(input);

            await _healthInfoRepository.InsertAsync(new HealthInfo { Id = input.Name});

            return MapToEntityDto(new HealthInfo { Id = input.Name });
        }
    }
}

