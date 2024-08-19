using Microsoft.AspNetCore.Http;
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
    public class ProductUpdateDto
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public float? Price { get; set; }
        public float? PriceByCard { get; set; }
        public string Content { get; set; }
        public bool? IsGold { get; set; }
        public string ThumbNail { get; set; }
        public string SeoDescription { get; set; }
        public string SeoTags { get; set; }
        public int? CategoryId { get; set; }
        public int? UnitId { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }
    }
}
