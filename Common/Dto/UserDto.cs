﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
    }
}
