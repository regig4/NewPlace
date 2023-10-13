using Common.Dto;

namespace ApplicationCore.Services
{
    public interface IGeolocationService
    {
        Location GetCurrentPosition();
        double GetDistanceToPosition(Location destination);
    }
}
