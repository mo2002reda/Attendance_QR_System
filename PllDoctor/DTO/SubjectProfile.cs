using AutoMapper;
using Dll.Entity;
using PllDoctor.Models;

namespace PllDoctor.DTO
{
	public class SubjectProfile : Profile
	{
		public SubjectProfile()
		{
			CreateMap<SubjectViewModel, Subject>().ReverseMap();
			//CreateMap<Attendance, AttendanceModelDTO>().ReverseMap();
		}
	}
}
