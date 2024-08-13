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
    public class RegisterManager : IRegisterService
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RegisterManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IDataResult<RegisterDto>> Add(RegisterAddDto teamAddDto, string createdByName)
        {
            try
            {
                var team = _mapper.Map<Registers>(teamAddDto);
                team.CreatedByName = createdByName;
                team.ModifiedByName = createdByName;
                team.IsActive = true;
                var addedteam = await _unitOfWork.Registers.AddAsync(team);
                await _unitOfWork.SaveAsync();
                return new DataResult<RegisterDto>(ResultStatus.Succes, Messages.Team.Add(addedteam.Fullname), new RegisterDto
                {
                    Team = addedteam,
                    ResultStatus = ResultStatus.Succes,
                    Message = Messages.Team.Add(addedteam.Fullname)
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }

        public async Task<IDataResult<RegisterDto>> Delete(int teamId, string modifiedByName)
        {
            var team = await _unitOfWork.Registers.GetAsync(c => c.Id == teamId);
            if (team != null)
            {
                team.IsActive = false;
                team.IsDeleted = true;
                team.ModifiedByName = modifiedByName;
                team.ModifiedDate = DateTime.Now;

                var deletedteam = await _unitOfWork.Registers.UpdateAsync(team);
                await _unitOfWork.SaveAsync();
                return new DataResult<RegisterDto>(ResultStatus.Succes, 
                    Messages.Team.Delete(deletedteam.Fullname), new RegisterDto
                    {
                        Team = deletedteam,
                        Message = Messages.Team.Delete(deletedteam.Fullname),
                        ResultStatus = ResultStatus.Succes
                    });
            }
            else
            {
                return new DataResult<RegisterDto>(ResultStatus.Error, Messages.Team.NotFound(isPlural: false), new RegisterDto
                    {
                        Team = null,
                        Message = Messages.Team.NotFound(isPlural: false),
                        ResultStatus = ResultStatus.Error
                    });
            }
        }

        public async Task<IDataResult<RegisterDto>> Get(int teamId)
        {
            var team =await _unitOfWork.Registers.GetAsync(c => c.Id == teamId);
            if (team != null)
            {
                return new DataResult<RegisterDto>(ResultStatus.Succes,new RegisterDto 
                {
                    Team =team,
                    ResultStatus=ResultStatus.Succes
                });
            }
            else
            {
                return new DataResult<RegisterDto>(ResultStatus.Error, Messages.Team.NotFound(isPlural:false), new RegisterDto { 
                Team=null,
                ResultStatus=ResultStatus.Error,
                Message= Messages.Team.NotFound(isPlural: false)
                });
            }
        }

        public async Task<IDataResult<RegisterListDto>> GetAll()
        {
            var Registers =await _unitOfWork.Registers.GetAllAsync(null);
            if (Registers.Count>-1)
            {
                return new DataResult<RegisterListDto>(ResultStatus.Succes,new RegisterListDto 
                {
                Registers=Registers,
                ResultStatus=ResultStatus.Succes
                });
            }
            return new DataResult<RegisterListDto>(ResultStatus.Error, Messages.Team.NotFound(isPlural: true),
                new RegisterListDto {
                    Message = Messages.Team.NotFound(isPlural: true),
                    Registers =null,
                    ResultStatus=ResultStatus.Error
                }) ;
        }

        public async Task<IDataResult<RegisterListDto>> GetAllByDelete()
        {
            var Registers = await _unitOfWork.Registers.GetAllAsync(c => c.IsDeleted);
            if (Registers.Count > -1)
            {
                return new DataResult<RegisterListDto>(ResultStatus.Succes, new RegisterListDto
                {
                    Registers = Registers,
                    ResultStatus=ResultStatus.Succes
                });
            }
            else
            {
                return new DataResult<RegisterListDto>(ResultStatus.Error, new RegisterListDto
                {
                    Registers = null,
                    ResultStatus = ResultStatus.Error,
                    Message=Messages.Team.NotFound(true)
                });
            }
        }

        public async Task<IDataResult<RegisterListDto>> GetAllByNonDelete()
        {                                                                   //==false
            var Registers = await _unitOfWork.Registers.GetAllAsync(c => !c.IsDeleted);
            if (Registers.Count > -1)
            {
                return new DataResult<RegisterListDto>(ResultStatus.Succes, new RegisterListDto 
                { 
                Registers =Registers,
                ResultStatus=ResultStatus.Succes
                });
            }
            else
            {
                return new DataResult<RegisterListDto>(ResultStatus.Error, Messages.Team.NotFound(isPlural: true), new RegisterListDto { 
                    Registers =null,
                    ResultStatus=ResultStatus.Error,
                    Message= Messages.Team.NotFound(isPlural: true)
                });
            }
        }

        public async Task<IDataResult<RegisterListDto>> GetAllByNonDeleteAndActive()
        {
            var Registers = await _unitOfWork.Registers.GetAllAsync(c => c.IsActive && !c.IsDeleted);
            if (Registers.Count > -1)
            {
                return new DataResult<RegisterListDto>(ResultStatus.Succes, new RegisterListDto
                {
                    Registers = Registers,
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<RegisterListDto>(ResultStatus.Error, Messages.Team.NotFound(isPlural: true), null);
        }

        public async Task<IDataResult<RegisterUpdateDto>> GetUpdateDto(int teamId)
        {
            var result = await _unitOfWork.Registers.AnyAsync(c => c.Id == teamId);
            if (result)
            {
                var team = await _unitOfWork.Registers.GetAsync(c => c.Id == teamId);
                var teamUpdateDto = _mapper.Map<RegisterUpdateDto>(team);
                return new DataResult<RegisterUpdateDto>(ResultStatus.Succes, teamUpdateDto);
            }
            else
            {
                return new DataResult<RegisterUpdateDto>(ResultStatus.Error, Messages.Team.NotFound(isPlural: false), null);
            }
        }

        public async Task<IResult> HardDelete(int teamId)
        {
            var team = await _unitOfWork.Registers.GetAsync(c => c.Id == teamId);
            if (team != null)
            {
                await _unitOfWork.Registers.DeleteAsync(team);
                await _unitOfWork.SaveAsync();

                return new Result(ResultStatus.Succes, Messages.Team.HardDelete(team.Fullname));
            }
            else
            {
                return new Result(ResultStatus.Error, message:
                   $"{team.Fullname} adlı team silinə bilmədi, təkrar yoxlayın");
            }
        }

        public async Task<IDataResult<RegisterDto>> UndoDelete(int teamId, string modifiedByName)
        {
            var team = await _unitOfWork.Registers.GetAsync(c => c.Id == teamId);
            if (team != null)
            {
                team.IsDeleted = false;
                team.IsActive = true;
                team.ModifiedByName = modifiedByName;
                team.ModifiedDate = DateTime.Now;

                var deletedTeam = await _unitOfWork.Registers.UpdateAsync(team);
                await _unitOfWork.SaveAsync();
                return new DataResult<RegisterDto>(ResultStatus.Succes, new RegisterDto
                {
                    Team = deletedTeam,
                    Message = Messages.Team.UndoDelete(team.Fullname),
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<RegisterDto>(ResultStatus.Error, new RegisterDto
            {
                Team = null,
                Message = Messages.Team.NotFound(false),
                ResultStatus = ResultStatus.Error
            });
        }

        public async Task<IDataResult<RegisterDto>> Update(RegisterUpdateDto teamUpdateDto, string modifiedByName)
        {
            var oldTeam = await _unitOfWork.Registers.GetAsync(c => c.Id == teamUpdateDto.Id);
            var team =  _mapper.Map<RegisterUpdateDto, Registers>(teamUpdateDto, oldTeam);
            team.ModifiedByName = modifiedByName;
            if (team != null)
            {
                var updatedTeam=await _unitOfWork.Registers.UpdateAsync(team);
                await _unitOfWork.SaveAsync();
                return new DataResult<RegisterDto>(ResultStatus.Succes, Messages.Team.Add(updatedTeam.Fullname), new RegisterDto { 
                    Team= updatedTeam,
                    Message= Messages.Team.Add(updatedTeam.Fullname),
                    ResultStatus=ResultStatus.Succes
                    });
            }
                return new DataResult<RegisterDto>(ResultStatus.Error, message: "Xəta baş verdi", new RegisterDto
                {
                    Team = null,
                    Message = "Xəta baş verdi",
                    ResultStatus = ResultStatus.Error
                });
        }
    }
}
