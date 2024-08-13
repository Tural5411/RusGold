using AutoMapper;
using RusGold.Entities.Concrete;
using RusGold.Mvc.Areas.Admin.Models;
using RusGold.Entities.DTOs;


namespace RusGold.Mvc.AutoMapper.Profiles
{
    public class ViewModelsProfile:Profile
    {
        public ViewModelsProfile()  
        {

            CreateMap<QuestionAddViewModel, QuestionAddDto>();
            CreateMap<QuestionUpdateDto, QuestionUpdateViewModel>().ReverseMap();

            CreateMap<SliderAddViewModel, SliderAddDto>();
            CreateMap<CarAddViewModel, CarAddDto>();
            CreateMap<CarAddDto, CarAddViewModel>();
            CreateMap<CarUpdateDto, CarUpdateViewModel>().ReverseMap();
            CreateMap<CarBrendModelUpdateDto, CarBrendModelUpdateViewModel>().ReverseMap();
            CreateMap<CarBrendModel, CarBrendModelUpdateViewModel>().ReverseMap();
            CreateMap<SliderUpdateDto, SliderUpdateViewModel>().ReverseMap();

            CreateMap<PhotoAddDto, PhotoAddViewModel>().ReverseMap();

            CreateMap<ArticleAddViewModel, ArticleAddDto>();
            CreateMap<ArticleUpdateViewModel, ArticleUpdateDto>();
            CreateMap<ArticleUpdateDto,ArticleUpdateViewModel>();


            CreateMap<CreditUpdateViewModel, CreditUpdateDto>();
            CreateMap<CreditUpdateDto, CreditUpdateViewModel>();

            CreateMap<CreditAddDto, Credits>();
        }
    }
}
