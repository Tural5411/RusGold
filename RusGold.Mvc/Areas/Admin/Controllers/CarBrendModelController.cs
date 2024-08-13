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
    public class CarBrendModelController : BaseController
    {
        private readonly ICarBrendModelService _carService;
        private readonly IToastNotification _toastNotification;

        public CarBrendModelController(ICarBrendModelService carService, IToastNotification toastNotification, UserManager<User> userManager, IMapper mapper, IImageHelper imageHelper) : base(userManager, mapper, imageHelper)
        {
            _carService = carService;
            _toastNotification = toastNotification;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _carService.GetAllByNonDeletedAndActive();
            return View(result.Data);
        }
        [HttpGet]
        public IActionResult Add()
        {
            var result = _carService.GetAllByNonDeletedAndActive();
            ViewBag.brendModels = result.Result.Data.CarBrendModels.Where(x=>x.ParentId==0);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(CarBrendModelAddDto carAddDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _carService.Add(carAddDto, LoggedInUser.UserName);
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
            return View(carAddDto);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int carBrendModelId)
        {
            var result = await _carService.GetCarBrendModelUpdateDto(carBrendModelId);
            if (result.ResultStatus == ResultStatus.Succes)
            {
                var videoUpdateViewModel = Mapper.Map<CarBrendModelUpdateViewModel>(result.Data);
                videoUpdateViewModel.Id = carBrendModelId;
                return View(videoUpdateViewModel);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Update(CarBrendModelUpdateViewModel videoUpdateViewModel)
        {
            if (ModelState.IsValid)
            {
                
                var videoUpdateDto = Mapper.Map<CarBrendModelUpdateDto>(videoUpdateViewModel);
                var result = await _carService.Update(videoUpdateDto, LoggedInUser.UserName);
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
        public async Task<JsonResult> Delete(int carBrendModelId)
        {
            var result = await _carService.Delete(carBrendModelId, LoggedInUser.UserName);
            var deletedVideo = JsonSerializer.Serialize(result);
            return Json(deletedVideo);
        }
    }
}
