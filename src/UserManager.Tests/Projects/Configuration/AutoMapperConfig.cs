using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using UserManager.Domain.Entities;
using UserManager.Services.DTO;

namespace UserManager.Tests.Projects.Configuration
{
    public static class AutoMapperConfig
    {
        public static IMapper GetConfigurations()
        {
           var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>().ReverseMap();
            });

            return config.CreateMapper();
        }
    }
}