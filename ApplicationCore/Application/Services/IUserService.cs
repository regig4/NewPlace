using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.DTOs;

namespace ApplicationCore.Application.Services
{
    public interface IUserService
    {
        Task<List<AdvertisementDto>> GetObservedAdvertisements(Guid userId);
    }
}
