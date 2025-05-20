using System;
using System.Collections.Generic;

namespace HealthTrackerApp
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }  // In a real app, this would be hashed
        public bool IsAdmin { get; set; }
        public List<Workout> Workouts { get; set; }

        public User(string username, string password, bool isAdmin = false)
        {
            Username = username;
            Password = password;
            IsAdmin = isAdmin;
            Workouts = new List<Workout>();
        }

        // For JSON serialization
        public User() 
        {
            Workouts = new List<Workout>();
        }
    }
} 