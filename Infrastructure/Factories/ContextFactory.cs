using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Factories
{
    public sealed class ContextFactory
    {
        private static volatile ContextFactory _instance;
        private static readonly object _syncLock = new object();
        public static ContextFactory Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                lock(_syncLock)
                    if (_instance == null)
                        _instance = new ContextFactory();

                return _instance;
            }
        }

        public NewPlaceDb Create()
        {
            var options = new DbContextOptionsBuilder<NewPlaceDb>();
            return new NewPlaceDb(options.Options);
        }
    }
}
