using RusGold.Shared.Concrete.EntityFramework;
using RusGold.Data.Abstract;
using RusGold.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace RusGold.Data.Concrete.EntityFramework.Repositories
{
    public class SliderRepository : EfEntityRepositoryBase<Slider>, ISliderRepository
    {
        public SliderRepository(DbContext Context) : base(Context)
        {
        }
    }
}
