using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Linq;

namespace HealthTrackerApp
{
    public class MainForm : Form
    {
        private List<Workout> workouts;
        private string dataFile = "workouts.txt";
        private DateTimePicker datePicker;
        private ComboBox exerciseTypeCombo;
        private NumericUpDown hoursNumeric;
        private NumericUpDown minutesNumeric;
        private TextBox caloriesTextBox;
        private TextBox notesTextBox;
        private ListBox workoutList;

        public MainForm()
        {
            workouts = new List<Workout>();
            InitializeComponent();
            LoadWorkouts();
        }

        private void InitializeComponent()
        {
            this.Text = "Health & Training Tracker";
            this.Size = new System.Drawing.Size(800, 600);

            // Create controls
            datePicker = new DateTimePicker { Location = new System.Drawing.Point(20, 20) };
            exerciseTypeCombo = new ComboBox 
            { 
                Location = new System.Drawing.Point(20, 60),
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            exerciseTypeCombo.Items.AddRange(new string[] { "Running", "Cycling", "Swimming", "Weight Training", "Yoga", "Other" });

            var durationLabel = new Label { Text = "Duration:", Location = new System.Drawing.Point(20, 100) };
            
            // Hours input
            hoursNumeric = new NumericUpDown
            {
                Location = new System.Drawing.Point(150, 100),
                Width = 60,
                Minimum = 0,
                Maximum = 24,
                Value = 0
            };

            var hoursLabel = new Label 
            { 
                Text = "hours", 
                Location = new System.Drawing.Point(215, 102),
                AutoSize = true
            };

            // Minutes input
            minutesNumeric = new NumericUpDown
            {
                Location = new System.Drawing.Point(270, 100),
                Width = 60,
                Minimum = 0,
                Maximum = 59,
                Value = 0
            };

            var minutesLabel = new Label 
            { 
                Text = "minutes", 
                Location = new System.Drawing.Point(335, 102),
                AutoSize = true
            };

            var caloriesLabel = new Label { Text = "Calories burned:", Location = new System.Drawing.Point(20, 140) };
            caloriesTextBox = new TextBox { Location = new System.Drawing.Point(150, 140), Width = 100 };

            var notesLabel = new Label { Text = "Notes:", Location = new System.Drawing.Point(20, 180) };
            notesTextBox = new TextBox 
            { 
                Location = new System.Drawing.Point(20, 200),
                Width = 400,
                Height = 60,
                Multiline = true
            };

            var addButton = new Button
            {
                Text = "Add Workout",
                Location = new System.Drawing.Point(20, 280),
                Width = 100
            };

            workoutList = new ListBox
            {
                Location = new System.Drawing.Point(20, 320),
                Width = 700,
                Height = 200
            };

            // Add controls to form
            this.Controls.AddRange(new Control[] 
            { 
                datePicker, exerciseTypeCombo, durationLabel, hoursNumeric, hoursLabel,
                minutesNumeric, minutesLabel, caloriesLabel, caloriesTextBox, 
                notesLabel, notesTextBox, addButton, workoutList
            });

            // Add button click event
            addButton.Click += (sender, e) =>
            {
                if (string.IsNullOrEmpty(exerciseTypeCombo.Text) ||
                    !int.TryParse(caloriesTextBox.Text, out int calories))
                {
                    MessageBox.Show("Please fill in all required fields with valid values.");
                    return;
                }

                int totalMinutes = (int)(hoursNumeric.Value * 60 + minutesNumeric.Value);
                if (totalMinutes == 0)
                {
                    MessageBox.Show("Duration must be greater than 0 minutes.");
                    return;
                }

                var workout = new Workout(
                    datePicker.Value,
                    exerciseTypeCombo.Text,
                    totalMinutes,
                    calories,
                    notesTextBox.Text
                );

                workouts.Add(workout);
                SaveWorkouts();
                RefreshWorkoutList(workoutList);
                ClearInputs(exerciseTypeCombo, caloriesTextBox, notesTextBox);
            };

            // Initial list refresh
            RefreshWorkoutList(workoutList);
        }

        private void RefreshWorkoutList(ListBox listBox)
        {
            listBox.Items.Clear();
            foreach (var workout in workouts.OrderByDescending(w => w.Date))
            {
                listBox.Items.Add(workout);
            }
        }

        private void ClearInputs(ComboBox exerciseType, TextBox calories, TextBox notes)
        {
            exerciseType.SelectedIndex = -1;
            hoursNumeric.Value = 0;
            minutesNumeric.Value = 0;
            calories.Clear();
            notes.Clear();
        }

        private void SaveWorkouts()
        {
            try
            {
                var lines = workouts.Select(w => 
                    $"{w.Date}|{w.ExerciseType}|{w.DurationMinutes}|{w.CaloriesBurned}|{w.Notes}");
                File.WriteAllLines(dataFile, lines);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving workouts: {ex.Message}");
            }
        }

        private void LoadWorkouts()
        {
            if (!File.Exists(dataFile)) return;

            try
            {
                var lines = File.ReadAllLines(dataFile);
                foreach (var line in lines)
                {
                    var parts = line.Split('|');
                    if (parts.Length >= 4)
                    {
                        var workout = new Workout(
                            DateTime.Parse(parts[0]),
                            parts[1],
                            int.Parse(parts[2]),
                            int.Parse(parts[3]),
                            parts.Length > 4 ? parts[4] : ""
                        );
                        workouts.Add(workout);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading workouts: {ex.Message}");
            }
        }
    }
} 