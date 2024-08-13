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
    public class CarAddViewModel
    {
        [DisplayName("Ad")]
        public string Name { get; set; }
        [DisplayName("Model")]
        public int ModelId { get; set; }
        [DisplayName("Brend")]
        public int BrendId { get; set; }

        [DisplayName("Qiymət")]
        public string Price { get; set; }

        [DisplayName("At Gücü")]
        public string HorsePower { get; set; }

        [DisplayName("Məzmun")]
        public string Content { get; set; }

        [DisplayName("Texniki Parametrlər")]
        public string TechnicalParameters { get; set; }

        [DisplayName("Şəkil")]
        public string ThumbNail { get; set; }

        [DisplayName("SEO Təsviri")]
        public string SeoDescription { get; set; }

        [DisplayName("SEO Etiketləri")]
        public string SeoTags { get; set; }

        [DisplayName("İstifadəçi ID")]
        public int UserId { get; set; }

        [DisplayName("Yaradanın Adı")]
        public string CreatedByName { get; set; }

        [DisplayName("Bədən Tipi")]
        public string Body { get; set; }

        [DisplayName("Yanacaq Növü")]
        public string FuelType { get; set; }

        [DisplayName("İl")]
        public string Year { get; set; }

        [DisplayName("Transmissiya")]
        public string Transmission { get; set; }

        [DisplayName("Sürüş Tipi")]
        public string DriveType { get; set; }

        [DisplayName("Rəng")]
        public string Color { get; set; }

        [DisplayName("Mühərrik Həcmi")]
        public string EngineSize { get; set; }
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
        public IList<CarBrendModel> CarModels { get; set; }
        public IList<CarBrendModel> CarBrends { get; set; }
    }
}
