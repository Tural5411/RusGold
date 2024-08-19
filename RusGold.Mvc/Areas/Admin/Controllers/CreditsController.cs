//using AutoMapper;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using NToastNotify;
//using RusGold.Entities.Concrete;
//using RusGold.Entities.DTOs;
//using RusGold.Mvc.Areas.Admin.Helpers.Abstract;
//using RusGold.Mvc.Areas.Admin.Models;
//using RusGold.Services.Abstract;
//using RusGold.Shared.Utilities.Results.ComplexTypes;
//using System.Text.Json;
//using System.Threading.Tasks;
//using System.Linq;

//namespace RusGold.Mvc.Areas.Admin.Controllers
//{
//    [Area("Admin")]
//    public class CreditsController : BaseController
//    {
//        private readonly ICreditService _creditService;
//        private readonly IToastNotification _toastNotification;
//        private readonly ICategoryService _carbrendModelService;

//        public CreditsController(ICategoryService carbrendModelService,ICreditService creditService, IToastNotification toastNotification, UserManager<User> userManager, IMapper mapper, IImageHelper imageHelper) : base(userManager, mapper, imageHelper)
//        {
//            _creditService = creditService;
//            _toastNotification = toastNotification;
//            _carbrendModelService = carbrendModelService;
//        }

//        public async Task<IActionResult> Index(CreditListDto list)
//        {
//            var models = _carbrendModelService.GetAllByNonDeletedAndActive().Result.Data.CarBrendModels;
//            var result = await _creditService.GetAllByNonDeletedAndActive();
//            return View(new CreditListDto
//            {
//                Credits = result.Data.Credits
//            });
//        }
//        [HttpGet]
//        public IActionResult Add()
//        {
//            var result = _carbrendModelService.GetAllByNonDeletedAndActive();
//            ViewBag.brendModels = result.Result.Data.CarBrendModels.Where(x => x.ParentId > 0);
//            return View();
//        }
//        [HttpPost]
//        public async Task<IActionResult> Add(CreditAddDto carAddDto)
//        {
//            if (ModelState.IsValid)
//            {
//                var result = await _creditService.Add(carAddDto);
//                if (result.ResultStatus == ResultStatus.Succes)
//                {
//                    _toastNotification.AddSuccessToastMessage(result.Message, new ToastrOptions
//                    {
//                        Title = "Uğurlu əməliyyat"
//                    });
//                    return RedirectToAction("Index");
//                }
//                else
//                {
//                    ModelState.AddModelError("", result.Message);
//                }
//            }
//            var result1 = _carbrendModelService.GetAllByNonDeletedAndActive();
//            ViewBag.brendModels = result1.Result.Data.CarBrendModels.Where(x => x.ParentId > 0);
//            return View(carAddDto);
//        }
//        [HttpGet]
//        public async Task<IActionResult> Update(int creditId)
//        {
//            var result = await _creditService.GetCreditUpdateDto(creditId);
//            var result1 = _carbrendModelService.GetAll();
//            ViewBag.brendModels = result1.Result.Data.CarBrendModels.Where(x => x.ParentId > 0);
//            if (result.ResultStatus == ResultStatus.Succes)
//            {
//                var videoUpdateViewModel = Mapper.Map<CreditUpdateViewModel>(result.Data);
//                return View(videoUpdateViewModel);
//            }
//            return NotFound();
//        }
//        [HttpPost]
//        public async Task<IActionResult> Update(CreditUpdateViewModel videoUpdateViewModel)
//        {
//            if (ModelState.IsValid)
//            {
                
//                var videoUpdateDto = Mapper.Map<CreditUpdateDto>(videoUpdateViewModel);
//                var result = await _creditService.Update(videoUpdateDto);
//                if (result.ResultStatus == ResultStatus.Succes)
//                {
//                    _toastNotification.AddSuccessToastMessage(result.Message, new ToastrOptions
//                    {
//                        Title = "Uğurlu əməliyyat",
//                        CloseButton = true
//                    });
//                    return RedirectToAction("Index");
//                }
//                else
//                {
//                    ModelState.AddModelError("", result.Message);
//                }
//            }
//            var result1 = _carbrendModelService.GetAllByNonDeletedAndActive();
//            ViewBag.brendModels = result1.Result.Data.CarBrendModels.Where(x => x.ParentId > 0);
//            return View(videoUpdateViewModel);
//        }
//        [HttpPost]
//        public async Task<JsonResult> Delete(int carBrendModelId)
//        {
//            var result = await _creditService.Delete(carBrendModelId);
//            var deletedVideo = JsonSerializer.Serialize(result);
//            return Json(deletedVideo);
//        }
//    }
//}
