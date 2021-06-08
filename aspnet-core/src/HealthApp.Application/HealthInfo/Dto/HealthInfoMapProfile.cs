using AutoMapper;

namespace HealthApp.HealthInfo.Dto
{
    public class HealthInfoMapProfile : Profile
    {
        public HealthInfoMapProfile()
        {
            CreateMap<HealthInfo, HealthInfoDto>().ForPath(x => x.SourceData.ActivityCalories,
                    opt => opt.MapFrom(x => x.ActivityCalories))

                .ForPath(x => x.SourceData.CaloriesBMR,
                    opt => opt.MapFrom(x => x.CaloriesBMR))

                .ForPath(x => x.SourceData.Tracker.Calories,
                    opt => opt.MapFrom(x => x.TrackerCalories))

                .ForPath(x => x.SourceData.Tracker.Distance,
                    opt => opt.MapFrom(x => x.TrackerDistance))

                .ForPath(x => x.SourceData.Tracker.Elevation,
                    opt => opt.MapFrom(x => x.TrackerElevation))

                .ForPath(x => x.SourceData.Tracker.Floors,
                    opt => opt.MapFrom(x => x.TrackerFloors))

                .ForPath(x => x.SourceData.Tracker.Steps,
                    opt => opt.MapFrom(x => x.TrackerSteps));

            CreateMap<HealthInfoDto, HealthInfo>().ForMember(x => x.ActivityCalories,
                    opt => opt.MapFrom(x => x.SourceData.ActivityCalories))

                .ForMember(x => x.CaloriesBMR,
                    opt => opt.MapFrom(x => x.SourceData.CaloriesBMR))

                .ForMember(x => x.TrackerCalories,
                    opt => opt.MapFrom(x => x.SourceData.Tracker.Calories))

                .ForMember(x => x.TrackerDistance,
                    opt => opt.MapFrom(x => x.SourceData.Tracker.Distance))

                .ForMember(x => x.TrackerElevation,
                    opt => opt.MapFrom(x => x.SourceData.Tracker.Elevation))

                .ForMember(x => x.TrackerFloors,
                    opt => opt.MapFrom(x => x.SourceData.Tracker.Floors))

                .ForMember(x => x.TrackerSteps,
                    opt => opt.MapFrom(x => x.SourceData.Tracker.Steps))

                .ForMember(x => x.Id,
                    opt => opt.MapFrom(x => x.Id));

            CreateMap<SourceHealthInfo, SourceHealthInfoDto>();
            CreateMap<SourceHealthInfoDto, SourceHealthInfo>();
        }
    }
}
