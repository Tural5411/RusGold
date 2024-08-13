using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using RusGold.Entities.Concrete;

namespace RusGold.Mvc.Areas.Admin.Models
{
    public class SliderUpdateViewModel
    {
        [Required]
        public int Id { get; set; }
        [DisplayName("Başlıq")]
        [MaxLength(60, ErrorMessage = "{0} {1} - dən böyük ola bilməz!")]
        [MinLength(3, ErrorMessage = "{0} {1} - dən az ola bilməz!")]
        public string Name { get; set; }
        [DisplayName("Şəkil")]
        [DataType(DataType.Upload)]
        public IFormFile PictureFile { get; set; }
        public string ImageUrl { get; set; }
        [DisplayName("Aktivdir ?")]
        [Required(ErrorMessage = "{0}  boş ola bilməz!")]
        public bool IsActive { get; set; }
        [DisplayName("Silinib mi ?")]
        [Required(ErrorMessage = "{0}  boş ola bilməz!")]
        public bool IsDeleted { get; set; }
        [DisplayName("Qiymet ?")]
        [Required(ErrorMessage = "{0}  boş ola bilməz!")]
        public string CreatedByName { get; set; }
    }
}
