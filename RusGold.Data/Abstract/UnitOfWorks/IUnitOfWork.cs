using System;
using System.Threading.Tasks;

namespace RusGold.Data.Abstract.UnitOfWorks
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IProductRepository Products { get; }
        ICreditRepository Credits { get; }
        IRegisterRepository Registers { get; }
        IPhotoRepository Photos { get; }
        IQuestionRepository Questions { get; }
        ISliderRepository Sliders { get; }
        IArticleRepository Articles { get; }
        ICategoryRepository Categories { get; }
        Task<int> SaveAsync();
    }
}
