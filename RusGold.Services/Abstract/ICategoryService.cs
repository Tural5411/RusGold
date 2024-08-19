using RusGold.Entities.ComplexTypes;
using RusGold.Entities.DTOs;
using RusGold.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusGold.Services.Abstract
{
    public interface ICategoryService
    {
        Task<IDataResult<CategoryDto>> Get(int CarBrendModelId);
        Task<IDataResult<CategoryListDto>> GetAll();
        Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAndActive();
        Task<IDataResult<CategoryUpdateDto>> GetCategoryUpdateDto(int CarBrendModelId);
        Task<IDataResult<CategoryDto>> Add(CategoryAddDto CarBrendModelAddDto, string createdByName);
        Task<IDataResult<CategoryDto>> Update(CategoryUpdateDto CarBrendModelUpdateDto, string modifiedByName);
        Task<IResult> Delete(int CarBrendModelId, string modifiedByName);
    }
}
