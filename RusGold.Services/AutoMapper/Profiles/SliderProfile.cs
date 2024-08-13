using AutoMapper;
using RusGold.Entities.Concrete;
using RusGold.Entities.DTOs;
using System;

namespace RusGold.Services.AutoMapper.Profiles
{
    public class SliderProfile : Profile
    {
        public SliderProfile()
        {
            CreateMap<SliderAddDto, Slider>().ForMember(dest=>dest.CreatedDate,opt=>opt.MapFrom(x=>DateTime.Now));
            CreateMap<SliderUpdateDto, Slider>().ForMember(dest=>dest.ModifiedDate,opt=>opt.MapFrom(x=>DateTime.Now));
            CreateMap<Slider, SliderUpdateDto>();
        }
    }
}
