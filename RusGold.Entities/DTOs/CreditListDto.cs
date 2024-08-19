using RusGold.Entities.Concrete;
using RusGold.Shared.Entities.Abstract;
using System.Collections.Generic;

namespace RusGold.Entities.DTOs
{
    public class CreditListDto : DtoGetBase
    {
        public IList<Credits> Credits { get; set; }
    }
}
