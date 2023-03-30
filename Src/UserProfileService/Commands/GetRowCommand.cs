namespace UserProfileService.Commands;
public class GetRowCommand  
{
    public static void Get(MySqlConnection connection, string userIdToRetrieve)
    {
        

        string selectQuery = "SELECT * FROM User WHERE UserID = @UserIdToRetrieve;";
        MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection);
        selectCommand.Parameters.AddWithValue("@UserIdToRetrieve", userIdToRetrieve);

        try
        {
            connection.Open();
            MySqlDataReader reader = selectCommand.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string userId = reader.GetString(0);
                    string fullName = reader.GetString(1);
                    string username = reader.GetString(2);
                    string password = reader.GetString(3);

                    Console.WriteLine($"UserID: {userId}");
                    Console.WriteLine($"FullName: {fullName}");
                    Console.WriteLine($"Username: {username}");
                    Console.WriteLine($"Password: {password}");
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            connection.Close();
        }
    }
}
