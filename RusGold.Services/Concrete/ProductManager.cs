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
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace RusGold.Services.Concrete
{
	public class ProductManager : IProductService
	{
		public readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
		private readonly IMapper _mapper;
		public ProductManager(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
		{
			_unitOfWork = unitOfWork;
            _configuration = configuration;
			_mapper = mapper;
		}
		public async Task<IDataResult<ProductDto>> Add(ProductAddDto CarAddDto, string createdByName)
		{
			var Car = _mapper.Map<Product>(CarAddDto);
			Car.CreatedByName = "RusGold";
			Car.ModifiedByName = "RusGold";
            Car.CreatedDate = DateTime.Now;
			Car.ModifiedDate = DateTime.Now;
			var addedCar = await _unitOfWork.Products.AddAsync(Car);
			await _unitOfWork.SaveAsync();
			return new DataResult<ProductDto>(ResultStatus.Succes, Messages.Car.Add(addedCar.Name), new ProductDto
			{
				Product = addedCar,
				ResultStatus = ResultStatus.Succes,
				Message = Messages.Car.Add(addedCar.Name)
			});
		}
		public async Task<IDataResult<ProductDto>> Update(ProductUpdateDto CarUpdateDto, string modifiedByName)
		{
			var oldCar = await _unitOfWork.Products.GetAsync(c => c.Id == CarUpdateDto.Id);
			var Car = _mapper.Map<ProductUpdateDto, Product>(CarUpdateDto, oldCar);
			Car.CreatedByName = "RusGold";
			Car.ModifiedByName = "RusGold";
			Car.CreatedDate = DateTime.Now;
			Car.ModifiedDate = DateTime.Now;
			if (Car != null)
			{
				var updatedCar = await _unitOfWork.Products.UpdateAsync(Car);
				await _unitOfWork.SaveAsync();
				return new DataResult<ProductDto>(ResultStatus.Succes, Messages.Car.Add(updatedCar.Name), new ProductDto
				{
					Product = updatedCar,
					Message = Messages.Car.Add(updatedCar.Name),
					ResultStatus = ResultStatus.Succes
				});
			}
			return new DataResult<ProductDto>(ResultStatus.Error, message: "Xəta baş verdi", new ProductDto
			{
				Product = null,
				Message = "Xəta baş verdi",
				ResultStatus = ResultStatus.Error
			});
		}
		public async Task<IDataResult<ProductUpdateDto>> GetUpdateDto(int CarId)
		{
			var result = await _unitOfWork.Products.AnyAsync(c => c.Id == CarId);
			if (result)
			{
				var Car = await _unitOfWork.Products.GetAsync(c => c.Id == CarId);
				var CarUpdateDto = _mapper.Map<ProductUpdateDto>(Car);
				await _unitOfWork.Products.UpdateAsync(Car);
				await _unitOfWork.SaveAsync();
				return new DataResult<ProductUpdateDto>(ResultStatus.Succes, CarUpdateDto);
			}
			else
			{
				return new DataResult<ProductUpdateDto>(ResultStatus.Error, Messages.Car.NotFound(isPlural: false), null);
			}
		}
		public async Task<IResult> HardDelete(int CarId)
		{
			var Car = await _unitOfWork.Products.GetAsync(c => c.Id == CarId);
			if (Car != null)
			{
				await _unitOfWork.Products.DeleteAsync(Car);
				await _unitOfWork.SaveAsync();

				return new Result(ResultStatus.Succes, Messages.Car.HardDelete(Car.Name));
			}
			else
			{
				return new Result(ResultStatus.Error, message:
				   $"{Car.Name} adlı Car silinə bilmədi, təkrar yoxlayın");
			}
		}
		public async Task<IResult> Delete(int carId, string modifiedByName)
		{
			var result = await _unitOfWork.Products.AnyAsync(a => a.Id == carId);
			if (result)
			{
				var article = await _unitOfWork.Products.GetAsync(a => a.Id == carId);
				article.IsActive = false;
				article.IsDeleted = true;
				article.ModifiedByName = modifiedByName;
				article.ModifiedDate = DateTime.Now;

				await _unitOfWork.Products.UpdateAsync(article);
				await _unitOfWork.SaveAsync();
				return new Result(ResultStatus.Succes, Messages.Car.Delete(article.Name));
			}
			return new Result(ResultStatus.Error, Messages.Car.NotFound(false));
		}
		public async Task<IDataResult<ProductDto>> Get(int CarId)
		{
			var Car = await _unitOfWork.Products.GetAsync(c => c.Id == CarId, a => a.Category);
			if (Car != null)
			{
				return new DataResult<ProductDto>(ResultStatus.Succes, new ProductDto
				{
					Product = Car,
					ResultStatus = ResultStatus.Succes
				});
			}
			else
			{
				return new DataResult<ProductDto>(ResultStatus.Error, Messages.Car.NotFound(isPlural: false), new ProductDto
				{
					Product = null,
					ResultStatus = ResultStatus.Error,
					Message = Messages.Car.NotFound(isPlural: false)
				});
			}
		}
		public async Task<IDataResult<ProductListDto>> GetAll()
		{
			var Car = await _unitOfWork.Products.GetAllAsync(null);
			if (Car.Count > -1)
			{
				return new DataResult<ProductListDto>(ResultStatus.Succes, new ProductListDto
				{
					Products = Car,
					ResultStatus = ResultStatus.Succes
				});
			}
			return new DataResult<ProductListDto>(ResultStatus.Error, Messages.Car.NotFound(isPlural: true),
				new ProductListDto
				{
					Message = Messages.Car.NotFound(isPlural: true),
					Products = null,
					ResultStatus = ResultStatus.Error
				});
		}
       
		public async Task<IDataResult<ProductListDto>> GetAllByNonDeleteAndActive()
		{
			var Car = await _unitOfWork.Products.GetAllAsync(c => c.IsActive && !c.IsDeleted, a => a.Category);
			if (Car.Count > -1)
			{
				return new DataResult<ProductListDto>(ResultStatus.Succes, new ProductListDto
				{
					Products = Car,
					ResultStatus = ResultStatus.Succes
				});
			}
			return new DataResult<ProductListDto>(ResultStatus.Error, Messages.Car.NotFound(isPlural: true), null);
		}

		public async Task<IDataResult<ProductListDto>> UpdateProductPricesBasedOnDollar()
        {
            try
            {
                var currentDollarRate = GetCurrentDollarRate();
                var oldDollarRate = GetPreviousDollarRate();

                var products = await _unitOfWork.Products.GetAllAsync(c => !c.IsDeleted, c=>c.IsActive, a => a.Category);

                if (products.Count > 0)
                {
                    foreach (var product in products)
                    {
                        //product.Price = product.Price * currentDollarRate;
                        product.Price = product.Price * (oldDollarRate / currentDollarRate);

						_ = _unitOfWork.Products.UpdateAsync(product);
					}

                    
                    await _unitOfWork.SaveAsync();

                    return new DataResult<ProductListDto>(ResultStatus.Succes, new ProductListDto
                    {
                        Products = products,
                        ResultStatus = ResultStatus.Succes
                    });
                }

                return new DataResult<ProductListDto>(ResultStatus.Error, Messages.Car.NotFound(isPlural: true), null);
            }
            catch (Exception ex)
            {
                return new DataResult<ProductListDto>(ResultStatus.Error, ex.Message, null);
            }
        }


		public decimal GetCurrentDollarRate()
        {
            var currentDollarRate = _configuration.GetValue<decimal>("ExchangePageInfo:DollarToRuble");
            return currentDollarRate;
        }

        public decimal GetPreviousDollarRate()
        {
            var oldDollarRate = _configuration.GetValue<decimal>("ExchangePageInfo:OldDollarToRuble");
            return oldDollarRate;
        }


		public async Task<IDataResult<ProductListDto>> SearchAsync(string keyword, int currentPage = 1, int pageSize = 4, bool isAscending = false)
        {
            pageSize = pageSize > 20 ? 20 : pageSize;
            if (string.IsNullOrWhiteSpace(keyword))
            {
                var products = await _unitOfWork.Products.GetAllAsync(p => p.IsActive && !p.IsDeleted, p => p.User, p => p.Category);
                var sortedProducts = isAscending
                    ? products.OrderBy(p => p.Id).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList()
                    : products.OrderByDescending(p => p.Id).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                return new DataResult<ProductListDto>(ResultStatus.Succes, new ProductListDto
                {
                    Products = sortedProducts,
                    CurrentPage = currentPage,
                    PageSize = pageSize,
                    ResultStatus = ResultStatus.Succes,
                    IsAscending = isAscending
                });
            }
            var searchedProducts = await _unitOfWork.Products.SearchAsync(
                new List<Expression<Func<Product, bool>>>
                {
                    (p) => p.Name.Contains(keyword),
                }, p => p.User, p => p.Category);
            var searchedAndSortedProducts = isAscending
                ? searchedProducts.OrderBy(p => p.Id).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList()
                : searchedProducts.OrderByDescending(p => p.Id).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            return new DataResult<ProductListDto>(ResultStatus.Succes, new ProductListDto
            {
                Products = searchedAndSortedProducts,
                CurrentPage = currentPage,
                PageSize = pageSize,
                ResultStatus = ResultStatus.Succes,
                IsAscending = isAscending
            });
        }

		public async Task<IDataResult<ProductListDto>> GetAllByPaging(int? categoryId, int currentPage = 1,int pageSize = 4, bool isAscending = false)
		{
			try
			{
				pageSize = pageSize > 20 ? 20 : pageSize;

				var Products = categoryId.HasValue
					? await _unitOfWork.Products.GetAllAsync(a => a.IsActive && !a.IsDeleted && a.CategoryId == categoryId,a=>a.Category)
					: await _unitOfWork.Products.GetAllAsync(a => a.IsActive && !a.IsDeleted, a => a.Category);

				Products = isAscending
					? Products.OrderBy(a => a.Id).ToList()
					: Products.OrderByDescending(a => a.Id).ToList();

				var paginatedProducts = Products.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

				return new DataResult<ProductListDto>(ResultStatus.Succes, new ProductListDto
				{
					Products = paginatedProducts,
					CurrentPage = currentPage,
					PageSize = pageSize,
					TotalCount = Products.Count,
					ResultStatus = ResultStatus.Succes,
					IsAscending = isAscending
				});
			}
			catch (Exception ex)
			{
				throw;
			}
		}

	}
}
