using System.Data.SqlClient;

namespace UserService.Dao
{
    public class ObservedDao : IObservedDao
    {

        public async Task<ICollection<int>> GetIdsOfObservedAdvertisements(Guid userId)
        {
            EnsureSchema();
            HashSet<int>? result = new HashSet<int>();
            using SqlConnection? connection = SqlConnectionFactory.Create();
            using SqlCommand? command = connection.CreateCommand();
            command.CommandText = "select * from user.observations where user_id = @userId";
            command.Parameters.AddWithValue("@userId", userId.ToString());
            using SqlDataReader? dataReader = await command.ExecuteReaderAsync();
            await connection.OpenAsync();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    result.Add((int)dataReader["advertisement_id"]);
                }
            }
            return result;
        }

        private void EnsureSchema()
        {

        }
    }
}
