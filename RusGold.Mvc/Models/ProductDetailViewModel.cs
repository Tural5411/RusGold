using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RusGold.Entities.DTOs;

namespace RusGold.Mvc.Models
{
    public class ProductDetailViewModel
    {
        public ProductDto ProductDto { get; set; }
        public PhotoListDto ProductPhotos { get; set; }
    }
}
