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
    public class CreditManager : ICreditService
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreditManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IDataResult<CreditDto>> Add(CreditAddDto CreditAddDto)
        {
            var Credit = _mapper.Map<Credits>(CreditAddDto);

            var addedCredit = await _unitOfWork.Credits.AddAsync(Credit);
            await _unitOfWork.SaveAsync();
            return new DataResult<CreditDto>(ResultStatus.Succes, Messages.Car.Add(addedCredit.MonthlyPay.ToString()), new CreditDto
            {
                Credit = addedCredit,
                ResultStatus = ResultStatus.Succes,
                Message = Messages.Car.Add(addedCredit.MonthlyPay.ToString())
            });
        }


        public async Task<IResult> Delete(int CreditId)
        {
            var result = await _unitOfWork.Credits.AnyAsync(a => a.Id == CreditId);
            if (result)
            {
                var article = await _unitOfWork.Credits.GetAsync(a => a.Id == CreditId);
                article.IsActive = false;
                article.IsDeleted = true;

                await _unitOfWork.Credits.UpdateAsync(article);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Succes, Messages.Car.Delete(article.MonthlyPay.ToString()));
            }
            return new Result(ResultStatus.Error, Messages.Car.NotFound(false));
        }

        public async Task<IDataResult<CreditDto>> Get(int CreditId)
        {
            var Credit = await _unitOfWork.Credits.GetAsync(c => c.Id == CreditId);
            if (Credit != null)
            {
                return new DataResult<CreditDto>(ResultStatus.Succes, new CreditDto
                {
                    Credit = Credit,
                    ResultStatus = ResultStatus.Succes
                });
            }
            else
            {
                return new DataResult<CreditDto>(ResultStatus.Error, Messages.Car.NotFound(isPlural: false), new CreditDto
                {
                    Credit = null,
                    ResultStatus = ResultStatus.Error,
                    Message = Messages.Car.NotFound(isPlural: false)
                });
            }
        }

        public async Task<IDataResult<CreditListDto>> GetAll()
        {
            var categories = await _unitOfWork.Credits.GetAllAsync(null);
            if (categories.Count > -1)
            {
                return new DataResult<CreditListDto>(ResultStatus.Succes, new CreditListDto
                {
                    Credits = categories,
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<CreditListDto>(ResultStatus.Error, Messages.Car.NotFound(isPlural: true),
                new CreditListDto
                {
                    Message = Messages.Car.NotFound(isPlural: true),
                    Credits = null,
                    ResultStatus = ResultStatus.Error
                });
        }

        public async Task<IDataResult<CreditListDto>> GetAllByNonDeletedAndActive()
        {
            var categories = await _unitOfWork.Credits.GetAllAsync(c => c.IsActive && !c.IsDeleted);
            if (categories.Count > -1)
            {
                return new DataResult<CreditListDto>(ResultStatus.Succes, new CreditListDto
                {
                    Credits = categories,
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<CreditListDto>(ResultStatus.Error, Messages.Car.NotFound(isPlural: true), null);
        }

        public async Task<IDataResult<CreditUpdateDto>> GetCreditUpdateDto(int CreditId)
        {
            var result = await _unitOfWork.Credits.AnyAsync(c => c.Id == CreditId);
            if (result)
            {
                var Credit = await _unitOfWork.Credits.GetAsync(c => c.Id == CreditId);
                var CreditUpdateDto = _mapper.Map<CreditUpdateDto>(Credit);
                return new DataResult<CreditUpdateDto>(ResultStatus.Succes, CreditUpdateDto);
            }
            else
            {
                return new DataResult<CreditUpdateDto>(ResultStatus.Error, Messages.Car.NotFound(isPlural: false), null);
            }
        }


        public async Task<IDataResult<CreditDto>> Update(CreditUpdateDto CreditUpdateDto)
        {
            var oldCredit = await _unitOfWork.Credits.GetAsync(c => c.Id == CreditUpdateDto.Id);
            var Credit = _mapper.Map<CreditUpdateDto, Credits>(CreditUpdateDto, oldCredit);
            if (Credit != null)
            {
                var updatedCredit = await _unitOfWork.Credits.UpdateAsync(Credit);
                await _unitOfWork.SaveAsync();
                return new DataResult<CreditDto>(ResultStatus.Succes, Messages.Car.Add(updatedCredit.MonthlyPay.ToString()), new CreditDto
                {
                    Credit = updatedCredit,
                    Message = Messages.Car.Add(updatedCredit.MonthlyPay.ToString()),
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<CreditDto>(ResultStatus.Error, message: "Xəta baş verdi", new CreditDto
            {
                Credit = null,
                Message = "Xəta baş verdi",
                ResultStatus = ResultStatus.Error
            });
        }
    }
}
