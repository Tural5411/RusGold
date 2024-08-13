using AutoMapper;
using RusGold.Entities.Concrete;
using RusGold.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusGold.Services.AutoMapper.Profiles
{
    public class RegistertProfile:Profile
    {
        public RegistertProfile()
        {
            CreateMap<Registers, RegisterAddDto>();
            CreateMap<RegisterAddDto, Registers>();

            CreateMap<Registers, RegisterUpdateDto>();
            CreateMap<RegisterUpdateDto, Registers>();
        }
    }
}
