using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Services
{
    interface ILogger
    {
        void LogError(Exception exception);
        void LogError(string customErrorInformation);
    }
}
