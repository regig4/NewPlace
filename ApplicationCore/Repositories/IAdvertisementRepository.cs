using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public interface IAdvertisementRepository
    {
        Advertisement GetById(int id);
        Advertisement Find(Func<Advertisement, bool> condition);
        IEnumerable<Advertisement> FindAll(Func<Advertisement, bool> condition, int quantity = int.MaxValue);
    }
}
