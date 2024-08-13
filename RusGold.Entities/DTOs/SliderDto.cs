using RusGold.Entities.Concrete;
using RusGold.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusGold.Entities.DTOs
{
    public class SliderDto:DtoGetBase
    {
        public Slider Slider { get; set; }
    }
}
