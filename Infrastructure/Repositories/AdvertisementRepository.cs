using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.Models;
using Infrastructure.Factories;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Specifications;

namespace Infrastructure.Repositories
{
    public class AdvertisementRepository : IAdvertisementRepository
    {
        public Advertisement GetById(int id)
        {
            using (var context = ContextFactory.Instance.Create())
            {
                return context.Advertisements.AsNoTracking().Where(a => a.Id.Value == id)
                    .Include(a => a.Category)
                    .Include(a => a.Estate).ThenInclude(apartment => apartment.Utilities)
                    .Include(a => a.User).ThenInclude(user => user.Agency)
                    .AsEnumerable().SingleOrDefault();
            }
        }

        public Advertisement Find(Func<Advertisement, bool> condition)
        {
            using (var context = ContextFactory.Instance.Create())
            {
                return context.Advertisements.AsNoTracking().Where(condition).FirstOrDefault();
            }
        }

        public IEnumerable<Advertisement> FindAll(Func<Advertisement, bool> condition, int quantity = int.MaxValue)
        {
            using (var context = ContextFactory.Instance.Create())
            {
                return context.Advertisements.AsNoTracking()
                    .Include(a => a.Category)
                    .Include(a => a.Estate).ThenInclude(apartment => apartment.Utilities)
                    .Include(a => a.Estate).ThenInclude(apartment => apartment.Location)
                    .Include(a => a.User)
                    .Where(condition).Take(quantity)
                    //.Select(new Advertisement()
                    //{
                    //      TODO: get only these columns from db which are important to us
                    //})
                    .ToList();
            }
        }

        public Advertisement Find(Specification<Estate> specification)
        {
            using (var context = ContextFactory.Instance.Create())
            {
                return context.Advertisements.AsNoTracking().Where(a => specification.IsSatisfiedBy(a.Estate)).FirstOrDefault();
            }
        }

        public IEnumerable<Advertisement> FindAll(Specification<Estate> specification, int quantity = int.MaxValue)
        {
            using (var context = ContextFactory.Instance.Create())
            {
                return context.Advertisements.AsNoTracking()
                    .Include(a => a.Category)
                    .Include(a => a.Estate).ThenInclude(apartment => apartment.Utilities)
                    .Include(a => a.User).ThenInclude(user => user.Agency)
                    .Where(a => specification.IsSatisfiedBy(a.Estate)).Take(quantity).ToList();
            }
        }
    }
}
