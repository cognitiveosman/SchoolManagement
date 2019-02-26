using AutoMapper;
using SchoolManagement.Data;
using SchoolManagement.Models;
using System.Linq;

namespace SchoolManagement.MappingProfiles
{
    public class StudentProfile : Profile
    {

        public StudentProfile()
        {
            CreateMap<Student, StudentModel>()
                .ForMember(dest => dest.PersonNumber, opt => opt.MapFrom(src => src.PersonNumber))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RecordId))
                .ForMember(dest => dest.Courses, opt => opt.MapFrom(src => src.StudentCourses.Select(sc => sc.Course.CourseCode)));


            CreateMap<StudentModel, Student>()
                .ForMember(dest => dest.PersonNumber, opt => opt.MapFrom(src => src.PersonNumber))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.RecordId, opt => opt.MapFrom(src => src.Id));
            //   .ForMember(dest => dest., opt => opt.MapFrom(src => src.StudentCourses.Select(sc => sc.Course)));

        }

    }
}
