using RusGold.Entities.DTOs;
using RusGold.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusGold.Services.Abstract
{
    public interface IRegisterService
    {
        Task<IDataResult<RegisterDto>> Get(int RegisterId);
        Task<IDataResult<RegisterUpdateDto>> GetUpdateDto(int RegisterId);
        Task<IDataResult<RegisterListDto>> GetAll();
        Task<IDataResult<RegisterListDto>> GetAllByNonDeleteAndActive();
        Task<IDataResult<RegisterDto>> Add(RegisterAddDto RegisterAddDto, string createdByName);
        Task<IDataResult<RegisterDto>> Update(RegisterUpdateDto RegisterUpdateDto, string modifiedByName);
        Task<IDataResult<RegisterDto>> Delete(int RegisterId, string modifiedByName);
    }
}
