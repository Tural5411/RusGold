using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RusGold.Entities.Concrete;
using RusGold.Shared.Entities.Abstract;

namespace RusGold.Entities.DTOs
{
    public class CarBrendModelDto:DtoGetBase
    {
        public CarBrendModel CarBrendModel { get; set; }
    }
}
