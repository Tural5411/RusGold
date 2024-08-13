using RusGold.Entities.DTOs;
using RusGold.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusGold.Services.Abstract
{
    public interface ICarService
    {
        Task<IDataResult<CarDto>> Get(int CarId);
        Task<IDataResult<CarUpdateDto>> GetUpdateDto(int CarId);
        Task<IDataResult<CarListDto>> GetAll();
        Task<IDataResult<CarListDto>> GetAllByNonDeleteAndActive();
        Task<IDataResult<CarListDto>> GetAllByPaging(int? brendId,int? modelId,
         int currentPage = 1, int pageSize = 4, bool isAscending = false);
        Task<IDataResult<CarDto>> Add(CarAddDto CarAddDto, string createdByName);
        Task<IDataResult<CarDto>> Update(CarUpdateDto CarUpdateDto, string modifiedByName);
        Task<IResult> Delete(int CarId, string modifiedByName);
        Task<IResult> HardDelete(int CarId);
    }
}
