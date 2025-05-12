using System;

namespace HealthTrackerApp
{
    public class Workout
    {
        public DateTime Date { get; set; }
        public string ExerciseType { get; set; }
        public int DurationMinutes { get; set; }
        public int CaloriesBurned { get; set; }
        public string Notes { get; set; }

        public Workout(DateTime date, string exerciseType, int durationMinutes, int caloriesBurned, string notes = "")
        {
            Date = date;
            ExerciseType = exerciseType;
            DurationMinutes = durationMinutes;
            CaloriesBurned = caloriesBurned;
            Notes = notes;
        }

        public override string ToString()
        {
            return $"{Date.ToShortDateString()} - {ExerciseType} ({DurationMinutes} min, {CaloriesBurned} cal)";
        }
    }
} 