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
    public class CarManager : ICarService
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CarManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IDataResult<CarDto>> Add(CarAddDto CarAddDto, string createdByName)
        {
            var Car = _mapper.Map<Car>(CarAddDto);
            Car.CreatedByName = "RusGold";
            Car.ModifiedByName = "RusGold";
            Car.CreatedDate= DateTime.Now;
            Car.ModifiedDate= DateTime.Now;
            var addedCar = await _unitOfWork.Cars.AddAsync(Car);
            await _unitOfWork.SaveAsync();
            return new DataResult<CarDto>(ResultStatus.Succes, Messages.Car.Add(addedCar.Name), new CarDto
            {
                Car = addedCar,
                ResultStatus = ResultStatus.Succes,
                Message = Messages.Car.Add(addedCar.Name)
            });
        }
        public async Task<IDataResult<CarDto>> Update(CarUpdateDto CarUpdateDto, string modifiedByName)
        {
            var oldCar = await _unitOfWork.Cars.GetAsync(c => c.Id == CarUpdateDto.Id);
            var Car = _mapper.Map<CarUpdateDto, Car>(CarUpdateDto, oldCar);
            Car.CreatedByName = "RusGold";
            Car.ModifiedByName = "RusGold";
            Car.CreatedDate = DateTime.Now;
            Car.ModifiedDate = DateTime.Now;
            if (Car != null)
            {
                var updatedCar = await _unitOfWork.Cars.UpdateAsync(Car);
                await _unitOfWork.SaveAsync();
                return new DataResult<CarDto>(ResultStatus.Succes, Messages.Car.Add(updatedCar.Name), new CarDto
                {
                    Car = updatedCar,
                    Message = Messages.Car.Add(updatedCar.Name),
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<CarDto>(ResultStatus.Error, message: "Xəta baş verdi", new CarDto
            {
                Car = null,
                Message = "Xəta baş verdi",
                ResultStatus = ResultStatus.Error
            });
        }
        public async Task<IDataResult<CarUpdateDto>> GetUpdateDto(int CarId)
        {
            var result = await _unitOfWork.Cars.AnyAsync(c => c.Id == CarId);
            if (result)
            {
                var Car = await _unitOfWork.Cars.GetAsync(c => c.Id == CarId);
                var CarUpdateDto = _mapper.Map<CarUpdateDto>(Car);
                await _unitOfWork.Cars.UpdateAsync(Car);
                await _unitOfWork.SaveAsync();
                return new DataResult<CarUpdateDto>(ResultStatus.Succes, CarUpdateDto);
            }
            else
            {
                return new DataResult<CarUpdateDto>(ResultStatus.Error, Messages.Car.NotFound(isPlural: false), null);
            }
        }
        public async Task<IResult> HardDelete(int CarId)
        {
            var Car = await _unitOfWork.Cars.GetAsync(c => c.Id == CarId);
            if (Car != null)
            {
                await _unitOfWork.Cars.DeleteAsync(Car);
                await _unitOfWork.SaveAsync();

                return new Result(ResultStatus.Succes, Messages.Car.HardDelete(Car.Name));
            }
            else
            {
                return new Result(ResultStatus.Error, message:
                   $"{Car.Name} adlı Car silinə bilmədi, təkrar yoxlayın");
            }
        }
        public async Task<IResult> Delete(int carId, string modifiedByName)
        {
            var result = await _unitOfWork.Cars.AnyAsync(a => a.Id == carId);
            if (result)
            {
                var article = await _unitOfWork.Cars.GetAsync(a => a.Id == carId);
                article.IsActive = false;
                article.IsDeleted = true;
                article.ModifiedByName = modifiedByName;
                article.ModifiedDate = DateTime.Now;

                await _unitOfWork.Cars.UpdateAsync(article);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Succes, Messages.Car.Delete(article.Name));
            }
            return new Result(ResultStatus.Error, Messages.Car.NotFound(false));
        }
        public async Task<IDataResult<CarDto>> Get(int CarId)
        {
            var Car = await _unitOfWork.Cars.GetAsync(c => c.Id == CarId);
            if (Car != null)
            {
                return new DataResult<CarDto>(ResultStatus.Succes, new CarDto
                {
                    Car = Car,
                    ResultStatus = ResultStatus.Succes
                });
            }
            else
            {
                return new DataResult<CarDto>(ResultStatus.Error, Messages.Car.NotFound(isPlural: false), new CarDto
                {
                    Car = null,
                    ResultStatus = ResultStatus.Error,
                    Message = Messages.Car.NotFound(isPlural: false)
                });
            }
        }
        public async Task<IDataResult<CarListDto>> GetAll()
        {
            var Car = await _unitOfWork.Cars.GetAllAsync(null);
            if (Car.Count > -1)
            {
                return new DataResult<CarListDto>(ResultStatus.Succes, new CarListDto
                {
                    Cars = Car,
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<CarListDto>(ResultStatus.Error, Messages.Car.NotFound(isPlural: true),
                new CarListDto
                {
                    Message = Messages.Car.NotFound(isPlural: true),
                    Cars = null,
                    ResultStatus = ResultStatus.Error
                });
        }
        public async Task<IDataResult<CarListDto>> GetAllByNonDeleteAndActive()
        {
            var Car = await _unitOfWork.Cars.GetAllAsync(c => c.IsActive && !c.IsDeleted);
            if (Car.Count > -1)
            {
                return new DataResult<CarListDto>(ResultStatus.Succes, new CarListDto
                {
                    Cars = Car,
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<CarListDto>(ResultStatus.Error, Messages.Car.NotFound(isPlural: true), null);
        }
        public async Task<IDataResult<CarListDto>> GetAllByPaging(int? brendId, int? modelId, int currentPage = 1,
            int pageSize = 4, bool isAscending = false)
        {
            pageSize = pageSize > 20 ? 20 : pageSize;
            var cars = await _unitOfWork.Cars.GetAllAsync(a => a.IsActive && !a.IsDeleted, a => a.User);

            if (brendId != null)
            {
                cars = cars.Where(a => a.BrendId == brendId).ToList();
            }
            if (modelId != null)
            {
                cars = cars.Where(a => a.ModelId == modelId).ToList();
            }
            cars = isAscending
                ? cars.OrderBy(a => a.Id).ToList()
                : cars.OrderByDescending(a => a.Id).ToList();

            var paginatedCars = cars.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            return new DataResult<CarListDto>(ResultStatus.Succes, new CarListDto
            {
                Cars = paginatedCars,
                BrendId = brendId == null ? null : brendId.Value,
                ModelId = modelId == null ? null : modelId.Value,
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalCount = cars.Count,
                ResultStatus = ResultStatus.Succes,
                IsAscending = false
            });
        }
    }
}
