using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RusGold.Shared.Entities.Concrete;

namespace RusGold.Entities.Concrete
{
    public class Credits : EntityBase
    {
        public int ModelId { get; set; }
        public int Period { get; set; }
        public decimal MonthlyPay { get; set; }
        public decimal InitialPayment { get; set; }
        public decimal CarPrice { get; set; }
        [NotMapped]
        public override string CreatedByName { get; set; }
        [NotMapped]
        public override DateTime ModifiedDate { get; set; } = DateTime.Now;
        [NotMapped]
        public override DateTime CreatedDate { get; set; } = DateTime.Now;
        [NotMapped]
        public override string ModifiedByName { get; set; }
    }
}
