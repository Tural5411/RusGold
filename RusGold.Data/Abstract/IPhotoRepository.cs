using RusGold.Entities.Concrete;
using RusGold.Shared.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusGold.Data.Abstract
{
    public interface IPhotoRepository : IEntityRepository<CarPhotos>
    {
    }
}
