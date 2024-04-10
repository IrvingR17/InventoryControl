using System.Data.SqlClient;
using System.Configuration;

namespace InventoryControl.Auth
{
    public class AuthenticationManager
    {
        public static bool AuthenticateUser(string Username, string Password)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MSSQLLocalDB"].ConnectionString))
            {
                string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password";

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@Username", Username);
                    sqlCommand.Parameters.AddWithValue("@Password", Password);

                    int count = (int)sqlCommand.ExecuteScalar();

                    if (count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

    }
}
