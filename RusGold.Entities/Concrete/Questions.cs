using RusGold.Shared.Entities.Abstract;
using RusGold.Shared.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusGold.Entities.Concrete
{
    public class Questions : EntityBase, IEntity
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
