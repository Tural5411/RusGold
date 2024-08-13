﻿using RusGold.Shared.Entities.Abstract;
using RusGold.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusGold.Entities.DTOs
{
    public class UserListDto:DtoGetBase
    {
        public IList<User> Users { get; set; }
    }
}
