using System;

namespace ApplicationCore.Services
{
    internal interface ILogger
    {
        void LogError(Exception exception);
        void LogError(string customErrorInformation);
    }
}
