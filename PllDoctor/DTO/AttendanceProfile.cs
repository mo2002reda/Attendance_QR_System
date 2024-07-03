using AutoMapper;
using Dll.Entity;

namespace PllDoctor.DTO
{
    public class AttendanceProfile : Profile
    {
        public AttendanceProfile()
        {
            CreateMap<AttendanceTable, AttendanceModelDTO>()
                .ForMember(x => x.StudentName, s => s.MapFrom(s => s.Name))
                .ForMember(x => x.STDId, s => s.MapFrom(s => s.StudentID))
                .ForMember(x => x.Date, s => s.MapFrom(s => s.CreateAt)).ReverseMap();
        }
    }
}
