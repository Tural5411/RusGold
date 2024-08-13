using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RusGold.Mvc.Areas.Admin.Models
{
    public class PhotoAddViewModel
    {
        [DisplayName("Şəkillər")]
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "{0} boş olmamalıdır.")]
        public int CarId { get; set; }
    }
}
