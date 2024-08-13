using RusGold.Shared.Concrete.EntityFramework;
using RusGold.Data.Abstract;
using RusGold.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RusGold.Data.Concrete.EntityFramework.Repositories
{
    public class PhotoRepository : EfEntityRepositoryBase<CarPhotos>, IPhotoRepository
    {
        public PhotoRepository(DbContext Context) : base(Context)
        {
        }
    }
}
