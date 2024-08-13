using RusGold.Data.Abstract;
using RusGold.Entities.Concrete;
using RusGold.Shared.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace RusGold.Data.Concrete.EntityFramework.Repositories
{
    public class CarRepository : EfEntityRepositoryBase<Car>, ICarRepository
    {
        public CarRepository(DbContext Context) : base(Context)
        {
        }
    }
}
