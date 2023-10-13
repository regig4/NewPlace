using ApplicationCore.DTOs;

namespace ApplicationCore.Application.Commands
{
    public record CreateAdvertisementCommand(AdvertisementDetailsDto Dto, string ImageBase64String) : ICommand<int>;
}
