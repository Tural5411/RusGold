using AutoMapper;
using RusGold.Entities.ComplexTypes;
using RusGold.Entities.Concrete;
using RusGold.Entities.DTOs;
using RusGold.Mvc.Areas.Admin.Helpers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RusGold.Mvc.AutoMapper.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile(/*IImageHelper imageHelper*/)
        {
            CreateMap<UserAddDto, User>();
            //    .ForMember(dest=>dest.Picture,opt=>opt
            //.MapFrom(x=>imageHelper.UploadImage(x.UserName,x.PictureFile,PictureType.User,null)));  
            CreateMap<User,UserAddDto>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<User, UserUpdateDto>();

            CreateMap<CreditAddDto, Credits>().ReverseMap();
            CreateMap<CreditUpdateDto, Credits>().ReverseMap();
            CreateMap<CarBrendModelUpdateDto, CarBrendModel>().ReverseMap();
        }
    }
}
