using RusGold.Shared.Entities.Abstract;
using RusGold.Shared.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusGold.Entities.Concrete
{
    public class Registers : EntityBase, IEntity
    {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }
        public string Message { get; set; }
    }
}
