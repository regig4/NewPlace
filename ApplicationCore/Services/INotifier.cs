using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.Models;

namespace ApplicationCore.Services
{
    interface INotifier
    {
        void SendMessage(User user, string title, string message);
    }
}
