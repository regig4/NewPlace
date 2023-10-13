using System.Data.SqlClient;

namespace UserService.Dao
{
    public static class SqlConnectionFactory
    {
        private const string _defaultConnectionString = @"Server=.;Database=NewPlaceDb;Trusted_connection=True";

        public static SqlConnection Create()
        {
            SqlConnection? connection = new SqlConnection(_defaultConnectionString);
            connection.Open();
            return connection;
        }
    }
}
