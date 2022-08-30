using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ApplicationCore.Models
{
    public class User
    {
        public User()
        {

        }

        public User(int? id, string login, string passwordHash, string email, Agency? agency)
            : this(id, login, passwordHash, email)
        {
            Agency = agency;
        }

        private User(int? id, string login, string passwordHash, string email)
        {
            Id = id;
            Login = login;
            PasswordHash = passwordHash;
            Email = email;
        }

        public int? Id { get; set; }
        public string Login { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public virtual Agency? Agency { get; set; }
    }
}
