using ApplicationCore.DTOs;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Converters
{
    public static class DtoConverters
    {
        public static Advertisement ToDomain(this AdvertisementDto dto)
        {
            // todo
            return new Advertisement(dto.Id, dto.Title, null, null, DateTime.Now, DateTime.Now + TimeSpan.FromHours(24), null, null, 10, 10);
        }
    }
}
