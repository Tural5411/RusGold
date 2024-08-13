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
    public class QuestionManager : IQuestionService
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public QuestionManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IDataResult<QuestionDto>> Add(QuestionAddDto questionAddDto, string createdByName)
        {
            var question = _mapper.Map<Questions>(questionAddDto);
            question.CreatedByName = createdByName;
            question.ModifiedByName = createdByName;
            var addedquestion = await _unitOfWork.Questions.AddAsync(question);
            await _unitOfWork.SaveAsync();
            return new DataResult<QuestionDto>(ResultStatus.Succes, Messages.Car.Add(addedquestion.Answer), new QuestionDto
            {
                Question = addedquestion,
                ResultStatus = ResultStatus.Succes,
                Message = Messages.Car.Add(addedquestion.Answer)
            });
        }


        public async Task<IResult> Delete(int questionId, string modifiedByName)
        {
            var result = await _unitOfWork.Questions.AnyAsync(a => a.Id == questionId);
            if (result)
            {
                var article = await _unitOfWork.Questions.GetAsync(a => a.Id == questionId);
                article.IsActive = false;
                article.IsDeleted = true;
                article.ModifiedByName = modifiedByName;
                article.ModifiedDate = DateTime.Now;

                await _unitOfWork.Questions.UpdateAsync(article);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Succes, Messages.Car.Delete(article.Answer));
            }
            return new Result(ResultStatus.Error, Messages.Car.NotFound(false));
        }

        public async Task<IDataResult<QuestionDto>> Get(int questionId)
        {
            var question = await _unitOfWork.Questions.GetAsync(c => c.Id == questionId);
            if (question != null)
            {
                return new DataResult<QuestionDto>(ResultStatus.Succes, new QuestionDto
                {
                    Question = question,
                    ResultStatus = ResultStatus.Succes
                });
            }
            else
            {
                return new DataResult<QuestionDto>(ResultStatus.Error, Messages.Car.NotFound(isPlural: false), new QuestionDto
                {
                    Question = null,
                    ResultStatus = ResultStatus.Error,
                    Message = Messages.Car.NotFound(isPlural: false)
                });
            }
        }

        public async Task<IDataResult<QuestionListDto>> GetAll()
        {
            var categories = await _unitOfWork.Questions.GetAllAsync(null);
            if (categories.Count > -1)
            {
                return new DataResult<QuestionListDto>(ResultStatus.Succes, new QuestionListDto
                {
                    Questions = categories,
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<QuestionListDto>(ResultStatus.Error, Messages.Car.NotFound(isPlural: true),
                new QuestionListDto
                {
                    Message = Messages.Car.NotFound(isPlural: true),
                    Questions = null,
                    ResultStatus = ResultStatus.Error
                });
        }

        public async Task<IDataResult<QuestionListDto>> GetAllByNonDeletedAndActive()
        {
            var categories = await _unitOfWork.Questions.GetAllAsync(c => c.IsActive && !c.IsDeleted);
            if (categories.Count > -1)
            {
                return new DataResult<QuestionListDto>(ResultStatus.Succes, new QuestionListDto
                {
                    Questions = categories,
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<QuestionListDto>(ResultStatus.Error, Messages.Car.NotFound(isPlural: true), null);
        }

        public async Task<IDataResult<QuestionUpdateDto>> GetQuestionUpdateDto(int questionId)
        {
            var result = await _unitOfWork.Questions.AnyAsync(c => c.Id == questionId);
            if (result)
            {
                var question = await _unitOfWork.Questions.GetAsync(c => c.Id == questionId);
                var questionUpdateDto = _mapper.Map<QuestionUpdateDto>(question);
                return new DataResult<QuestionUpdateDto>(ResultStatus.Succes, questionUpdateDto);
            }
            else
            {
                return new DataResult<QuestionUpdateDto>(ResultStatus.Error, Messages.Car.NotFound(isPlural: false), null);
            }
        }


        public async Task<IDataResult<QuestionDto>> Update(QuestionUpdateDto questionUpdateDto, string modifiedByName)
        {
            var oldquestion = await _unitOfWork.Questions.GetAsync(c => c.Id == questionUpdateDto.Id);
            var question = _mapper.Map<QuestionUpdateDto, Questions>(questionUpdateDto, oldquestion);
            question.ModifiedByName = modifiedByName;
            if (question != null)
            {
                var updatedquestion = await _unitOfWork.Questions.UpdateAsync(question);
                await _unitOfWork.SaveAsync();
                return new DataResult<QuestionDto>(ResultStatus.Succes, Messages.Car.Add(updatedquestion.Answer), new QuestionDto
                {
                    Question = updatedquestion,
                    Message = Messages.Car.Add(updatedquestion.Answer),
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<QuestionDto>(ResultStatus.Error, message: "Xəta baş verdi", new QuestionDto
            {
                Question = null,
                Message = "Xəta baş verdi",
                ResultStatus = ResultStatus.Error
            });
        }
    }
}
