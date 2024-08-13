using RusGold.Entities.Concrete;
using RusGold.Shared.Entities.Abstract;
using System.Collections.Generic;

namespace RusGold.Entities.DTOs
{
    public class CarListDto:DtoGetBase
    {
        public IList<Car> Cars { get; set; }
        public int? BrendId { get; set; }
        public int? ModelId { get; set; }
    }
}
