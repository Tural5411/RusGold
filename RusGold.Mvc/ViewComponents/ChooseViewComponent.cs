﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RusGold.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RusGold.Mvc.ViewComponents
{
    public class ChooseViewComponent:ViewComponent
    {
        private readonly ChooseUsPageInfo _chooseUsPageInfo;
        public ChooseViewComponent(IOptions<ChooseUsPageInfo> chooseUsPageInfo)
        {
            _chooseUsPageInfo = chooseUsPageInfo.Value;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(_chooseUsPageInfo);
        }
    }
}
