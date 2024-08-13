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
    public class SliderController : BaseController
    {
        private readonly ISliderService _SliderService;
        private readonly IToastNotification _toastNotification;

        public SliderController(ISliderService SliderService, IToastNotification toastNotification, UserManager<User> userManager, IMapper mapper, IImageHelper imageHelper) : base(userManager, mapper, imageHelper)
        {
            _SliderService = SliderService;
            _toastNotification = toastNotification;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _SliderService.GetAllByNonDeletedAndActive();
            return View(result.Data);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(SliderAddViewModel sliderAddViewModel)
        {
            if (ModelState.IsValid)
            {
                var articleAddDto = Mapper.Map<SliderAddDto>(sliderAddViewModel);
                var imageResult = await ImageHelper.UploadImage(sliderAddViewModel.Name,
                    sliderAddViewModel.PictureFile, PictureType.Post);
                articleAddDto.ImageUrl = imageResult.Data.FullName;

                var result = await _SliderService.Add(articleAddDto);
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
            return View(sliderAddViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int sliderId)
        {
            var result = await _SliderService.GetSliderUpdateDto(sliderId);
            if (result.ResultStatus == ResultStatus.Succes)
            {
                var videoUpdateViewModel = Mapper.Map<SliderUpdateViewModel>(result.Data);
                return View(videoUpdateViewModel);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Update(SliderUpdateViewModel videoUpdateViewModel)
        {
            if (ModelState.IsValid)
            {

                var videoUpdateDto = Mapper.Map<SliderUpdateDto>(videoUpdateViewModel);
                var result = await _SliderService.Update(videoUpdateDto, LoggedInUser.UserName);
                if (result.ResultStatus == ResultStatus.Succes)
                {
                    _toastNotification.AddSuccessToastMessage(result.Message, new ToastrOptions
                    {
                        Title = "Uğurlu əməliyyat",
                        CloseButton = true
                    });
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }
            }
            return View(videoUpdateViewModel);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int sliderId)
        {
            var result = await _SliderService.HardDelete(sliderId);
            var deletedVideo = JsonSerializer.Serialize(result);
            return Json(deletedVideo);
        }
    }
}
