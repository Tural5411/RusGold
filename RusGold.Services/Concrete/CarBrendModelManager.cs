using AutoMapper;
using RusGold.Shared.Utilities.Results.Abstract;
using RusGold.Shared.Utilities.Results.ComplexTypes;
using RusGold.Shared.Utilities.Results.Concrete;
using RusGold.Data.Abstract.UnitOfWorks;
using RusGold.Entities.Concrete;
using RusGold.Entities.DTOs;
using RusGold.Services.Abstract;
using RusGold.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusGold.Services.Concrete
{
    public class CarBrendModelManager : ICarBrendModelService
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CarBrendModelManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IDataResult<CarBrendModelDto>> Add(CarBrendModelAddDto CarBrendModelAddDto, string createdByName)
        {
            var CarBrendModel = _mapper.Map<CarBrendModel>(CarBrendModelAddDto);
            CarBrendModel.Description = CarBrendModelAddDto.Name;
            CarBrendModel.IsActive = true;
            CarBrendModel.CreatedByName = createdByName;
            CarBrendModel.ModifiedByName = createdByName;
            var addedCarBrendModel = await _unitOfWork.CarBrendModels.AddAsync(CarBrendModel);
            await _unitOfWork.SaveAsync();
            return new DataResult<CarBrendModelDto>(ResultStatus.Succes, Messages.Car.Add(addedCarBrendModel.Name), new CarBrendModelDto
            {
                CarBrendModel = addedCarBrendModel,
                ResultStatus = ResultStatus.Succes,
                Message = Messages.Car.Add(addedCarBrendModel.Name)
            });
        }


        public async Task<IResult> Delete(int CarBrendModelId, string modifiedByName)
        {
            var result = await _unitOfWork.CarBrendModels.AnyAsync(a => a.Id == CarBrendModelId);
            if (result)
            {
                var article = await _unitOfWork.CarBrendModels.GetAsync(a => a.Id == CarBrendModelId);
                article.IsActive = false;
                article.IsDeleted = true;
                article.ModifiedByName = modifiedByName;
                article.ModifiedDate = DateTime.Now;

                await _unitOfWork.CarBrendModels.UpdateAsync(article);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Succes, Messages.Car.Delete(article.Name));
            }
            return new Result(ResultStatus.Error, Messages.Car.NotFound(false));
        }

        public async Task<IDataResult<CarBrendModelDto>> Get(int CarBrendModelId)
        {
            var CarBrendModel = await _unitOfWork.CarBrendModels.GetAsync(c => c.Id == CarBrendModelId);
            if (CarBrendModel != null)
            {
                return new DataResult<CarBrendModelDto>(ResultStatus.Succes, new CarBrendModelDto
                {
                    CarBrendModel = CarBrendModel,
                    ResultStatus = ResultStatus.Succes
                });
            }
            else
            {
                return new DataResult<CarBrendModelDto>(ResultStatus.Error, Messages.Car.NotFound(isPlural: false), new CarBrendModelDto
                {
                    CarBrendModel = null,
                    ResultStatus = ResultStatus.Error,
                    Message = Messages.Car.NotFound(isPlural: false)
                });
            }
        }

        public async Task<IDataResult<CarBrendModelListDto>> GetAll()
        {
            var categories = await _unitOfWork.CarBrendModels.GetAllAsync(null);
            if (categories.Count > -1)
            {
                return new DataResult<CarBrendModelListDto>(ResultStatus.Succes, new CarBrendModelListDto
                {
                    CarBrendModels = categories,
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<CarBrendModelListDto>(ResultStatus.Error, Messages.Car.NotFound(isPlural: true),
                new CarBrendModelListDto
                {
                    Message = Messages.Car.NotFound(isPlural: true),
                    CarBrendModels = null,
                    ResultStatus = ResultStatus.Error
                });
        }

        public async Task<IDataResult<CarBrendModelListDto>> GetAllByNonDeletedAndActive()
        {
            var categories = await _unitOfWork.CarBrendModels.GetAllAsync(c => c.IsActive && !c.IsDeleted);
            if (categories.Count > -1)
            {
                return new DataResult<CarBrendModelListDto>(ResultStatus.Succes, new CarBrendModelListDto
                {
                    CarBrendModels = categories,
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<CarBrendModelListDto>(ResultStatus.Error, Messages.Car.NotFound(isPlural: true), null);
        }

        public async Task<IDataResult<CarBrendModelUpdateDto>> GetCarBrendModelUpdateDto(int CarBrendModelId)
        {
            var result = await _unitOfWork.CarBrendModels.AnyAsync(c => c.Id == CarBrendModelId);
            if (result)
            {
                var CarBrendModel = await _unitOfWork.CarBrendModels.GetAsync(c => c.Id == CarBrendModelId);
                var CarBrendModelUpdateDto = _mapper.Map<CarBrendModelUpdateDto>(CarBrendModel);
                return new DataResult<CarBrendModelUpdateDto>(ResultStatus.Succes, CarBrendModelUpdateDto);
            }
            else
            {
                return new DataResult<CarBrendModelUpdateDto>(ResultStatus.Error, Messages.Car.NotFound(isPlural: false), null);
            }
        }


        public async Task<IDataResult<CarBrendModelDto>> Update(CarBrendModelUpdateDto CarBrendModelUpdateDto, string modifiedByName)
        {
            var oldCarBrendModel = await _unitOfWork.CarBrendModels.GetAsync(c => c.Id == CarBrendModelUpdateDto.Id);
            var CarBrendModel = _mapper.Map<CarBrendModelUpdateDto, CarBrendModel>(CarBrendModelUpdateDto, oldCarBrendModel);
            CarBrendModel.ModifiedByName = modifiedByName;
            CarBrendModel.CreatedByName = modifiedByName;
            if (CarBrendModel != null)
            {
                var updatedCarBrendModel = await _unitOfWork.CarBrendModels.UpdateAsync(CarBrendModel);
                await _unitOfWork.SaveAsync();
                return new DataResult<CarBrendModelDto>(ResultStatus.Succes, Messages.Car.Add(updatedCarBrendModel.Name), new CarBrendModelDto
                {
                    CarBrendModel = updatedCarBrendModel,
                    Message = Messages.Car.Add(updatedCarBrendModel.Name),
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<CarBrendModelDto>(ResultStatus.Error, message: "Xəta baş verdi", new CarBrendModelDto
            {
                CarBrendModel = null,
                Message = "Xəta baş verdi",
                ResultStatus = ResultStatus.Error
            });
        }
    }
}
