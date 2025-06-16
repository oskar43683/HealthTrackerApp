using System;
using System.IO;
using Xunit;
using HealthTrackerApp;

namespace HealthTrackerApp.Tests
{
    public class UserTests : IDisposable
    {
        private readonly string _testDirectory;

        public UserTests()
        {
            // Create a test directory for each test run
            _testDirectory = Path.Combine(Path.GetTempPath(), "HealthTrackerTests", Guid.NewGuid().ToString());
            Directory.CreateDirectory(_testDirectory);
            User.SetBaseDirectory(_testDirectory);
        }

        public void Dispose()
        {
            // Clean up test directory after each test
            if (Directory.Exists(_testDirectory))
            {
                Directory.Delete(_testDirectory, true);
            }
            // Reset base directory to default after test
            User.SetBaseDirectory(AppDomain.CurrentDomain.BaseDirectory);
        }

        [Fact]
        public void CreateUser_ValidCredentials_ShouldCreateUserDirectory()
        {
            // Arrange
            string username = "testuser";
            string password = "testpass";

            // Act
            var user = new User(username, password);

            // Assert
            string expectedPath = Path.Combine(_testDirectory, "Users", username);
            Assert.True(Directory.Exists(expectedPath));
            Assert.True(File.Exists(Path.Combine(expectedPath, "user.txt")));
        }

        [Fact]
        public void CreateUser_ValidCredentials_ShouldHashPassword()
        {
            // Arrange
            string username = "testuser";
            string password = "testpass";

            // Act
            var user = new User(username, password);

            // Assert
            Assert.NotNull(user.PasswordHash);
            Assert.NotEqual(password, user.PasswordHash);
        }

        [Fact]
        public void VerifyPassword_CorrectPassword_ShouldReturnTrue()
        {
            // Arrange
            string username = "testuser";
            string password = "testpass";
            var user = new User(username, password);

            // Act
            bool result = user.VerifyPassword(password);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void VerifyPassword_IncorrectPassword_ShouldReturnFalse()
        {
            // Arrange
            string username = "testuser";
            string password = "testpass";
            var user = new User(username, password);

            // Act
            bool result = user.VerifyPassword("wrongpass");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LoadUser_ExistingUser_ShouldLoadCorrectly()
        {
            // Arrange
            string username = "testuser";
            string password = "testpass";
            var originalUser = new User(username, password);
            originalUser.SaveUser();

            // Act
            var loadedUser = User.LoadUser(username);

            // Assert
            Assert.NotNull(loadedUser);
            Assert.Equal(username, loadedUser.Username);
            Assert.True(loadedUser.VerifyPassword(password));
        }

        [Fact]
        public void LoadUser_NonExistentUser_ShouldReturnNull()
        {
            // Act
            var user = User.LoadUser("nonexistentuser");

            // Assert
            Assert.Null(user);
        }

        [Fact]
        public void SaveUser_ShouldCreateUserFile()
        {
            // Arrange
            string username = "testuser";
            string password = "testpass";
            var user = new User(username, password);

            // Act
            user.SaveUser();

            // Assert
            string expectedPath = Path.Combine(_testDirectory, "Users", username, "user.txt");
            Assert.True(File.Exists(expectedPath));
            string[] lines = File.ReadAllLines(expectedPath);
            Assert.Equal(2, lines.Length);
            Assert.Equal(username, lines[0]);
            Assert.Equal(user.PasswordHash, lines[1]);
        }
    }
} 