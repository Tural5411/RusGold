using RusGold.Entities.Concrete;
using RusGold.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusGold.Entities.DTOs
{
    public class CreditDto : DtoGetBase
    {
        public Credits Credit { get; set; }
    }
}
