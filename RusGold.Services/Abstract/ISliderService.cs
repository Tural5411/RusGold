using RusGold.Entities.DTOs;
using RusGold.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusGold.Services.Abstract
{
    public interface ISliderService
    {
        Task<IDataResult<SliderDto>> Get(int sliderId);
        Task<IDataResult<SliderListDto>> GetAll();
        Task<IDataResult<SliderListDto>> GetAllByNonDeletedAndActive();
        Task<IDataResult<SliderDto>> Add(SliderAddDto sliderAddDto);
        Task<IDataResult<SliderListDto>> GetAllByPage(int pageSize = 4, int currentPage = 1, bool isAscending = false);
        Task<IDataResult<SliderUpdateDto>> GetSliderUpdateDto(int sliderId);
        Task<IDataResult<SliderDto>> Update(SliderUpdateDto sliderUpdateDto, string modifiedByName);
        Task<IResult> HardDelete(int sliderId);
    }
}
