using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ApplicationCore.Resources;

namespace ApplicationCore.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Login { get; set; }

        [StringLength(512, MinimumLength = 512)]
        public string PasswordHash { get; set; }

        [Required]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [DisplayName("Agencja")]
        public virtual Agency Agency { get; set; }
    }
}
