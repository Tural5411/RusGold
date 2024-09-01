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
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;

namespace RusGold.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : BaseController
    {
        private readonly IProductService _carService;
        private readonly ICategoryService _category;
        private readonly IToastNotification _toastNotification;
        private readonly IPhotoService _photoService;

        public ProductController(ICategoryService category, IPhotoService photoService, IProductService carService, IToastNotification toastNotification, UserManager<User> userManager, IMapper mapper, IImageHelper imageHelper) : base(userManager, mapper, imageHelper)
        {
            _carService = carService;
            _toastNotification = toastNotification;
            _photoService = photoService;
            _category = category;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _carService.GetAllByNonDeleteAndActive();
            return View(result.Data);
        }
        [HttpGet]
        public IActionResult Add()
        {
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
            return View(ProductViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int carId)
        {
            var result = await _carService.GetUpdateDto(carId);
            var images = await _photoService.GetAllByNonDeletedAndActive(carId);
            if (result.ResultStatus == ResultStatus.Succes)
            {
                var teamUpdateViewModel = Mapper.Map<CarUpdateViewModel>(result.Data);
                teamUpdateViewModel.Images = images.Data.Photos;
                return View(teamUpdateViewModel);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Update(CarUpdateViewModel teamUpdateViewModel)
        {
            if (ModelState.IsValid)
            {
                bool isNewThumbnailUploaded = false;
                var oldThumbnail = teamUpdateViewModel.Thumbnail;
                if (teamUpdateViewModel.PictureFile != null)
                {
                    var uploadedImageResult = await ImageHelper.UploadImage(teamUpdateViewModel.Name,
                        teamUpdateViewModel.PictureFile, PictureType.Post);
                    teamUpdateViewModel.Thumbnail = uploadedImageResult.ResultStatus
                        == ResultStatus.Succes ? uploadedImageResult.Data.FullName
                        : "postImages/defaultThumbnail.jpg";
                    if (oldThumbnail != "postImages/defaultThumbnail.jpg")
                    {
                        isNewThumbnailUploaded = true;
                    }
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
            return View(teamUpdateViewModel);
        }
        [HttpPost]
        public async Task<JsonResult> Delete(int teamId)
        {
            var result = await (_carService).Delete(teamId, LoggedInUser.UserName);
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
