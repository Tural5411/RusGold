using RusGold.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusGold.Entities.DTOs
{
    public class PhotoAddDto
    {
        [DisplayName("Şəkillər")]
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "{0} boş olmamalıdır.")]
        public int CarId { get; set; }
    }
}
