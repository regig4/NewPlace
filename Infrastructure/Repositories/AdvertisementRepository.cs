using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.Models;
using Infrastructure.Factories;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Specifications;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class AdvertisementRepository : IAdvertisementRepository
    {
        public Advertisement GetById(int id)
        {
            using var context = ContextFactory.Instance.Create();
            return context.Advertisements.Where(a => a.Id!.Value == id)
                .Include(a => a.Category)
                .Include(a => a.Estate).ThenInclude(apartment => apartment.Utilities)
                .Include(a => a.User).ThenInclude(user => user.Agency)
                .AsEnumerable()
                .SingleOrDefault();

        }

        public Advertisement Find(Func<Advertisement, bool> condition)
        {
            using var context = ContextFactory.Instance.Create();
            return context.Advertisements.Where(condition).FirstOrDefault();
        }

        public async IAsyncEnumerable<Advertisement> FindAllAsync(Expression<Func<Advertisement, bool>> condition, int quantity = int.MaxValue)
        {
            using var context = ContextFactory.Instance.Create();
            var query = context.Advertisements
                .Include(a => a.Category)
                .Include(a => a.Estate).ThenInclude(apartment => apartment.Utilities)
                .Include(a => a.Estate).ThenInclude(apartment => apartment.Location)
                .Include(a => a.User)
                .ToList()                   // TODO: calls to db 
                                            .Where(condition.Compile()) // (ef cant translate Where to sql), when it will be possible delete '.Compile()'
                .Take(quantity);

            foreach (var item in query)
                yield return item;
            //.Select(new Advertisement()
            //{
            //      TODO: get only these columns from db which are important to us
            //              Consider is it truly async, we are returning IEnumerable of IQueryable?
            //})
            ;

        }

        public Advertisement Find(Specification<Estate> specification)
        {
            using var context = ContextFactory.Instance.Create();
            return context.Advertisements.Where(a => specification.IsSatisfiedBy(a.Estate)).FirstOrDefault();
        }

        public IEnumerable<Advertisement> FindAll(Specification<Estate> specification, int quantity = int.MaxValue)
        {
            using var context = ContextFactory.Instance.Create();
            return context.Advertisements
                .Include(a => a.Category)
                .Include(a => a.Estate).ThenInclude(apartment => apartment.Utilities)
                .Include(a => a.User).ThenInclude(user => user.Agency)
                .Where(a => specification.IsSatisfiedBy(a.Estate)).Take(quantity).ToList();

        }

        public async Task<int> Add(Advertisement advertisement)
        {
            using var context = ContextFactory.Instance.Create();
            context.Advertisements.Add(advertisement);
            int id = await context.SaveChangesAsync();
            return id;
        }
    }
}
