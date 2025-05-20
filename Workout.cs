using System;

namespace HealthTrackerApp
{
    public class Workout
    {
        public DateTime Date { get; set; }
        public string ExerciseType { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int CaloriesBurned { get; set; }
        public string Notes { get; set; }

        public Workout(DateTime date, string exerciseType, int hours, int minutes, int caloriesBurned, string notes = "")
        {
            Date = date;
            ExerciseType = exerciseType;
            Hours = hours;
            Minutes = minutes;
            CaloriesBurned = caloriesBurned;
            Notes = notes;
        }

        public override string ToString()
        {
            string duration = Hours > 0 
                ? $"{Hours}h {Minutes}m" 
                : $"{Minutes}m";
            return $"{Date.ToShortDateString()} - {ExerciseType} ({duration}, {CaloriesBurned} cal)";
        }
    }
} 