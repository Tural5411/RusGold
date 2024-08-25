using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using RusGold.Entities.ComplexTypes;
using RusGold.Entities.Concrete;
using RusGold.Entities.DTOs;
using RusGold.Mvc.Areas.Admin.Helpers.Abstract;
using RusGold.Mvc.Areas.Admin.Models;
using RusGold.Services.Abstract;
using RusGold.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace RusGold.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IToastNotification _toastNotification;

        public CategoryController(ICategoryService carService, IToastNotification toastNotification, UserManager<User> userManager, IMapper mapper, IImageHelper imageHelper) : base(userManager, mapper, imageHelper)
        {
            _categoryService = carService;
            _toastNotification = toastNotification;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetAllByNonDeletedAndActive();
            return View(result.Data);
        }
        [HttpGet]
        public IActionResult Add()
        {
            var result = _categoryService.GetAllByNonDeletedAndActive();
            ViewBag.brendModels = result.Result.Data.Categories.Where(x=>x.ParentId==0);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(CategoryAddViewModel categoryAddViewModel)
        {
            if (ModelState.IsValid)
            {
                var articleAddDto = Mapper.Map<CategoryAddDto>(categoryAddViewModel);
                var imageResult = await ImageHelper.UploadImage(categoryAddViewModel.Name,
                    categoryAddViewModel.PictureFile, PictureType.Post);
                articleAddDto.Thumbnail = imageResult.Data.FullName;

                var result = await _categoryService.Add(articleAddDto,"Radiodetal");
                if (result.ResultStatus == ResultStatus.Succes)
                {
                    _toastNotification.AddSuccessToastMessage(result.Message, new ToastrOptions
                    {
                        Title = "Uğurlu əməliyyat"
                    });
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }
            }
            return View(categoryAddViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int categoryId)
        {
            var result = await _categoryService.GetCategoryUpdateDto(categoryId);


            if (result.ResultStatus == ResultStatus.Succes)
            {
                var videoUpdateViewModel = Mapper.Map<CategoryUpdateViewModel>(result.Data);
                videoUpdateViewModel.Id = categoryId;
                return View(videoUpdateViewModel);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateViewModel videoUpdateViewModel)
        {
            if (ModelState.IsValid)
            {
                var videoUpdateDto = Mapper.Map<CategoryUpdateDto>(videoUpdateViewModel);

                var imageResult = await ImageHelper.UploadImage(videoUpdateViewModel.Name,
                    videoUpdateViewModel.PictureFile, PictureType.Post);
                videoUpdateDto.Thumbnail = imageResult.Data.FullName;

                var result = await _categoryService.Update(videoUpdateDto, LoggedInUser.UserName);
                if (result.ResultStatus == ResultStatus.Succes)
                {
                    _toastNotification.AddSuccessToastMessage(result.Message, new ToastrOptions
                    {
                        Title = "Uğurlu əməliyyat",
                        CloseButton = true
                    });
                    return RedirectToAction("Index","Category",new { area = "Admin" });
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }
            }
            return View(videoUpdateViewModel);
        }
        [HttpPost]
        public async Task<JsonResult> Delete(int carBrendModelId)
        {
            var result = await _categoryService.Delete(carBrendModelId, LoggedInUser.UserName);
            var deletedVideo = JsonSerializer.Serialize(result);
            return Json(deletedVideo);
        }
    }
}
