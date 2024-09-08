using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusGold.Entities.Concrete
{
    public class ExchangePageInfo
    {
        [DisplayName("Yeni Dollar-Rubl Mezennesi")]
        [Required(ErrorMessage = "{0} boş olmamalıdır.")]
        public string DollarToRuble { get; set; }
        [DisplayName("Əvvəlki Dollar-Rubl Mezennesi")]
        [Required(ErrorMessage = "{0} boş olmamalıdır.")]
        public string OldDollarToRuble { get; set; }
    }
}
