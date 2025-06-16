using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace HealthTrackerApp
{
    public class User
    {
        public string Username { get; private set; }
        public string PasswordHash { get; private set; }
        public string UserDirectory { get; private set; }
        private static string _baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        public static void SetBaseDirectory(string baseDirectory)
        {
            _baseDirectory = baseDirectory;
        }

        public User(string username, string password)
        {
            Username = username;
            PasswordHash = HashPassword(password);
            UserDirectory = Path.Combine(_baseDirectory, "Users", username);
            Directory.CreateDirectory(UserDirectory);
        }

        private User(string username, string passwordHash, string userDirectory)
        {
            Username = username;
            PasswordHash = passwordHash;
            UserDirectory = userDirectory;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public bool VerifyPassword(string password)
        {
            return HashPassword(password) == PasswordHash;
        }

        public static User LoadUser(string username)
        {
            string userDirectory = Path.Combine(_baseDirectory, "Users", username);
            string userPath = Path.Combine(userDirectory, "user.txt");
            
            if (!File.Exists(userPath))
                return null;

            string[] lines = File.ReadAllLines(userPath);
            if (lines.Length != 2)
                return null;

            return new User(lines[0], lines[1], userDirectory);
        }

        public void SaveUser()
        {
            string userPath = Path.Combine(UserDirectory, "user.txt");
            File.WriteAllLines(userPath, new[] { Username, PasswordHash });
        }
    }
} 