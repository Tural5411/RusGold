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
    public class ArticleProfile:Profile
    {
        public ArticleProfile()
        {
            //Birinci neyi donusdureceyimi yaziriq,ikinci neye donusdureceyimizi yaziriq
            //ArticleAddDto icinde CreateDate propertisi olmadigi ucun buradan verdim
            CreateMap<ArticleAddDto, Article>().ForMember(dest=>dest.CreatedDate,opt=>opt.MapFrom(x=>DateTime.Now));
            CreateMap<ArticleUpdateDto, Article>().ForMember(dest=>dest.ModifiedDate,opt=>opt.MapFrom(x=>DateTime.Now));
            CreateMap<Article, ArticleUpdateDto>();

            CreateMap<CarAddDto, Car>().ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(x => DateTime.Now));
            CreateMap<CarUpdateDto, Car>().ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(x => DateTime.Now));
            CreateMap<Car, CarUpdateDto>();

            CreateMap<CarBrendModelAddDto, CarBrendModel>().ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(x => DateTime.Now));
            CreateMap<CarBrendModelDto, CarBrendModel>().ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(x => DateTime.Now));
            CreateMap<CarBrendModel, CarBrendModelUpdateDto>();
        }
    }
}
