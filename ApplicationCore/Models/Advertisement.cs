using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Models
{
    public class Advertisement
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
    }
}
