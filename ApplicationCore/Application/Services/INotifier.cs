using Common.Dto;

namespace ApplicationCore.Services
{
    internal interface INotifier
    {
        void SendMessage(UserDto user, string title, string message);
    }
}
