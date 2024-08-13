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
        private  CarRepository _carRepository;
        private  CreditsRepository _creditRepository;
        private  CarBrendModelRepository _carBrendModelRepository;

        public UnitOfWork(RusGoldContext context)
        {
            _context = context;
        }
        public IArticleRepository Articles => _articleRepository ??= new ArticleRepository(_context);
        public ICarRepository Cars => _carRepository ??= new CarRepository(_context);
        public ICreditRepository Credits => _creditRepository ??= new CreditsRepository(_context);
        public ICarBrendModelRepository CarBrendModels => _carBrendModelRepository ??= new CarBrendModelRepository(_context);
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
