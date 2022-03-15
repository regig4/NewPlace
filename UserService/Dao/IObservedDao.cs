namespace UserService.Dao
{
    public interface IObservedDao
    {
        Task<ICollection<int>> GetIdsOfObservedAdvertisements(Guid userId);
    }
}
