using RusGold.Shared.Entities.Abstract;
using RusGold.Shared.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusGold.Entities.Concrete
{
    public class Product:EntityBase,IEntity
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int UnitId { get; set; }
        public double PriceByCard { get; set; }
        public double Price { get; set; }
        public bool IsGold { get; set; }
        public string Content { get; set; }
        public string ThumbNail { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
		public Category Category { get; set; }
	}
}
