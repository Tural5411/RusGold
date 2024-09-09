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
    public class ProductViewModel
    {
        [Display(Name = "Məhsulun Adı")]
        public string Name { get; set; }

        [Display(Name = "Qiymət")]
        public float? Price { get; set; }

        [Display(Name = "Kartla Qiymət")]
        public float? PriceByCard { get; set; }

        [Display(Name = "Məzmun")] public string Content { get; set; } = "Content";

        [Display(Name = "Qızıl Məhsul")]
        public bool? IsGold { get; set; }

        [Display(Name = "Şəkil")]
        public string ThumbNail { get; set; }

        [Display(Name = "SEO Təsviri")]
        public string SeoDescription { get; set; }

        [Display(Name = "SEO Açar Sözləri")]
        public string SeoTags { get; set; }

        [Display(Name = "Kateqoriya")]
        public int? CategoryId { get; set; }

        [Display(Name = "Vahid")]
        public int? UnitId { get; set; }

        [Display(Name = "İstifadəçi İd")]
        public int UserId { get; set; }

        [DisplayName("Şəkil")]
        [Required(ErrorMessage = "Zəhmət olmasa {0} seçin")]
        [DataType(DataType.Upload)]
        public IFormFile PictureFile { get; set; }
        public string Thumbnail { get; set; }
        [DisplayName("Aktivdir ?")]
        [Required(ErrorMessage = "{0}  boş ola bilməz!")]
        public bool IsActive { get; set; }
        public IFormFileCollection CarPhotos { get; set; }
        public IList<PhotoAddViewModel> Photos { get; set; }
    }
}
