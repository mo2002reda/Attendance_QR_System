using AutoMapper;
using Dll.Entity;
using Microsoft.AspNetCore.Identity;
using PllDoctor.Models;

namespace PllDoctor.DTO
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<IdentityRole, RoleViewModel>().ForMember(o => o.RoleName, s => s.MapFrom(s => s.Name)).ReverseMap();

        }
    }
}
