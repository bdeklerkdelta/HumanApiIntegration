using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Roles;
using HealthApp.Authorization.Roles;

namespace HealthApp.HealthInfo.Dto
{
    public class CreateHealthInfoDto
    {
        [Required]
        [StringLength(AbpRoleBase.MaxNameLength)]
        public string Name { get; set; }

        public CreateHealthInfoDto()
        {
        }
    }
}
