using RusGold.Shared.Entities.Abstract;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RusGold.Entities.DTOs
{
    public class SliderAddDto : DtoGetBase
    {
        [DisplayName("Başlıq")]
        [MaxLength(60, ErrorMessage = "{0} {1} - dən böyük ola bilməz!")]
        [MinLength(3, ErrorMessage = "{0} {1} - dən az ola bilməz!")]
        public string Name { get; set; }
        [DisplayName("Şəkil")]
        [Required(ErrorMessage = "Zəhmət olmasa {0} seçin")]
        [DataType(DataType.Upload)]
        public IFormFile PictureFile { get; set; }
        public string ImageUrl { get; set; }
        [DisplayName("Aktivdir ?")]
        [Required(ErrorMessage = "{0}  boş ola bilməz!")]
        public bool IsActive { get; set; }
        [DisplayName("Qiymet ?")]
        [Required(ErrorMessage = "{0}  boş ola bilməz!")]
        public string CreatedByName { get; set; }
    }
}
