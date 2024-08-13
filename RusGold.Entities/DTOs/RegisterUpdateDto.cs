using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusGold.Entities.DTOs
{
    public class RegisterUpdateDto
    {
        [Required]
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }
        public string Message { get; set; }
        [DisplayName("Aktivdir ?")]
        [Required(ErrorMessage = "{0}  boş ola bilməz!")]
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
