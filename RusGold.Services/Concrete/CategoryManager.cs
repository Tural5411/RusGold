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
using System.Threading.Tasks;

namespace RusGold.Services.Concrete
{
    public class CategoryManager : ICategoryService
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IDataResult<CategoryDto>> Add(CategoryAddDto CategoryAddDto, string createdByName)
        {
            var Category = _mapper.Map<Category>(CategoryAddDto);
            Category.Description = CategoryAddDto.Name;
            Category.IsActive = true;
            Category.CreatedByName = createdByName;
            Category.ModifiedByName = createdByName;
            var addedCategory = await _unitOfWork.Categories.AddAsync(Category);
            await _unitOfWork.SaveAsync();
            return new DataResult<CategoryDto>(ResultStatus.Succes, Messages.Car.Add(addedCategory.Name), new CategoryDto
            {
                Category = addedCategory,
                ResultStatus = ResultStatus.Succes,
                Message = Messages.Car.Add(addedCategory.Name)
            });
        }


        public async Task<IResult> Delete(int categoryId, string modifiedByName)
        {
            var result = await _unitOfWork.Categories.AnyAsync(a => a.Id == categoryId);
            if (result)
            {
                var article = await _unitOfWork.Categories.GetAsync(a => a.Id == categoryId);
                article.IsActive = false;
                article.IsDeleted = true;
                article.ModifiedByName = modifiedByName;
                article.ModifiedDate = DateTime.Now;

                await _unitOfWork.Categories.UpdateAsync(article);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Succes, Messages.Car.Delete(article.Name));
            }
            return new Result(ResultStatus.Error, Messages.Car.NotFound(false));
        }

        public async Task<IDataResult<CategoryDto>> Get(int CategoryId)
        {
            var Category = await _unitOfWork.Categories.GetAsync(c => c.Id == CategoryId);
            if (Category != null)
            {
                return new DataResult<CategoryDto>(ResultStatus.Succes, new CategoryDto
                {
                    Category = Category,
                    ResultStatus = ResultStatus.Succes
                });
            }
            else
            {
                return new DataResult<CategoryDto>(ResultStatus.Error, Messages.Car.NotFound(isPlural: false), new CategoryDto
                {
                    Category = null,
                    ResultStatus = ResultStatus.Error,
                    Message = Messages.Car.NotFound(isPlural: false)
                });
            }
        }

        public async Task<IDataResult<CategoryListDto>> GetAll()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(null);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Succes, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, Messages.Car.NotFound(isPlural: true),
                new CategoryListDto
                {
                    Message = Messages.Car.NotFound(isPlural: true),
                    Categories = null,
                    ResultStatus = ResultStatus.Error
                });
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAndActive()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(c => c.IsActive && !c.IsDeleted);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Succes, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, Messages.Car.NotFound(isPlural: true), null);
        }

        public async Task<IDataResult<CategoryUpdateDto>> GetCategoryUpdateDto(int categoryId)
        {
            var result = await _unitOfWork.Categories.AnyAsync(c => c.Id == categoryId);
            if (result)
            {
                var Category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
                var CategoryUpdateDto = _mapper.Map<CategoryUpdateDto>(Category);
                return new DataResult<CategoryUpdateDto>(ResultStatus.Succes, CategoryUpdateDto);
            }
            else
            {
                return new DataResult<CategoryUpdateDto>(ResultStatus.Error, Messages.Car.NotFound(isPlural: false), null);
            }
        }


        public async Task<IDataResult<CategoryDto>> Update(CategoryUpdateDto categoryUpdateDto, string modifiedByName)
        {
            var oldCategory = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryUpdateDto.Id);
            var category = _mapper.Map(categoryUpdateDto, oldCategory);
            category.ModifiedByName = modifiedByName;
            category.CreatedByName = modifiedByName;
            if (category != null)
            {
                var updatedCategory = await _unitOfWork.Categories.UpdateAsync(category);
                await _unitOfWork.SaveAsync();
                return new DataResult<CategoryDto>(ResultStatus.Succes, Messages.Car.Add(updatedCategory.Name), new CategoryDto
                {
                    Category = updatedCategory,
                    Message = Messages.Car.Add(updatedCategory.Name),
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<CategoryDto>(ResultStatus.Error, message: "Xəta baş verdi", new CategoryDto
            {
                Category = null,
                Message = "Xəta baş verdi",
                ResultStatus = ResultStatus.Error
            });
        }
    }
}
