using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusGold.Entities.DTOs
{
    public class ImageDeletedDto
    {
        public string Path { get; set; }
        public string FullName { get; set; }
        public string Extension { get; set; }
        public long Size { get; set; }
    }
}
