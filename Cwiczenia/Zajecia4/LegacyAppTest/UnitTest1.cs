using LegacyApp;
namespace LegacyAppTests
{
    public class UserServiceTests
    {
        [Fact]
        public void AddUser_Should_Return_False_When_FirstName_Is_Empty()
        {
            // Arrange
            var service = new UserService();
            string firstName = "";
            string lastName = "Doe";
            DateTime birthDate = new DateTime(1980, 1, 1);
            int clientId = 1;
            string email = "john.doe@example.com";

            // Act
            bool result = service.AddUser(firstName, lastName, email, birthDate, clientId);

            // Assert
            Assert.False(result);
        }
        [Fact]
        public void AddUser_Should_Return_False_When_LastName_Is_Empty()
        {
            // Arrange
            var service = new UserService();
            string firstName = "Doe";
            string lastName = "";
            DateTime birthDate = new DateTime(1980, 1, 1);
            int clientId = 1;
            string email = "john.doe@example.com";

            // Act
            bool result = service.AddUser(firstName, lastName, email, birthDate, clientId);

            // Assert
            Assert.False(result);
        }
        [Fact]
        public void AddUser_Should_Return_False_When_Email_Is_Invalid()
        {
            // Arrange
            var service = new UserService();
            string firstName = "John";
            string lastName = "Doe";
            DateTime birthDate = new DateTime(1980, 1, 1);
            int clientId = 1;
            string email = "johndoe";

            // Act
            bool result = service.AddUser(firstName, lastName, email, birthDate, clientId);

            // Assert
            Assert.False(result);
        }
        [Fact]
        public void AddUser_Should_Return_False_When_User_Is_Under_21()
        {
            // Arrange
            var service = new UserService();
            string firstName = "John";
            string lastName = "Doe";
            DateTime birthDate = new DateTime(2005, 1, 1);
            int clientId = 1;
            string email = "john.doe@example.com";

            // Act
            bool result = service.AddUser(firstName, lastName, email, birthDate, clientId);

            // Assert
            Assert.False(result);
        }
        [Fact]
        public void AddUser_Should_Set_HasCreditLimit_To_False_For_VeryImportantClient()
        {
            // Arrange
            var service = new UserService();
            string firstName = "John";
            string lastName = "Doe";
            DateTime birthDate = new DateTime(1980, 1, 1);
            int clientId = 2;
            string email = "john.doe@example.com";

            // Act
            bool result = service.AddUser(firstName, lastName, email, birthDate, clientId);

            // Assert
            Assert.True(result);
        }
        [Fact]
        public void AddUser_Should_Set_CreditLimit_Correctly_For_ImportantClient()
        {
            // Arrange
            var service = new UserService();
            string firstName = "Jane";
            string lastName = "Doe";
            DateTime birthDate = new DateTime(1980, 1, 1);
            int clientId = 3;
            string email = "jane.doe@example.com";

            // Act
            bool result = service.AddUser(firstName, lastName, email, birthDate, clientId);

            // Assert
            Assert.True(result);

        }
        [Fact]
        public void AddUser_Should_Set_CreditLimit_Correctly_For_NormalClient()
        {
            // Arrange
            var service = new UserService();
            string firstName = "Jane";
            string lastName = "Doe";
            DateTime birthDate = new DateTime(1980, 1, 1);
            int clientId = 5;
            string email = "jane.doe@example.com";

            // Act
            bool result = service.AddUser(firstName, lastName, email, birthDate, clientId);

            // Assert
            Assert.True(result);

        }
    }
}