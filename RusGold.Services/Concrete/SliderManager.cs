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
    public class SliderManager : ISliderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SliderManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<SliderDto>> Add(SliderAddDto teamAddDto)
        {
            var team = _mapper.Map<Slider>(teamAddDto);
            team.ModifiedByName = "RusGold";
            var addedteam = await _unitOfWork.Sliders.AddAsync(team);
            await _unitOfWork.SaveAsync();
            return new DataResult<SliderDto>(ResultStatus.Succes, Messages.Team.Add(addedteam.Name), new SliderDto
            {
                Slider = addedteam,
                ResultStatus = ResultStatus.Succes,
                Message = Messages.Team.Add(addedteam.Name)
            });
        }

        public async Task<IDataResult<SliderDto>> Delete(int teamId, string modifiedByName)
        {
            var team = await _unitOfWork.Sliders.GetAsync(c => c.Id == teamId);
            if (team != null)
            {
                team.IsActive = false;
                team.IsDeleted = true;
                team.ModifiedByName = modifiedByName;
                team.ModifiedDate = DateTime.Now;

                var deletedteam = await _unitOfWork.Sliders.UpdateAsync(team);
                await _unitOfWork.SaveAsync();
                return new DataResult<SliderDto>(ResultStatus.Succes,
                    Messages.Team.Delete(deletedteam.Name), new SliderDto
                    {
                        Slider = deletedteam,
                        Message = Messages.Team.Delete(deletedteam.Name),
                        ResultStatus = ResultStatus.Succes
                    });
            }
            else
            {
                return new DataResult<SliderDto>(ResultStatus.Error, Messages.Team.NotFound(isPlural: false), new SliderDto
                {
                    Slider = null,
                    Message = Messages.Team.NotFound(isPlural: false),
                    ResultStatus = ResultStatus.Error
                });
            }
        }

        public async Task<IDataResult<SliderDto>> Get(int SliderId)
        {
            var Slider = await _unitOfWork.Sliders.GetAsync(c => c.Id == SliderId);
            if (Slider != null)
            {
                return new DataResult<SliderDto>(ResultStatus.Succes, new SliderDto
                {
                    Slider = Slider
                });
            }
            else
            {
                return new DataResult<SliderDto>(ResultStatus.Error, Messages.Team.NotFound(false), new SliderDto
                {
                    Slider = null
                });
            }
        }

        public async Task<IDataResult<SliderListDto>> GetAll()
        {
            var Sliders = await _unitOfWork.Sliders.GetAllAsync(null);
            if (Sliders.Count > -1)
            {
                return new DataResult<SliderListDto>(ResultStatus.Succes, new SliderListDto
                {
                    ResultStatus = ResultStatus.Succes,
                    Sliders = Sliders
                });
            }
            else
            {
                return new DataResult<SliderListDto>(ResultStatus.Error, new SliderListDto
                {
                    Sliders = null,
                    ResultStatus = ResultStatus.Error,
                    Message = Messages.Team.NotFound(false)
                });
            }
        }

        public async Task<IDataResult<SliderListDto>> GetAllByNonDeletedAndActive()
        {
            var Sliders = await _unitOfWork.Sliders.GetAllAsync(c => c.IsActive && !c.IsDeleted);
            if (Sliders.Count > -1)
            {
                return new DataResult<SliderListDto>(ResultStatus.Succes, new SliderListDto
                {
                    Sliders = Sliders,
                    ResultStatus = ResultStatus.Succes
                });
            }
            else
            {
                return new DataResult<SliderListDto>(ResultStatus.Error, new SliderListDto
                {
                    Sliders = null,
                    ResultStatus = ResultStatus.Error,
                    Message = Messages.Team.NotFound(isPlural: true)
                });
            }
        }

        public Task<IDataResult<SliderListDto>> GetAllByPage(int pageSize = 4, int currentPage = 1, bool isAscending = false)
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<SliderUpdateDto>> GetSliderUpdateDto(int SliderId)
        {
            var result = await _unitOfWork.Sliders.AnyAsync(c => c.Id == SliderId);
            if (result)
            {
                var Slider = await _unitOfWork.Sliders.GetAsync(c => c.Id == SliderId);
                var SliderUpdateDto = _mapper.Map<SliderUpdateDto>(Slider);
                return new DataResult<SliderUpdateDto>(ResultStatus.Succes, SliderUpdateDto);
            }
            else
            {
                return new DataResult<SliderUpdateDto>(ResultStatus.Error, Messages.Team.NotFound(isPlural: false), null);
            }
        }

        public Task<IResult> HardDelete(int sliderId)
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<SliderDto>> Update(SliderUpdateDto SliderUpdateDto, string modifiedByName)
        {
            var oldSlider = await _unitOfWork.Sliders.GetAsync(c => c.Id == SliderUpdateDto.Id);
            var Slider = _mapper.Map<SliderUpdateDto, Slider>(SliderUpdateDto, oldSlider);
            Slider.ModifiedByName = modifiedByName;
            var updatedSlider = await _unitOfWork.Sliders.UpdateAsync(Slider);

            await _unitOfWork.SaveAsync();
            return new DataResult<SliderDto>(ResultStatus.Succes, Messages.Team.Update(Slider.CreatedByName), new SliderDto
            {
                Slider = updatedSlider,
            });
        }
    }
}

