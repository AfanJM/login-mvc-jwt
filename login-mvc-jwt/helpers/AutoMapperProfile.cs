using AutoMapper;
using login_mvc_jwt.Dto.Users;
using login_mvc_jwt.Models;

namespace login_mvc_jwt.helpers
{
    public class AutoMapperProfile: Profile
    {

           public AutoMapperProfile() 
             {

            CreateMap<registerDto, UsersModels>();    
            CreateMap<loginDto, UsersModels>();

             }

    }
}
