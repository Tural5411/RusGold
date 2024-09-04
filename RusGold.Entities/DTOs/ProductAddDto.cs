using RusGold.Shared.Entities.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RusGold.Entities.Concrete;

namespace RusGold.Entities.DTOs
{
    public class ProductAddDto
    {
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public decimal? PriceByCard { get; set; }
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
