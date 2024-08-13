using RusGold.Shared.Concrete.EntityFramework;
using RusGold.Data.Abstract;
using RusGold.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace RusGold.Data.Concrete.EntityFramework.Repositories
{
    public class RegisterRepository : EfEntityRepositoryBase<Registers>, IRegisterRepository
    {
        public RegisterRepository(DbContext Context) : base(Context)
        {
        }
    }
}
