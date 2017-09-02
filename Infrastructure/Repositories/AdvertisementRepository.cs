using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.Models;
using Infrastructure.Factories;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AdvertisementRepository : IAdvertisementRepository
    {
        public Advertisement GetById(int id)
        {
            using (var context = ContextFactory.Instance.Create())
            {
                return context.Advertisements.AsNoTracking().Where(a => a.Id.Value == id)
                    .Include(a => a.Apartment).ThenInclude(apartment => apartment.Category)
                    .Include(a => a.Apartment).ThenInclude(apartment => apartment.Utilities)
                    .Include(a => a.User).ThenInclude(user => user.Agency)
                    .AsEnumerable().FirstOrDefault();
            }
        }

        public IEnumerable<Advertisement> GetByCondition(Func<Advertisement, bool> condition, int quantity = int.MaxValue)
        {
            using (var context = ContextFactory.Instance.Create())
            {
                return context.Advertisements.AsNoTracking().Where(condition).AsEnumerable();
            }
        }
    }
}
