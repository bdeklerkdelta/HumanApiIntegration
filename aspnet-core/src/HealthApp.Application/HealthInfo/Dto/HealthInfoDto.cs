using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Abp.Application.Services.Dto;
using Abp.Authorization.Roles;
using Abp.AutoMapper;
using HealthApp.Authorization.Roles;

namespace HealthApp.HealthInfo.Dto
{
    public class HealthInfoDto : EntityDto<string>
    {
        [JsonPropertyName("id")]
        public string Id2 { set { Id = value; } }
        [JsonPropertyName("date")]
        public DateTimeOffset Date { get; set; }

        [JsonPropertyName("duration")]
        public int Duration { get; set; }

        [JsonPropertyName("distance")]
        public double Distance { get; set; }

        [JsonPropertyName("steps")]
        public int Steps { get; set; }

        [JsonPropertyName("calories")]
        public double Calories { get; set; }

        [JsonPropertyName("source")]
        public string Source { get; set; }

        [JsonPropertyName("vigorous")]
        public double Vigorous { get; set; }

        [JsonPropertyName("moderate")]
        public double Moderate { get; set; }

        [JsonPropertyName("light")]
        public double Light { get; set; }

        [JsonPropertyName("sedentary")]
        public double Sedentary { get; set; }

        [JsonPropertyName("sourceData")]
        public SourceData SourceData { get; set; }

        [JsonPropertyName("humanId")]
        public string HumanId { get; set; }
    }

    public class Tracker
    {
        [JsonPropertyName("calories")]
        public string Calories { get; set; }

        [JsonPropertyName("steps")]
        public string Steps { get; set; }

        [JsonPropertyName("distance")]
        public double Distance { get; set; }

        [JsonPropertyName("floors")]
        public double Floors { get; set; }

        [JsonPropertyName("elevation")]
        public double Elevation { get; set; }
    }

    public class SourceData
    {
        [JsonPropertyName("tracker")]
        public Tracker Tracker { get; set; }

        [JsonPropertyName("caloriesBMR")]
        public double CaloriesBMR { get; set; }

        [JsonPropertyName("activityCalories")]
        public double ActivityCalories { get; set; }
    }
}