using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RusGold.Entities.Concrete;
using RusGold.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RusGold.Services.Abstract;

namespace RusGold.Mvc.ViewComponents
{
    public class SliderViewComponent : ViewComponent
    {
        private readonly ISliderService _sliderService;
        public SliderViewComponent(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var slider = _sliderService.GetAllByNonDeletedAndActive();
            return View(slider.Result.Data);
        }
    }
}
