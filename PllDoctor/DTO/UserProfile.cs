using AutoMapper;
using Dll.Entity;
using PllDoctor.Views.Account;

namespace PllDoctor.DTO
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserViewModel, ApplicationUser>().ReverseMap();
        }
    }
}
