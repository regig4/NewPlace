using ApplicationCore.Services;
using Infrastructure.Repositories;
using Infrastructure.Services;

namespace Infrastructure.Factories
{
    public sealed class AdvertisementServiceFactory
    {
        private static AdvertisementServiceFactory _instance;
        private static readonly object _syncLock = new object();

        private AdvertisementServiceFactory() { }

        public static AdvertisementServiceFactory Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                lock (_syncLock)
                    if (_instance == null)
                        _instance = new AdvertisementServiceFactory();

                return _instance;
            }
        }

        public IAdvertisementService Create()
        {
            return new AdvertisementService(new AdvertisementRepository(), new ImageService());
        }
    }
}
