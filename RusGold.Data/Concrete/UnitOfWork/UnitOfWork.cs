using RusGold.Data.Abstract;
using RusGold.Data.Abstract.UnitOfWorks;
using RusGold.Data.Concrete.EntityFramework.Context;
using RusGold.Data.Concrete.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusGold.Data.Concrete.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RusGoldContext _context;
        private  ArticleRepository _articleRepository;
        private  SliderRepository _sliderRepository;
        private  PhotoRepository   _photoRepository;
        private  QuestionRepository _questionRepository;
        private  RegisterRepository _registerRepository;
        private  ProductRepository _productRepository;
        private  CreditsRepository _creditRepository;
        private  CategoryRepository _categoryRepository;

        public UnitOfWork(RusGoldContext context)
        {
            _context = context;
        }
        public IArticleRepository Articles => _articleRepository ??= new ArticleRepository(_context);
        public IProductRepository Products => _productRepository ??= new ProductRepository(_context);
        public ICreditRepository Credits => _creditRepository ??= new CreditsRepository(_context);
        public ICategoryRepository Categories => _categoryRepository ??= new CategoryRepository(_context);
        public IRegisterRepository Registers => _registerRepository ??= new RegisterRepository(_context);
        public IQuestionRepository Questions => _questionRepository ??= new QuestionRepository(_context);
        public ISliderRepository Sliders => _sliderRepository ??= new SliderRepository(_context);
        public IPhotoRepository Photos => _photoRepository ??= new PhotoRepository(_context);

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<int> SaveAsync()
        {
           return await  _context.SaveChangesAsync();
        }
    }
}
