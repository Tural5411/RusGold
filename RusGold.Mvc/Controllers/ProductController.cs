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
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IPhotoService _photoService;

        public ProductController(IProductService productService, ICategoryService categoryService, IPhotoService photoService)
        {
            _productService = productService;
            _photoService = photoService;
            _categoryService = categoryService;
        }
        [Route("Produkt")]
        [HttpGet]
        public async Task<IActionResult> Index(int? categoryId,int currentPage = 1, int pageSize = 9, bool isAscending = false)
        {
            var articleResult = await _productService.GetAllByPaging(categoryId, currentPage, pageSize, isAscending);

            if (categoryId != null)
            {
                var category = _categoryService.Get(categoryId.Value).Result.Data;
                ViewBag.categoryName = category.Category.Name;
            }
           
            return View(articleResult.Data);
        }


        [HttpGet]
        public async Task<IActionResult> Search(string keyword, int currentPage = 1, int pageSize = 4, bool isAscending = false)
        {
            var searchResult = await _productService.SearchAsync(keyword, currentPage, pageSize, isAscending);
            if (searchResult.ResultStatus == ResultStatus.Succes)
            {
                ViewBag.keyword = keyword;
                return View(searchResult.Data);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int productId)
        {
            var articleResult = await _productService.Get(productId);
            //var carPhotosResult = await _photoService.GetAllByNonDeletedAndActive(productId);

            if (articleResult.ResultStatus == ResultStatus.Succes)
            {
                return View(articleResult.Data);
            }
            return NotFound();
        }
        //[HttpGet]
        //public JsonResult GetCreditDetails(int modelId,int period)
        //{
        //    var creditDetails = _creditService.GetAllByNonDeletedAndActive().Result.Data.Credits.FirstOrDefault(x => x.ModelId == modelId && x.Period == period);

        //    return Json(new
        //    {
        //        initialPayment = creditDetails.InitialPayment,
        //        period = creditDetails.Period,
        //        monthlyPayment = creditDetails.MonthlyPay,
        //        carPrice = creditDetails.CarPrice
        //    });
        //}

    }
}
