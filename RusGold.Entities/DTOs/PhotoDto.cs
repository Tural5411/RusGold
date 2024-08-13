using RusGold.Shared.Entities.Abstract;
using RusGold.Shared.Utilities.Results.ComplexTypes;
using RusGold.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusGold.Entities.DTOs
{
    public class PhotoDto : DtoGetBase
    {
        public CarPhotos Photo { get; set; }
    }
}
