using System.Collections.Generic;
using AutoMapper;
using RusGold.Entities.ComplexTypes;
using RusGold.Entities.Concrete;
using RusGold.Entities.DTOs;
using RusGold.Mvc.Areas.Admin.Helpers.Abstract;
using RusGold.Mvc.Areas.Admin.Models;
using RusGold.Services.Abstract;
using RusGold.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RusGold.Data.Abstract.UnitOfWorks;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace RusGold.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : BaseController
    {
        private readonly IProductService _carService;
        private readonly ICategoryService _categoryService;
        private readonly IToastNotification _toastNotification;
        private readonly IPhotoService _photoService;

        public ProductController(ICategoryService categoryService, IPhotoService photoService, IProductService carService, IToastNotification toastNotification, UserManager<User> userManager, IMapper mapper, IImageHelper imageHelper) : base(userManager, mapper, imageHelper)
        {
            _carService = carService;
            _toastNotification = toastNotification;
            _photoService = photoService;
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProductPricesBasedOnDollar()
        {
            var result =await  _carService.UpdateProductPricesBasedOnDollar();
            if (result.ResultStatus == ResultStatus.Succes)
            {
                _toastNotification.AddSuccessToastMessage(result.Message, new ToastrOptions
                {
                    Title = "Uğurlu əməliyyat"
                });
                return RedirectToAction("Index", "Product", new { area = "Admin" });
            }
            else
            {
                ModelState.AddModelError("", result.Message);
                return RedirectToAction("Index", "Product", new { area = "Admin" });
            }
        }

       

        public async Task<IActionResult> Index()
        {
            var result = await _carService.GetAllByNonDeleteAndActive();
            return View(result.Data);
        }
        [HttpGet]
        public IActionResult Add()
        {
            var categories = _categoryService.GetAllByNonDeletedAndActive();
            ViewBag.categories = categories.Result.Data.Categories.Where(x => x.ParentId == null);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(ProductViewModel ProductViewModel)
        {
            if (ModelState.IsValid)
            {
                var carAddDto = Mapper.Map<ProductAddDto>(ProductViewModel);
                var imageResult = await ImageHelper.UploadImage(ProductViewModel.Name,
                    ProductViewModel.PictureFile, PictureType.Post);
                carAddDto.ThumbNail = imageResult.Data.FullName;
                var result = await _carService.Add(carAddDto, LoggedInUser.UserName);

                if (ProductViewModel.CarPhotos != null)
                {
                    ProductViewModel.Photos = new List<PhotoAddViewModel>();
                    foreach (var file in ProductViewModel.CarPhotos)
                    {
                        var galleryResult = await ImageHelper.UploadImageV2(file);
                        var gallery = new PhotoAddDto()
                        {
                            CarId = result.Data.Product.Id,
                            ImageUrl = galleryResult
                        };
                        await _photoService.Add(gallery, "RusGold");
                    }
                }

                if (result.ResultStatus == ResultStatus.Succes)
                {
                    _toastNotification.AddSuccessToastMessage(result.Message, new ToastrOptions
                    {
                        Title = "Uğurlu əməliyyat"
                    });
                    return RedirectToAction("Index", "Product", new { area = "Admin" });
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }
            }
            var categories = _categoryService.GetAllByNonDeletedAndActive();
            ViewBag.categories = categories.Result.Data.Categories.Where(x => x.ParentId == null);
            return View(ProductViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int productId)
        {
            var categories = _categoryService.GetAllByNonDeletedAndActive();
            ViewBag.categories = categories.Result.Data.Categories.Where(x => x.ParentId == null);
            var result = await _carService.GetUpdateDto(productId);
            var images = await _photoService.GetAllByNonDeletedAndActive(productId);
            if (result.ResultStatus == ResultStatus.Succes)
            {
                var teamUpdateViewModel = Mapper.Map<ProductUpdateViewModel>(result.Data);
                teamUpdateViewModel.Images = images.Data.Photos;
                return View(teamUpdateViewModel);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateViewModel teamUpdateViewModel)
        {
            if (ModelState.IsValid)
            {
                bool isNewThumbnailUploaded = false;
                var oldThumbnail = teamUpdateViewModel.Thumbnail;
                if (teamUpdateViewModel.PictureFile != null)
                {
                    var uploadedImageResult = await ImageHelper.UploadImage(teamUpdateViewModel.Name,
                        teamUpdateViewModel.PictureFile, PictureType.Post);
                    teamUpdateViewModel.Thumbnail = uploadedImageResult.Data.FullName;
                  
                }
                var teamUpdateDto = Mapper.Map<ProductUpdateDto>(teamUpdateViewModel);
                var result = await (_carService).Update(teamUpdateDto, LoggedInUser.UserName);


                ////Multi Image
                //if (teamUpdateViewModel.CarPhotos != null)
                //{
                //    teamUpdateViewModel.p = new List<PhotoAddViewModel>();
                //    foreach (var file in projectUpdateViewModel.ProjectPhotos)
                //    {
                //        var galleryResult = await ImageHelper.UploadImageV2(file);
                //        var gallery = new PhotoAddDto()
                //        {
                //            ProjectId = result.Data.Project.Id,
                //            URL = galleryResult
                //        };
                //        await _photoService.Add(gallery, "Damplus");
                //    }
                //}

                if (result.ResultStatus == ResultStatus.Succes)
                {
                    if (isNewThumbnailUploaded)
                    {
                        ImageHelper.ImageDelete(oldThumbnail);
                    }
                    _toastNotification.AddSuccessToastMessage(result.Message, new ToastrOptions
                    {
                        Title = "Uğurlu əməliyyat",
                        CloseButton = true
                    });
                    return RedirectToAction("Index", "Product", new { area = "Admin" });
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }
            }
            var categories = _categoryService.GetAllByNonDeletedAndActive();
            ViewBag.categories = categories.Result.Data.Categories.Where(x => x.ParentId == null);
            return View(teamUpdateViewModel);
        }
        [HttpPost]
        public async Task<JsonResult> Delete(int productId)
        {
            var result = await (_carService).Delete(productId, LoggedInUser.UserName);
            var deletedTeam = JsonSerializer.Serialize(result);
            return Json(deletedTeam);
        }

        [HttpPost]
        public async Task<JsonResult> DeletePhoto(int photoId)
        {
            var result = await _photoService.HardDelete(photoId);
            var deletedTeam = JsonSerializer.Serialize(result);
            return Json(deletedTeam);
        }

    }
}
