using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RusGold.Data.Abstract.UnitOfWorks;
using RusGold.Entities.ComplexTypes;
using RusGold.Entities.Concrete;
using RusGold.Entities.DTOs;
using RusGold.Services.Abstract;
using RusGold.Services.Utilities;
using RusGold.Shared.Utilities.Results.Abstract;
using RusGold.Shared.Utilities.Results.ComplexTypes;
using RusGold.Shared.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace RusGold.Services.Concrete
{
    public class PhotoManager : IPhotoService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        public PhotoManager(IMapper mapper, IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public async Task<IResult> Add(PhotoAddDto PhotoAddDto, string createdByName)
        {
            var Photo = _mapper.Map<CarPhotos>(PhotoAddDto);
            Photo.CreatedByName = createdByName;
            Photo.ModifiedByName = createdByName;
            Photo.IsActive = true;
            await _unitOfWork.Photos.AddAsync(Photo);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Succes, Messages.Article.Add(Photo.ImageUrl));
        }
        public async Task<IResult> HardDelete(int PhotoId)
        {
            var result = await _unitOfWork.Photos.AnyAsync(a => a.Id == PhotoId);
            if (result)
            {
                var Photo = await _unitOfWork.Photos.GetAsync(a => a.Id == PhotoId);

                await _unitOfWork.Photos.DeleteAsync(Photo);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Succes, Messages.Article.Add(Photo.ImageUrl));
            }
            return new Result(ResultStatus.Error, Messages.Article.NotFound(false));
        }
        public async Task<IDataResult<PhotoDto>> Get(int PhotoId)
        {
            var Photo = await _unitOfWork.Photos.GetAsync(a => a.Id == PhotoId, a => a.Product);
            if (Photo != null)
            {
                return new DataResult<PhotoDto>(ResultStatus.Succes, new PhotoDto
                {
                    Photo = Photo,
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<PhotoDto>(ResultStatus.Error, new PhotoDto
            {
                Photo = null,
                ResultStatus = ResultStatus.Error,
                Message = Messages.Article.NotFound(false)
            });
        }
        public async Task<IDataResult<PhotoListDto>> GetAllByNonDeletedAndActive(int projectId)
        {
            var Photos = await _unitOfWork.Photos.GetAllAsync(a=>a.ProductId==projectId,
                 a => a.Product);
            if (Photos.Count > -1)
            {
                return new DataResult<PhotoListDto>(ResultStatus.Succes, new PhotoListDto
                {
                    Photos = Photos,
                    ResultStatus = ResultStatus.Succes,
                });
            }
            return new DataResult<PhotoListDto>(ResultStatus.Error, Messages.Article.NotFound(false), null);
        }

        public Task<IDataResult<PhotoListDto>> GetAllByNonDeletedAndActiveV2(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
