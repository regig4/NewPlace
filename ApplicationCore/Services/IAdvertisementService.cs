using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.Models;

namespace ApplicationCore.Services
{
    public interface IAdvertisementService
    {
        Advertisement GetById(int id);
    }
}
