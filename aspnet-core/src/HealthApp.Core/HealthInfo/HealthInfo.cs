using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthApp.HealthInfo
{
    public class HealthInfo : Entity<string>
    {
        public long UserId { get; set; }
        public  string HumanId { get; set; }
        public DateTimeOffset Date { get; set; }
        public int Duration { get; set; }
        public double Distance { get; set; }
        public int Steps { get; set; }
        public double Calories { get; set; }
        public string Source { get; set; }
        public double Vigorous { get; set; }
        public double Moderate { get; set; }
        public double Light { get; set; }
        public double Sedentary { get; set; }
        public double TrackerCalories { get; set; }
        public int TrackerSteps { get; set; }
        public double TrackerDistance { get; set; }
        public double TrackerFloors { get; set; }
        public double TrackerElevation { get; set; }
        public double CaloriesBMR { get; set; }
        public double ActivityCalories { get; set; }
    }
}
