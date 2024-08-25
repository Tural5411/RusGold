using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RusGold.Mvc.Models;
using RusGold.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RusGold.Mvc.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Route("Katalog")]
        public async Task<IActionResult> Index()
        {
            var data = await _categoryService.GetAllByNonDeletedAndActive();
            return View(data.Data);
        }

    }
}
