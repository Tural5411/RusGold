using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RusGold.Entities.Concrete;
using RusGold.Entities.DTOs;

namespace RusGold.Mvc.Models
{
    public class AboutViewModel
    {
        public AboutUsPageInfo AboutUsPageInfo { get; set; }
        public QuestionListDto Questions { get; set; }
    }
}
