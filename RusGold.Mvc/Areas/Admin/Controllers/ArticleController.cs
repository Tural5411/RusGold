﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using RusGold.Shared.Utilities.Results.ComplexTypes;
using RusGold.Entities.ComplexTypes;
using RusGold.Entities.Concrete;
using RusGold.Entities.DTOs;
using RusGold.Mvc.Areas.Admin.Helpers.Abstract;
using RusGold.Mvc.Areas.Admin.Models;
using RusGold.Services.Abstract;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RusGold.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticleController : BaseController
    {
        private readonly IArticleService _articleService;
        private readonly IToastNotification _toastNotification;

        public ArticleController(IArticleService articleService, 
            IMapper mapper, UserManager<User> userManager, IImageHelper imageHelper, IToastNotification toastNotification) : base(userManager, mapper, imageHelper)
        {
            _articleService = articleService;
            _toastNotification = toastNotification;
        }
        [Authorize(Roles = "SuperAdmin,Article.Read")]
        public async Task<IActionResult> Index()
        {
            var result = await _articleService.GetAllByNonDeletedAndActive();
            if (result.ResultStatus == ResultStatus.Succes) return View(result.Data);
            return NotFound();

        }
        [Authorize(Roles = "SuperAdmin,Article.Create")]
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }
        [Authorize(Roles = "SuperAdmin,Article.Create")]
        [HttpPost]
        public async Task<IActionResult> Add(ArticleAddViewModel articleAddViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var articleAddDto = Mapper.Map<ArticleAddDto>(articleAddViewModel);
                    var imageResult = await ImageHelper.UploadImage(articleAddViewModel.Title,
                        articleAddViewModel.ThumbnailFile, PictureType.Post);
                    articleAddDto.Thumbnail = imageResult.Data.FullName;
                    var result = await _articleService.Add(articleAddDto, LoggedInUser.UserName, LoggedInUser.Id);
                    if (result.ResultStatus == ResultStatus.Succes)
                    {
                        _toastNotification.AddSuccessToastMessage(result.Message, new ToastrOptions
                        {
                            Title = "Uğurlu əməliyyat"
                        });
                    }
                    else
                    {
                        ModelState.AddModelError("", result.Message);
                    }
                }
                catch (Exception ex)
                {

                    return View(ex.ToString());
                }

            }
            return View(articleAddViewModel);
        }
        [Authorize(Roles = "SuperAdmin,Article.Update")]
        [HttpGet]
        public async Task<IActionResult> Update(int articleId)
        {
            var articleResult = await _articleService.GetArticleUpdateDto(articleId);
            if (articleResult.ResultStatus == ResultStatus.Succes)
            {
                var articleUpdateViewModel = Mapper.Map<ArticleUpdateViewModel>(articleResult.Data);
                return View(articleUpdateViewModel);
            }
            return NotFound();
        }
        [Authorize(Roles = "SuperAdmin,Article.Update")]
        [HttpPost]
        public async Task<IActionResult> Update(ArticleUpdateViewModel articleUpdateViewModel)
        {
            if (ModelState.IsValid)
            {
                bool isNewThumbnailUploaded = false;
                var oldThumbnail = articleUpdateViewModel.Thumbnail;
                if (articleUpdateViewModel.ThumbnailFile != null)
                {
                    var uploadedImageResult = await ImageHelper.UploadImage(articleUpdateViewModel.Title,
                        articleUpdateViewModel.ThumbnailFile, PictureType.Post);
                    articleUpdateViewModel.Thumbnail = uploadedImageResult.ResultStatus
                        == ResultStatus.Succes ? uploadedImageResult.Data.FullName
                        : "postImages/defaultThumbnail.jpg";
                    if (oldThumbnail != "postImages/defaultThumbnail.jpg")
                    {
                        isNewThumbnailUploaded = true;
                    }
                }
                var articleUpdateDto = Mapper.Map<ArticleUpdateDto>(articleUpdateViewModel);
                var result = await _articleService.Update(articleUpdateDto, LoggedInUser.UserName);
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
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }
            }
            return View(articleUpdateViewModel);
        }
        [Authorize(Roles = "SuperAdmin,Article.Delete")]
        [HttpPost]
        public async Task<JsonResult> Delete(int articleId)
        {
            var result = await _articleService.Delete(articleId, LoggedInUser.UserName);
            var deletedArticle = JsonSerializer.Serialize(result);
            return Json(deletedArticle);
        }
        [Authorize(Roles = "SuperAdmin,Article.Read")]
        [HttpGet]
        public async Task<JsonResult> GetAllArticles()
        {
            var result = await _articleService.GetAllByNonDeletedAndActive();
            var articles = JsonSerializer.Serialize(result.Data, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(articles);
        }
        [Authorize(Roles = "SuperAdmin,Article.Read")]
        [HttpGet]
        public async Task<IActionResult> DeletedArticles()
        {
            var result = await _articleService.GetAllByDeleted();
            return View(result.Data);

        }
        [Authorize(Roles = "SuperAdmin,Article.Read")]
        [HttpGet]
        public async Task<JsonResult> GetAllDeletedArticles()
        {
            var result = await _articleService.GetAllByDeleted();
            var articles = JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(articles);
        }
        [Authorize(Roles = "SuperAdmin,Article.Update")]
        [HttpPost]
        public async Task<JsonResult> UndoDelete(int articleId)
        {
            var result = await _articleService.UndoDelete(articleId, LoggedInUser.UserName);
            var undoDeleteArticleResult = JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(undoDeleteArticleResult);
        }
        [Authorize(Roles = "SuperAdmin,Article.Delete")]
        [HttpPost]
        public async Task<JsonResult> HardDelete(int articleId)
        {
            var result = await _articleService.HardDelete(articleId);
            var hardDeletedArticleResult = JsonSerializer.Serialize(result);
            return Json(hardDeletedArticleResult);
        }
        //[Authorize(Roles = "SuperAdmin,Article.Read")]
        //[HttpGet]
        //public async Task<JsonResult> GetAllByViewCount(bool isAscending, int takeSize)
        //{
        //    var result = await _articleService.GetAllByViewCount();
        //    var articles = JsonSerializer.Serialize(result.Data.Articles, new JsonSerializerOptions
        //    {
        //        ReferenceHandler = ReferenceHandler.Preserve
        //    });
        //    return Json(articles);
        //}
    }
}
