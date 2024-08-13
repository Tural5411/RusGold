using Microsoft.AspNetCore.Http;
using RusGold.Shared.Utilities.Results.Abstract;
using RusGold.Entities.ComplexTypes;
using RusGold.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RusGold.Mvc.Areas.Admin.Helpers.Abstract
{
    public interface IImageHelper
    {
        //string UploadImage(string name, 
        //    IFormFile pictureFile,PictureType pictureType, string folderName=null);

        Task<string> UploadImageV2(IFormFile file);
        Task<IDataResult<ImageUploadedDto>> UploadImage(string name,
            IFormFile pictureFile, PictureType pictureType, string folderName = null);
        IDataResult<ImageDeletedDto> ImageDelete(string PictureName);
    }
}
