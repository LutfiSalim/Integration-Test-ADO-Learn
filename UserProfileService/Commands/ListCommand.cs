namespace UserProfileService.Commands;
public class ListCommand    
{
    public static void List(MySqlConnection connection)
    {
        string selectQuery = "SELECT * FROM User;";
        MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection);

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
                    Console.WriteLine();
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
