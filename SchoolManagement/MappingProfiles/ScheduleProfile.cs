using AutoMapper;
using SchoolManagement.Data;
using SchoolManagement.Models;

namespace SchoolManagement.MappingProfiles
{
    public class ScheduleProfile : Profile
    {

        public ScheduleProfile()
        {
            CreateMap<Schedule, ScheduleModel>()
                .ForMember(dest => dest.ClassRoom, opt => opt.MapFrom(src => src.ClassRoom.Code))
                   .ForMember(dest => dest.CourseCode, opt => opt.MapFrom(src => src.Course.CourseCode))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RecordId));

            CreateMap<ScheduleModel, Schedule>()
            .ForMember(dest => dest.ClassRoom, opt => opt.MapFrom(src => new ClassRoom() { Code = src.ClassRoom }))
            .ForMember(dest => dest.Course, opt => opt.MapFrom(src => new Course() { CourseCode = src.CourseCode }))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
            .ForMember(dest => dest.RecordId, opt => opt.MapFrom(src => src.Id));

        }
    }
}
