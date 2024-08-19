using RusGold.Entities.Concrete;
using RusGold.Shared.Entities.Abstract;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RusGold.Entities.DTOs
{
    public class RegisterAddDto:DtoGetBase
    {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }
        [DisplayName("Aktivdir ?")]
        [Required(ErrorMessage = "{0}  boş ola bilməz!")]
        public bool IsActive { get; set; }
    }
}
