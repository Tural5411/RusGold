using RusGold.Entities.Concrete;
using RusGold.Shared.Entities.Abstract;
using System.Collections.Generic;

namespace RusGold.Entities.DTOs
{
    public class ProductListDto:DtoGetBase
    {
        public IList<Product> Products { get; set; }
    }
}
