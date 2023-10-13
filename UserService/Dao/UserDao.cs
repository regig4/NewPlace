namespace UserService.Dao
{
    public static class UserDao
    {
        public static ApplicationUser? GetByEmail(string email)
        {
            using System.Data.SqlClient.SqlConnection? connection = SqlConnectionFactory.Create();
            using System.Data.SqlClient.SqlCommand? command = connection.CreateCommand();
            command.CommandText = "select id, login from [User] where login = @email";
            command.Parameters.AddWithValue("email", email);
            using System.Data.SqlClient.SqlDataReader? reader = command.ExecuteReader();
            if (!reader.HasRows)
            {
                return null;
            }

            return new ApplicationUser
            {
                Id = (string)reader["id"],
                Email = (string)reader["login"]
            };
        }
    }
}
