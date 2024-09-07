using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using RusGold.Entities.Concrete;
using System.ComponentModel.DataAnnotations.Schema;

namespace RusGold.Mvc.Areas.Admin.Models
{
    public class ProductUpdateViewModel
    {
        [Required]
        public int Id { get; set; }
        [Display(Name = "Məhsulun Adı")]
        public string Name { get; set; }

        [Display(Name = "Qiymət")]
        public float? Price { get; set; }

        [Display(Name = "Kartla Qiymət")]
        public float? PriceByCard { get; set; }

        [Display(Name = "Məzmun")]
        public string Content { get; set; }

        [Display(Name = "Qızıl Məhsul")]
        public bool? IsGold { get; set; }

        [Display(Name = "Şəkil")]
        public string Thumbnail { get; set; }

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
        [DataType(DataType.Upload)]
        public IFormFile PictureFile { get; set; }
        [DisplayName("Aktivdir ?")]
        [Required(ErrorMessage = "{0}  boş ola bilməz!")]
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public IFormFileCollection CarPhotos { get; set; }
        public IList<CarPhotos> Images { get; set; }
    }
}
