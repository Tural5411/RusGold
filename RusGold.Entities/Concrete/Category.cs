using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RusGold.Shared.Entities.Concrete;

namespace RusGold.Entities.Concrete
{
    public class Category:EntityBase
    {
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Thumbnail { get; set; }
		public ICollection<Product> Products{ get; set; }
	}
}
