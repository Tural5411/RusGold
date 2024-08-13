using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RusGold.Data.Abstract.UnitOfWorks;
using RusGold.Entities.ComplexTypes;
using RusGold.Entities.Concrete;
using RusGold.Entities.DTOs;
using RusGold.Services.Abstract;
using RusGold.Services.Utilities;
using RusGold.Shared.Utilities.Results.Abstract;
using RusGold.Shared.Utilities.Results.ComplexTypes;
using RusGold.Shared.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace RusGold.Services.Concrete
{
    public class ArticleManager : IArticleService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        public ArticleManager(IMapper mapper, IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public async Task<IResult> Add(ArticleAddDto articleAddDto, string createdByName, int userId)
        {
            var article = _mapper.Map<Article>(articleAddDto);
            article.CreatedByName = createdByName;
            article.ModifiedByName = createdByName;
            article.UserId = userId;
            await _unitOfWork.Articles.AddAsync(article);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Succes, Messages.Article.Add(article.Title));
        }
        public async Task<IResult> Update(ArticleUpdateDto articleUpdateDto, string modifiedByName)
        {
            var oldArticle = await _unitOfWork.Articles.GetAsync(a => a.Id == articleUpdateDto.Id);
            var article = _mapper.Map<ArticleUpdateDto, Article>(articleUpdateDto, oldArticle);
            article.ModifiedByName = modifiedByName;
            article.CreatedByName = modifiedByName;
            await _unitOfWork.Articles.UpdateAsync(article);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Succes, Messages.Article.Update(article.Title));
        }
        public async Task<IResult> Delete(int articleId, string modifiedByName)
        {
            var result = await _unitOfWork.Articles.AnyAsync(a => a.Id == articleId);
            if (result)
            {
                var article = await _unitOfWork.Articles.GetAsync(a => a.Id == articleId);
                article.IsActive = false;
                article.IsDeleted = true;
                article.ModifiedByName = modifiedByName;
                article.ModifiedDate = DateTime.Now;

                await _unitOfWork.Articles.UpdateAsync(article);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Succes, Messages.Article.Delete(article.Title));
            }
            return new Result(ResultStatus.Error, Messages.Article.NotFound(false));
        }
        public async Task<IResult> HardDelete(int articleId)
        {
            var result = await _unitOfWork.Articles.AnyAsync(a => a.Id == articleId);
            if (result)
            {
                var article = await _unitOfWork.Articles.GetAsync(a => a.Id == articleId);

                await _unitOfWork.Articles.DeleteAsync(article);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Succes, Messages.Article.Delete(article.Title));
            }
            return new Result(ResultStatus.Error, Messages.Article.NotFound(false));
        }
        public async Task<IDataResult<ArticleDto>> UndoDelete(int articleId, string modifiedByName)
        {
            var article = await _unitOfWork.Articles.GetAsync(a => a.Id == articleId);
            if (article != null)
            {
                article.IsActive = true;
                article.IsDeleted = false;
                article.ModifiedByName = modifiedByName;
                article.ModifiedDate = DateTime.Now;

                await _unitOfWork.Articles.UpdateAsync(article);
                await _unitOfWork.SaveAsync();
                return new DataResult<ArticleDto>(ResultStatus.Succes, new ArticleDto
                {
                    Article = article,
                    ResultStatus = ResultStatus.Succes,
                    Message = Messages.Article.UndoDelete(article.Title)
                });
            }
            return new DataResult<ArticleDto>(ResultStatus.Error, new ArticleDto
            {
                Article = null,
                Message = Messages.Article.UndoDelete(article.Title),
                ResultStatus = ResultStatus.Error
            });
        }

        public async Task<IDataResult<int>> Count()
        {
            var articlesCount = await _unitOfWork.Articles.CountAsync(x => x.IsActive);
            if (articlesCount > -1)
            {
                return new DataResult<int>(ResultStatus.Succes, articlesCount);
            }
            return new DataResult<int>(ResultStatus.Error, Messages.Article.NotFound(false), -1);
        }

        public async Task<IDataResult<int>> CountByNonDeleted()
        {
            var articlesCount = await _unitOfWork.Articles.CountAsync(x => !x.IsDeleted);
            if (articlesCount > -1)
            {
                return new DataResult<int>(ResultStatus.Succes, articlesCount);
            }
            return new DataResult<int>(ResultStatus.Error, Messages.Article.NotFound(false), -1);
        }



        public async Task<IDataResult<ArticleDto>> Get(int articleId)
        {
            var article = await _unitOfWork.Articles.GetAsync(a => a.Id == articleId, a => a.User);
            if (article != null)
            {
                return new DataResult<ArticleDto>(ResultStatus.Succes, new ArticleDto
                {
                    Article = article,
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<ArticleDto>(ResultStatus.Error, new ArticleDto
            {
                Article = null,
                ResultStatus = ResultStatus.Error,
                Message = Messages.Article.NotFound(false)
            });
        }

        public async Task<IDataResult<ArticleListDto>> GetAll()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync(null);
            if (articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Succes, new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, new ArticleListDto
            {
                Articles = null,
                ResultStatus = ResultStatus.Error,
                Message = Messages.Article.NotFound(isPlural: true)
            });
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByCategory(int categoryId)
        {
            var result = await _unitOfWork.Articles.AnyAsync(a => a.Id == categoryId);
            var booleanResult = Convert.ToBoolean(result);
            if (booleanResult)
            {
                var articles = await _unitOfWork.Articles.GetAllAsync(a => !a.IsDeleted  &&
                 a.IsActive, a => a.User);
                if (articles.Count > -1)
                {
                    return new DataResult<ArticleListDto>(ResultStatus.Succes, new ArticleListDto
                    {
                        Articles = articles,
                        ResultStatus = ResultStatus.Succes
                    });
                }
                return new DataResult<ArticleListDto>(ResultStatus.Error, new ArticleListDto
                {
                    Articles = null,
                    ResultStatus = ResultStatus.Error,
                    Message = Messages.Article.NotFound(isPlural: true)
                });
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, message: Messages.Article.NotFound(false), null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByDeleted()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync(a => a.IsDeleted, a => a.User);
            if (articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Succes, new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Succes,
                });
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, Messages.Article.NotFound(false), null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByNonDeletedAndActive()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync(a => !a.IsDeleted && a.IsActive,
                a => a.User);
            if (articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Succes, new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Succes,
                });
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, Messages.Article.NotFound(false), null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByPaging(int? categoryId, int currentPage = 1,
            int pageSize = 4, bool isAscending = false)
        {
            pageSize = pageSize > 20 ? 20 : pageSize;
            var articles = categoryId == null
                ? await _unitOfWork.Articles.GetAllAsync(a => a.IsActive && !a.IsDeleted,  a => a.User)
                : await _unitOfWork.Articles.GetAllAsync(a => a.IsActive && !a.IsDeleted,  a => a.User);
            var sortedArticles = isAscending ? articles.OrderBy(a => a.Id).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList()
                : articles.OrderByDescending(a => a.Id).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            return new DataResult<ArticleListDto>(ResultStatus.Succes, new ArticleListDto
            {
                Articles = sortedArticles,
                CategoryId = categoryId == null ? null : categoryId.Value,
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalCount = articles.Count,
                ResultStatus = ResultStatus.Succes,
                IsAscending = false
            });
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByViewCount(int takeSize = 3)
        {
            var articles = await _unitOfWork.Articles.GetAllAsync(a => !a.IsDeleted && a.IsActive);
            var sortedArticles = articles.OrderByDescending(a => a.CreatedDate);
            return new DataResult<ArticleListDto>(ResultStatus.Succes, new ArticleListDto
            {
                Articles = sortedArticles.Take(takeSize).ToList(),
                ResultStatus = ResultStatus.Succes
            });
        }

        public async Task<IDataResult<ArticleListDto>> GetAllNonDeleted()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync(a => !a.IsDeleted, a => a.User);
            if (articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Succes, new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Succes,
                });
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, Messages.Article.NotFound(false), null);
        }


        public async Task<IDataResult<ArticleUpdateDto>> GetArticleUpdateDto(int articleId)
        {
            var result = await _unitOfWork.Articles.AnyAsync(a => a.Id == articleId);
            if (result)
            {
                var article = await _unitOfWork.Articles.GetAsync(c => c.Id == articleId);
                var articleUpdateDto = _mapper.Map<ArticleUpdateDto>(article);
                return new DataResult<ArticleUpdateDto>(ResultStatus.Succes, articleUpdateDto);
            }
            else
            {
                return new DataResult<ArticleUpdateDto>(ResultStatus.Error, Messages.Article.NotFound(isPlural: false), null);
            }
        }


        public async Task<IResult> IncreaseViewCount(int articleId)
        {
            var article = await _unitOfWork.Articles.GetAsync(a => a.Id == articleId);
            if (article != null)
            {
                article.ViewsCount += 1;
                await _unitOfWork.Articles.UpdateAsync(article);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Succes, Messages.Article.IncreaseViewCount(article.Title));
            }
            return new Result(ResultStatus.Error, Messages.Article.NotFound(false));
        }

        public async Task<IDataResult<ArticleListDto>> SearchAsync(string keyword, int currentPage = 1, int pageSize = 4, bool isAscending = false)
        {
            pageSize = pageSize > 20 ? 20 : pageSize;
            if (string.IsNullOrWhiteSpace(keyword))
            {
                var articles = await _unitOfWork.Articles.GetAllAsync(a => a.IsActive && !a.IsDeleted, a => a.User);
                var sortedArticles = isAscending
                    ? articles.OrderBy(a => a.Id).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList()
                    : articles.OrderByDescending(a => a.Id).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                return new DataResult<ArticleListDto>(ResultStatus.Succes, new ArticleListDto
                {
                    Articles = sortedArticles,
                    CurrentPage = currentPage,
                    PageSize = pageSize,
                    ResultStatus = ResultStatus.Succes,
                    IsAscending = false
                });
            }
            var searchedArticles = await _unitOfWork.Articles.SearchAsync(
                new List<Expression<Func<Article, bool>>>
                {
                    (a)=>a.Title.Contains(keyword),
                    (a)=>a.SeoDescription.Contains(keyword),
                    (a)=>a.SeoTags.Contains(keyword)
                }, a => a.User);
            var searchedAndSortedArticles = isAscending
                ? searchedArticles.OrderBy(a => a.Id).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList()
                : searchedArticles.OrderByDescending(a => a.Id).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            return new DataResult<ArticleListDto>(ResultStatus.Succes, new ArticleListDto
            {
                Articles = searchedAndSortedArticles,
                CurrentPage = currentPage,
                PageSize = pageSize,
                ResultStatus = ResultStatus.Succes,
                IsAscending = false
            });
        }
    }
}
