using RusGold.Entities.DTOs;
using RusGold.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusGold.Services.Abstract
{
    public interface IProductService
    {
        Task<IDataResult<ProductDto>> Get(int CarId);
        Task<IDataResult<ProductUpdateDto>> GetUpdateDto(int CarId);
        Task<IDataResult<ProductListDto>> GetAll();
        Task<IDataResult<ProductListDto>> GetAllByNonDeleteAndActive();
        Task<IDataResult<ProductListDto>> GetAllByPaging(int? categoryId,
         int currentPage = 1, int pageSize = 4, bool isAscending = false);
        Task<IDataResult<ProductListDto>> SearchAsync(string keyword, int currentPage = 1,
            int pageSize = 4, bool isAscending = false);
        Task<IDataResult<ProductDto>> Add(ProductAddDto CarAddDto, string createdByName);
        Task<IDataResult<ProductDto>> Update(ProductUpdateDto CarUpdateDto, string modifiedByName);
        Task<IResult> Delete(int CarId, string modifiedByName);
        Task<IResult> HardDelete(int CarId);
    }
}
