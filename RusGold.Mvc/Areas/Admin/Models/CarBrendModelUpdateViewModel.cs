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
    public class CategoryUpdateViewModel
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        [DisplayName("Şəkil")]
        [DataType(DataType.Upload)]
        public IFormFile PictureFile { get; set; }
        public string Thumbnail { get; set; }

    }

    public class CreditUpdateViewModel
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public int Period { get; set; }
        public decimal MonthlyPay { get; set; }
        public decimal CarPrice { get; set; }
        public decimal InitialPayment { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}

