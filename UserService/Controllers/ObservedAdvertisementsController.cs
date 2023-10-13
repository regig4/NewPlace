using Microsoft.AspNetCore.Mvc;
using UserService.Dao;

namespace UserService.Controllers
{
    public class ObservedAdvertisementsController : ControllerBase
    {
        private readonly IObservedDao _observedDao;

        public ObservedAdvertisementsController(IObservedDao observedDao)
        {
            _observedDao = observedDao;
        }

        [HttpGet("/{userId}")]
        public async Task<ICollection<int>> GetObservedAdvertisements(Guid userId)
        {
            return await _observedDao.GetIdsOfObservedAdvertisements(userId);
        }
    }
}
