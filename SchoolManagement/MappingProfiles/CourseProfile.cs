using AutoMapper;
using SchoolManagement.Data;
using SchoolManagement.Models;
using System.Linq;

namespace SchoolManagement.MappingProfiles
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<Course, CourseModel>()
                .ForMember(dest => dest.CourseCode, opt => opt.MapFrom(src => src.CourseCode))
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.CourseName))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RecordId))
             .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.StudentCourses.Select(s => s.Student)));

            CreateMap<CourseModel, Course>()
               .ForMember(dest => dest.CourseCode, opt => opt.MapFrom(src => src.CourseCode))
               .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.CourseName))
               .ForMember(dest => dest.RecordId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.StudentCourses, opt => opt.Ignore()); //check

        }
    }
}
