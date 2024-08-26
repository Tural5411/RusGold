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
        private readonly ICreditService _creditService;
        private readonly ICategoryService _categoryService;
        private readonly IPhotoService _photoService;

        public ProductController(ICreditService creditService,IProductService carService, ICategoryService categoryService, IPhotoService photoService)
        {
            _productService = carService;
            _creditService = creditService;
            _photoService = photoService;
            _categoryService = categoryService;
        }
        [Route("Produkt")]
        [HttpGet]
        public async Task<IActionResult> Index(int? brendId, int? modelId, int currentPage = 1, int pageSize = 6, bool isAscending = false)
        {
            var articleResult = await _productService.GetAllByPaging(brendId, modelId, currentPage, pageSize, isAscending);
            return View(articleResult.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int articleId)
        {
            var articleResult = await _productService.Get(articleId);
            var carPhotosResult = await _photoService.GetAllByNonDeletedAndActive(articleId);

            if (articleResult.ResultStatus == ResultStatus.Succes)
            {
                return View(new ProductDetailViewModel
                {
                    ProductDto = articleResult.Data,
                    ProductPhotos = carPhotosResult.Data
                });
            }
            return NotFound();
        }
        [HttpGet]
        public JsonResult GetCreditDetails(int modelId,int period)
        {
            var creditDetails = _creditService.GetAllByNonDeletedAndActive().Result.Data.Credits.FirstOrDefault(x => x.ModelId == modelId && x.Period == period);

            return Json(new
            {
                initialPayment = creditDetails.InitialPayment,
                period = creditDetails.Period,
                monthlyPayment = creditDetails.MonthlyPay,
                carPrice = creditDetails.CarPrice
            });
        }

    }
}
