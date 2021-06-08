using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthApp.HealthInfo
{
    public class SourceHealthInfo : Entity<string>
    {
        public string Source { get; set; }
        public string SourceName { get; set; }
    }
}