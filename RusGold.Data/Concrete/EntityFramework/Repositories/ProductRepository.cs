using RusGold.Data.Abstract;
using RusGold.Entities.Concrete;
using RusGold.Shared.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace RusGold.Data.Concrete.EntityFramework.Repositories
{
    public class ProductRepository : EfEntityRepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(DbContext Context) : base(Context)
        {
        }
    }
}
