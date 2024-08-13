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
    public class QuestionController: BaseController
    {
        private readonly IQuestionService _QuestionService;
        private readonly IToastNotification _toastNotification;

        public QuestionController(IQuestionService QuestionService, IToastNotification toastNotification, UserManager<User> userManager, IMapper mapper, IImageHelper imageHelper) : base(userManager, mapper, imageHelper)
        {
            _QuestionService = QuestionService;
            _toastNotification = toastNotification;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _QuestionService.GetAllByNonDeletedAndActive();
            return View(result.Data);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(QuestionAddViewModel QuestionAddViewModel)
        {
            if (ModelState.IsValid)
            {
                var articleAddDto = Mapper.Map<QuestionAddDto>(QuestionAddViewModel);

                var result = await _QuestionService.Add(articleAddDto, LoggedInUser.UserName);
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
            return View(QuestionAddViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int QuestionId)
        {
            var result = await _QuestionService.GetQuestionUpdateDto(QuestionId);
            if (result.ResultStatus == ResultStatus.Succes)
            {
                var videoUpdateViewModel = Mapper.Map<QuestionUpdateViewModel>(result.Data);
                return View(videoUpdateViewModel);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Update(QuestionUpdateViewModel videoUpdateViewModel)
        {
            if (ModelState.IsValid)
            {

                var videoUpdateDto = Mapper.Map<QuestionUpdateDto>(videoUpdateViewModel);
                var result = await _QuestionService.Update(videoUpdateDto, LoggedInUser.UserName);
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
        public async Task<JsonResult> Delete(int QuestionId)
        {
            var result = await _QuestionService.Delete(QuestionId,LoggedInUser.FirstName);
            var deletedVideo = JsonSerializer.Serialize(result);
            return Json(deletedVideo);
        }
    }
}
