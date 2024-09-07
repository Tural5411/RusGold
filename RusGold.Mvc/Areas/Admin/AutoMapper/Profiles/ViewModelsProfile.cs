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
            CreateMap<ProductViewModel, ProductAddDto>();
            CreateMap<ProductAddDto, ProductViewModel>();
            CreateMap<ProductUpdateDto, ProductUpdateViewModel>().ReverseMap();
            CreateMap<CategoryUpdateDto, CategoryUpdateViewModel>().ReverseMap();
            CreateMap<Category, CategoryUpdateViewModel>().ReverseMap();
            CreateMap<SliderUpdateDto, SliderUpdateViewModel>().ReverseMap();

            CreateMap<PhotoAddDto, PhotoAddViewModel>().ReverseMap();

            CreateMap<ArticleAddViewModel, ArticleAddDto>();
            CreateMap<CategoryAddViewModel, CategoryAddDto>().ReverseMap();
            CreateMap<CategoryUpdateViewModel, CategoryUpdateDto>().ReverseMap();
            CreateMap<ArticleUpdateViewModel, ArticleUpdateDto>();
            CreateMap<ArticleUpdateDto,ArticleUpdateViewModel>();


            CreateMap<CreditUpdateViewModel, CreditUpdateDto>();
            CreateMap<CreditUpdateDto, CreditUpdateViewModel>();

            CreateMap<CreditAddDto, Credits>();
        }
    }
}
