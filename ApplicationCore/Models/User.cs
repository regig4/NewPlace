using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationCore.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public virtual Agency Agency { get; set; }
    }
}
