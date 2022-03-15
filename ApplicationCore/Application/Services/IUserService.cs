﻿using ApplicationCore.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Application.Services
{
    public interface IUserService
    {
        Task<List<AdvertisementDto>> GetObservedAdvertisements(Guid userId);
    }
}
