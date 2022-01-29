using AdvertisementService.ApplicationCore.Application.Services;
using ApplicationCore.Services;
using Infrastructure.Repositories;
using Infrastructure.Services;

namespace Infrastructure.Factories
{
    public sealed class AdvertisementApplicationServiceFactory
    {
        private static AdvertisementApplicationServiceFactory _instance;
        private static readonly object _syncLock = new object();

        private AdvertisementApplicationServiceFactory() { }

        public static AdvertisementApplicationServiceFactory Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                lock (_syncLock)
                    if (_instance == null)
                        _instance = new AdvertisementApplicationServiceFactory();

                return _instance;
            }
        }

        public IAdvertisementApplicationService CreateAdvertisementService()
        {
            return new AdvertisementApplicationService(new AdvertisementRepository(), new ImageService());
        }
    }
}
