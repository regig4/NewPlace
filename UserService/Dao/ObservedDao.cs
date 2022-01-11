using System.Data.SqlClient;

namespace UserService.Dao
{
    public class ObservedDao : IObservedDao
    {
        public const string DefaultConnectionString = @"Server=.;Database=NewPlaceDb;Trusted_connection=True";

        public async Task<ICollection<int>> GetIdsOfObservedAdvertisements(Guid userId)
        {
            EnsureSchema();
            var result = new HashSet<int>();
            using var connection = new SqlConnection(DefaultConnectionString);
            using var command = connection.CreateCommand();
            command.CommandText = "select * from user.observations where user_id = @userId";
            command.Parameters.AddWithValue("@userId", userId.ToString());
            using var dataReader = await command.ExecuteReaderAsync();
            await connection.OpenAsync();
            if(dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    result.Add((int) dataReader["advertisement_id"]);
                }
            }
            return result;
        }

        private void EnsureSchema()
        {

        }
    }
}
