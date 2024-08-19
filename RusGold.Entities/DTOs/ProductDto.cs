using RusGold.Entities.Concrete;
using RusGold.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusGold.Entities.DTOs
{
    public class ProductDto:DtoGetBase
    {
        public Product Product{ get; set; }
    }
}
