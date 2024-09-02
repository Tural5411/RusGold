﻿using Microsoft.AspNetCore.Mvc;
using RusGold.Mvc.Models;
using RusGold.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RusGold.Mvc.ViewComponents
{
    public class MobilHeaderViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        public MobilHeaderViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryService.GetAllByNonDeletedAndActive();
            return View(categories.Data);
        }
    }
}
