using RusGold.Shared.Entities.Abstract;
using RusGold.Shared.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusGold.Entities.Concrete
{
    public class CarPhotos : EntityBase, IEntity
    {
        public string ImageUrl { get; set; }
        public int CarId { get; set; }
        public virtual Car Car{ get; set; }
    }
}
