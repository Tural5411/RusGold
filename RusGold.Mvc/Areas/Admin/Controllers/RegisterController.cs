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
    public class RegisterController : BaseController
    {
        private readonly IRegisterService _RegisterService;
        private readonly IToastNotification _toastNotification;

        public RegisterController(IRegisterService RegisterService, IToastNotification toastNotification, UserManager<User> userManager, IMapper mapper, IImageHelper imageHelper) : base(userManager, mapper, imageHelper)
        {
            _RegisterService = RegisterService;
            _toastNotification = toastNotification;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _RegisterService.GetAllByNonDeleteAndActive();
            return View(result.Data);
        }
        
        [HttpPost]
        public async Task<JsonResult> Delete(int registerId)
        {
            var result = await _RegisterService.Delete(registerId, LoggedInUser.UserName);
            var deletedVideo = JsonSerializer.Serialize(result);
            return Json(deletedVideo);
        }
    }
}
