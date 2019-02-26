using AutoMapper;
using SchoolManagement.Data;
using SchoolManagement.Models;

namespace SchoolManagement.MappingProfiles
{
    public class ClassRoomsProfile : Profile
    {
        public ClassRoomsProfile()
        {
            CreateMap<ClassRoom, ClassRoomModel>()
                .ForMember(dest => dest.Code, opts => opts.MapFrom(src => src.Code))
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.RecordId));

            CreateMap<ClassRoomModel, ClassRoom>()
                .ForMember(dest => dest.Code, opts => opts.MapFrom(src => src.Code))
                .ForMember(dest => dest.RecordId, opts => opts.Ignore());

        }
    }
}
