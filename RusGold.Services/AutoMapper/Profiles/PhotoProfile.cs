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
    public class PhotoProfile : Profile
    {
        public PhotoProfile()
        {
            CreateMap<PhotoAddDto, CarPhotos>().ForMember(dest=>dest.CreatedDate,opt=>opt.MapFrom(x=>DateTime.Now)).ReverseMap();
        }
    }
}
