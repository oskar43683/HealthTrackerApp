using System;

namespace HealthTrackerApp
{
    public class Workout
    {
        public DateTime Date { get; set; }
        public string ExerciseType { get; set; }
        public int Duration { get; set; }  // Duration in minutes
        public int Calories { get; set; }
        public string Notes { get; set; }

        public Workout()
        {
        }

        public override string ToString()
        {
            int hours = Duration / 60;
            int minutes = Duration % 60;
            string duration = hours > 0 
                ? $"{hours}h {minutes}m" 
                : $"{minutes}m";
            return $"{Date.ToShortDateString()} - {ExerciseType} ({duration}, {Calories} cal)";
        }
    }
} 