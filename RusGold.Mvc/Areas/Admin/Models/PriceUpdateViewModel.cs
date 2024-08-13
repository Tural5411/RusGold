﻿using Microsoft.AspNetCore.Http;
using RusGold.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RusGold.Mvc.Areas.Admin.Models
{
    public class PriceUpdateViewModel
    {
        [Required]
        public int Id { get; set; }
        [DisplayName("Başlıq")]
        [MaxLength(200, ErrorMessage = "{0} {1} - dən böyük ola bilməz!")]
        [MinLength(3, ErrorMessage = "{0} {1} - dən az ola bilməz!")]
        public string Header { get; set; }
        [DisplayName("Şəkil")]
        public string Icon { get; set; }
        [DisplayName("Kicik Resm Elave Et")]
        public IFormFile IconFile { get; set; }
        [DisplayName("Qiymet")]
        public string PriceValue { get; set; }
        [DisplayName("Kontent")]
        public string Content { get; set; }
        public string Text { get; set; }
        public string Text2 { get; set; }
        public string Text3 { get; set; }
        public string Text4 { get; set; }
        public string Text5 { get; set; }
        public int BusinessId { get; set; }
    }
}
