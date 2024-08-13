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
    public class CarUpdateViewModel
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public int ModelId { get; set; }
        public int BrendId { get; set; }
        public string Price { get; set; }
        public string HorsePower { get; set; }
        public string Content { get; set; }
        public string TechnicalParameters { get; set; }
        public string SeoDescription { get; set; }
        public string SeoTags { get; set; }
        public int UserId { get; set; }
        public string CreatedByName { get; set; }
        public string Body { get; set; }
        public string FuelType { get; set; }
        public string Year { get; set; }
        public string Transmission { get; set; }
        public string DriveType { get; set; }
        public string Color { get; set; }
        public string EngineSize { get; set; }
        [DisplayName("Şəkil")]
        [DataType(DataType.Upload)]
        public IFormFile PictureFile { get; set; }
        public string Thumbnail { get; set; }
        [DisplayName("Aktivdir ?")]
        [Required(ErrorMessage = "{0}  boş ola bilməz!")]
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public IFormFileCollection CarPhotos { get; set; }
        public IList<CarPhotos> Images { get; set; }
    }
}
