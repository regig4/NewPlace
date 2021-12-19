using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IAdvertisementRepository
    {
        Advertisement? GetById(int id);
        Advertisement? FindFirstOrDefault(Func<Advertisement, bool> condition);
        IAsyncEnumerable<Advertisement> FindAsync(Expression<Func<Advertisement, bool>> condition, int quantity = int.MaxValue);
        Task<int> Add(Advertisement advertisement);
    }
}
