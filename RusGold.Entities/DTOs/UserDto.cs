﻿using RusGold.Shared.Entities.Abstract;
using RusGold.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusGold.Entities.DTOs
{
    public class UserDto:DtoGetBase
    {
        public User User { get; set; }
    }
}
