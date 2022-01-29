using System;
using System.Collections.Generic;
using System.Text;
using Common.Dto;

namespace ApplicationCore.Services
{
    interface INotifier
    {
        void SendMessage(UserDto user, string title, string message);
    }
}
