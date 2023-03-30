namespace UserDatabaseTests.NUnit
{
    public class NUnitUserDatabaseTests
    {
        private string _server = "localhost";
        private string _user = "LutfiSalim";
        private string _password = "Lutfi203832@";
        private string _database = "mydatabase";

        [Test]
        public void Create_User_Test()
        {
            using (var connection = new MySqlConnection($"server={_server};user={_user};password={_password};database={_database};"))
            {
                // Arrange
                string fullName = "Lutfi";
                string username = "Lupi00";
                string password = "Lupi123";

                // Act
                CreateDataCommand.Create(connection, fullName, username, password);

                // Assert
                MySqlCommand selectCommand = new MySqlCommand("SELECT * FROM User WHERE Username=@Username;", connection);
                selectCommand.Parameters.AddWithValue("@Username", username);

                connection.Open();
                MySqlDataReader reader = selectCommand.ExecuteReader();
                bool recordFound = reader.Read();

                Assert.True(recordFound);
                Assert.AreEqual(fullName, reader["FullName"].ToString());
                Assert.AreEqual(username, reader["Username"].ToString());
                Assert.AreEqual(password, reader["Password"].ToString());
            }
        }

        [Test]
        public void Get_User_Test()
        {
            using (var connection = new MySqlConnection($"server={_server} ;user= {_user} ;password= {_password} ;database= {_database};"))
            {
                // Arrange

                MySqlCommand insertCommand = new MySqlCommand("INSERT INTO User (FullName, Username, Password) VALUES ('Lan', 'Lan00', 'Lan123');", connection);
                connection.Open();
                insertCommand.ExecuteNonQuery();
                connection.Close();

                MySqlCommand selectCommand = new MySqlCommand("SELECT UserID FROM User WHERE Username='Lan00';", connection);
                connection.Open();
                object generatedId = selectCommand.ExecuteScalar();
                connection.Close();

                //Act
                GetRowCommand.Get(connection, generatedId.ToString());

                //Assert
                selectCommand = new MySqlCommand("SELECT COUNT(*) FROM User WHERE Username='Lan00';", connection);
                connection.Open();
                int count = Convert.ToInt32(selectCommand.ExecuteScalar());
                connection.Close();

                Assert.AreEqual(1, count);
            }
        }

        [Test]
        public void Delete_User_Test()
        {
            using (var connection = new MySqlConnection($"server={_server} ;user= {_user} ;password= {_password} ;database= {_database};"))
            {
                // Arrange
                

                MySqlCommand selectCommand = new MySqlCommand("SELECT UserID FROM User WHERE Username='Lupi00';", connection);
                connection.Open();
                object generatedId = selectCommand.ExecuteScalar();
                connection.Close();

                // Act
                DeleteRowCommand.Delete(connection, generatedId.ToString());

                // Assert
                selectCommand = new MySqlCommand("SELECT COUNT(*) FROM User WHERE Username='Lupi00';", connection);
                connection.Open();
                int count = Convert.ToInt32(selectCommand.ExecuteScalar());
                connection.Close();

                Assert.AreEqual(0, count);
            }
        }

        [Test]
        public void List_Users_Test()
        {
            using (var connection = new MySqlConnection($"server={_server}  ;user=  {_user}  ;password=  {_password}  ;database=  {_database};"))
            {
                ListCommand.List(connection);
                
            }
        }
    }
}
