using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.DTOs;

namespace ApplicationCore.Application.Commands
{
    public record CreateAdvertisementCommand(AdvertisementDetailsDto Dto, string ImageBase64String) : ICommand<int>;
}
