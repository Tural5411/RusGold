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
    public class Car:EntityBase,IEntity
    {
        public string Name { get; set; }
        public int ModelId { get; set; }
        public int BrendId { get; set; }
        public string Price { get; set; }
        public string HorsePower { get; set; }
        public string Content { get; set; }
        public string TechnicalParameters { get; set; }
        public string ThumbNail { get; set; }
        public string Body { get; set; }
        public string FuelType { get; set; }
        public string Year { get; set; }
        public string Transmission { get; set; }
        public string DriveType { get; set; }
        public string Color { get; set; }
        public string EngineSize { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
