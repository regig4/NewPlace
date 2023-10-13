using System.Linq.Expressions;
using ApplicationCore.Models;
using ApplicationCore.Specifications;
using Infrastructure.Factories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AdvertisementRepository : IAdvertisementRepository
    {
        public Advertisement? GetById(int id)
        {
            using NewPlaceDb? context = ContextFactory.Instance.Create();
            return context.Advertisements.Where(a => a.Id!.Value == id)
                .Include(a => a.Category)
                .Include(a => a.Estate).ThenInclude(apartment => apartment.Utilities)
                .Include(a => a.User).ThenInclude(user => user.Agency)
                .AsEnumerable()
                .SingleOrDefault();

        }

        public Advertisement? FindFirstOrDefault(Func<Advertisement, bool> condition)
        {
            using NewPlaceDb? context = ContextFactory.Instance.Create();
            return context.Advertisements.Where(condition).FirstOrDefault();
        }

        public async IAsyncEnumerable<Advertisement> FindAsync(Expression<Func<Advertisement, bool>> condition, int quantity = int.MaxValue)
        {
            using NewPlaceDb? context = ContextFactory.Instance.Create();
            IEnumerable<Advertisement>? query = context.Advertisements
                .Include(a => a.Category)
                .Include(a => a.Estate).ThenInclude(apartment => apartment.Utilities)
                .Include(a => a.Estate).ThenInclude(apartment => apartment.Location)
                .Include(a => a.User)
                .Where(condition.Compile())
                .Take(quantity);

            foreach (Advertisement? item in query)
            {
                yield return item;
            }
            //.Select(new Advertisement()
            //{
            //      TODO: get only these columns from db which are important to us
            //              Consider is it truly async, we are returning IEnumerable of IQueryable?
            //})
            ;

        }

        public Advertisement Find(Specification<Estate> specification)
        {
            using NewPlaceDb? context = ContextFactory.Instance.Create();
            return context.Advertisements.Where(a => specification.IsSatisfiedBy(a.Estate)).FirstOrDefault();
        }

        public IEnumerable<Advertisement> FindAll(Specification<Estate> specification, int quantity = int.MaxValue)
        {
            using NewPlaceDb? context = ContextFactory.Instance.Create();
            return context.Advertisements
                .Include(a => a.Category)
                .Include(a => a.Estate).ThenInclude(apartment => apartment.Utilities)
                .Include(a => a.User).ThenInclude(user => user.Agency)
                .Where(a => specification.IsSatisfiedBy(a.Estate)).Take(quantity).ToList();

        }

        public async Task<int> Add(Advertisement advertisement)
        {
            try
            {
                using NewPlaceDb? context = ContextFactory.Instance.Create();
                context.Advertisements.Add(advertisement);
                int id = await context.SaveChangesAsync();
                return id;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
