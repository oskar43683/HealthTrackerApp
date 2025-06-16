using System;
using Xunit;
using HealthTrackerApp;

namespace HealthTrackerApp.Tests
{
    public class WorkoutTests
    {
        [Fact]
        public void CreateWorkout_ValidData_ShouldSetPropertiesCorrectly()
        {
            DateTime date = DateTime.Now;
            string exerciseType = "Running";
            int duration = 45;
            int calories = 300;
            string notes = "Morning run";

            var workout = new Workout
            {
                Date = date,
                ExerciseType = exerciseType,
                Duration = duration,
                Calories = calories,
                Notes = notes
            };

            Assert.Equal(date, workout.Date);
            Assert.Equal(exerciseType, workout.ExerciseType);
            Assert.Equal(duration, workout.Duration);
            Assert.Equal(calories, workout.Calories);
            Assert.Equal(notes, workout.Notes);
        }

        [Fact]
        public void ToString_ShouldFormatCorrectly()
        {
            var workout = new Workout
            {
                Date = new DateTime(2024, 3, 15),
                ExerciseType = "Running",
                Duration = 75, // 1 hour and 15 minutes
                Calories = 500,
                Notes = "Morning run"
            };

            string result = workout.ToString();

            Assert.Equal("2024-03-15 - Running (1h 15m, 500 cal)", result);
        }

        [Fact]
        public void ToString_LessThanOneHour_ShouldFormatCorrectly()
        {
            var workout = new Workout
            {
                Date = new DateTime(2024, 3, 15),
                ExerciseType = "Running",
                Duration = 45, // 45 minutes
                Calories = 300,
                Notes = "Morning run"
            };

            string result = workout.ToString();

            Assert.Equal("2024-03-15 - Running (45m, 300 cal)", result);
        }

        [Fact]
        public void ToString_ZeroMinutes_ShouldFormatCorrectly()
        {
            var workout = new Workout
            {
                Date = new DateTime(2024, 3, 15),
                ExerciseType = "Running",
                Duration = 0,
                Calories = 0,
                Notes = "Morning run"
            };

            string result = workout.ToString();

            Assert.Equal("2024-03-15 - Running (0m, 0 cal)", result);
        }
    }
} 