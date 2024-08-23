using RusGold.Entities.Concrete;
using RusGold.Mvc.Models;
using RusGold.Services.Abstract;
using RusGold.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RusGold.Mvc.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }
        [Route("Bloq")]
        [HttpGet]
        public async Task<IActionResult> Index(int currentPage = 1, int pageSize = 6, bool isAscending = false)
        {
            var articleResult = await _articleService.GetAllByPaging(null, currentPage, pageSize, isAscending);
            return View(articleResult.Data);
        }
        
        [HttpGet]
        public async Task<IActionResult> Detail(int articleId)
        {
            var articleResult = await _articleService.Get(articleId);

            if (articleResult.ResultStatus == ResultStatus.Succes)
            {
                await _articleService.IncreaseViewCount(articleId);
                List<String> listStrLineElements;

                listStrLineElements = articleResult.Data.Article.SeoTags.Split(',').ToList();
                ViewBag.listTags = listStrLineElements;
                return View(new ArticleDetailViewModel
                {
                    ArticleDto = articleResult.Data
                });
            }
            return NotFound();
        }
    }
}
