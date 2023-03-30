    namespace UserDatabaseTests.XUnitTestADO;
        public class XUnitDatabaseTest
        {
            private string server = "localhost";
            private string user = "LutfiSalim";
            private string password = "Lutfi203832@";
            private string database = "mydatabase";

        [Fact]
        public void Create_User_Test()
        {

            using (var connection = new MySqlConnection($"server={server};user={user};password={password};database={database};"))
            {
                // Arrange
                string fullName = "Lutfi Salim";
                string username = "lupin";
                string password = "Lutfi123";

                // Act
                CreateDataCommand.Create(connection, fullName, username, password);

                // Assert
                MySqlCommand selectCommand = new MySqlCommand("SELECT * FROM User WHERE Username=@Username;", connection);
                selectCommand.Parameters.AddWithValue("@Username", username);

                connection.Open();
                MySqlDataReader reader = selectCommand.ExecuteReader();
                bool recordFound = reader.Read();

                Assert.True(recordFound);
                Assert.Equal(fullName, reader["FullName"].ToString());
                Assert.Equal(username, reader["Username"].ToString());
                Assert.Equal(password, reader["Password"].ToString());
        }
        }

        [Fact]
        public void Get_User_Test()
        {
            using (var connection = new MySqlConnection($"server={server};user={user};password={password};database={database};"))
            {
                // Arrange

                MySqlCommand insertCommand = new MySqlCommand("INSERT INTO User (FullName, Username, Password) VALUES ('Ali', 'Ali00', 'Ali123');", connection);
                connection.Open();
                insertCommand.ExecuteNonQuery();
                connection.Close();


                MySqlCommand selectCommand = new MySqlCommand("SELECT UserID FROM User WHERE Username='ali00';", connection);
                connection.Open();
                object generatedId = selectCommand.ExecuteScalar();
                connection.Close();

                //Act
                GetRowCommand.Get(connection, generatedId.ToString());


                //Assert
                selectCommand = new MySqlCommand("SELECT COUNT(*) FROM User WHERE Username='ali00';", connection);
                connection.Open();
                int count = Convert.ToInt32(selectCommand.ExecuteScalar());
                connection.Close();

                Assert.Equal(1, count);
            }
        }

        [Fact]
        public void Delete_User_Test()
        {
            using (var connection = new MySqlConnection($"server={server};user={user};password={password};database={database};"))
            {
            // Arrange

                MySqlCommand insertCommand = new MySqlCommand("INSERT INTO User (FullName, Username, Password) VALUES ('Pablo', 'Pablo00', 'Pablo123');", connection);
                connection.Open();
                insertCommand.ExecuteNonQuery();
                connection.Close();

                MySqlCommand selectCommand = new MySqlCommand("SELECT UserID FROM User WHERE Username='Pablo00';", connection);
                connection.Open();
                object generatedId = selectCommand.ExecuteScalar();
                connection.Close();

                // Act
                DeleteRowCommand.Delete(connection, generatedId.ToString());

                // Assert
                selectCommand = new MySqlCommand("SELECT COUNT(*) FROM User WHERE Username='Pablo00';", connection);
                connection.Open();
                int count = Convert.ToInt32(selectCommand.ExecuteScalar());
                connection.Close();

                Assert.Equal(0, count);
            }
        }

    

        [Fact]
        public void List_Users_Test()
        {
            using (var connection = new MySqlConnection($"server={server};user={user};password={password};database={database};"))
            {
                ListCommand.List(connection);
            }
        }
        
        }   

