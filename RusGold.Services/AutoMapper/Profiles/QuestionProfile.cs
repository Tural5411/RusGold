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
    public class QuestionProfile:Profile
    {
        public QuestionProfile()
        {
            CreateMap<QuestionDto, Questions>();
            CreateMap<Questions, QuestionDto>();

            CreateMap<QuestionAddDto, Questions>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(x => DateTime.Now))
                .ForMember(dest => dest.ModifiedByName, opt => opt.MapFrom(x => DateTime.Now))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(x => false));
            CreateMap<Questions,QuestionAddDto>();

            CreateMap<QuestionUpdateDto, Questions>()
                .ForMember(dest=>dest.CreatedDate,opt=>opt.MapFrom(x=>DateTime.Now));
            CreateMap<Questions, QuestionUpdateDto>();

        }
    }
}
