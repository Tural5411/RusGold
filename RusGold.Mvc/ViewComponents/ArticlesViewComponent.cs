using Microsoft.AspNetCore.Mvc;
using RusGold.Mvc.Models;
using RusGold.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RusGold.Mvc.ViewComponents
{
    public class ArticlesViewComponent : ViewComponent
    {
        private readonly IArticleService _articleService;
        public ArticlesViewComponent(IArticleService articleService)
        {
            _articleService = articleService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var articlesResult = await _articleService.GetAllByViewCount(takeSize: 3);
            return View(new ArticleViewModel
            {
                ArticleListDto = articlesResult.Data
            });
        }
    }
}
