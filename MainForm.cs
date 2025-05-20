using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;

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
        private Panel inputPanel;
        private Panel listPanel;
        private TextBox selectedNotesBox;
        private Label selectedNotesLabel;

        public MainForm()
        {
            workouts = new List<Workout>();
            InitializeComponent();
            LoadWorkouts();
        }

        private void InitializeComponent()
        {
            this.Text = "Health & Training Tracker";
            this.Size = new System.Drawing.Size(1100, 800);
            this.BackColor = Color.FromArgb(18, 18, 18);
            this.ForeColor = Color.White;
            this.Font = new Font(new FontFamily("Segoe UI"), 10F, FontStyle.Regular);
            this.Padding = new Padding(20);

            inputPanel = new Panel
            {
                Location = new System.Drawing.Point(20, 20),
                Size = new System.Drawing.Size(500, 700),
                BackColor = Color.FromArgb(30, 30, 30),
                Padding = new Padding(25)
            };

            listPanel = new Panel
            {
                Location = new System.Drawing.Point(540, 20),
                Size = new System.Drawing.Size(520, 700),
                BackColor = Color.FromArgb(30, 30, 30),
                Padding = new Padding(25)
            };

            var headerLabel = new Label
            {
                Text = "Add New Workout",
                Font = new Font(new FontFamily("Segoe UI"), 18F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new System.Drawing.Point(25, 25)
            };

            datePicker = new DateTimePicker 
            { 
                Location = new System.Drawing.Point(25, 70),
                Width = 220,
                BackColor = Color.FromArgb(45, 45, 45),
                ForeColor = Color.White,
                CalendarTitleBackColor = Color.FromArgb(45, 45, 45),
                CalendarTitleForeColor = Color.White,
                CalendarTrailingForeColor = Color.Gray,
                Format = DateTimePickerFormat.Short,
                Font = new Font(new FontFamily("Segoe UI"), 11F, FontStyle.Bold)
            };

            exerciseTypeCombo = new ComboBox 
            { 
                Location = new System.Drawing.Point(25, 120),
                Width = 220,
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.FromArgb(45, 45, 45),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font(new FontFamily("Segoe UI"), 11F, FontStyle.Bold)
            };
            exerciseTypeCombo.Items.AddRange(new string[] { "Running", "Cycling", "Swimming", "Weight Training", "Yoga", "Other" });

            var durationLabel = new Label 
            { 
                Text = "Duration", 
                Location = new System.Drawing.Point(25, 170),
                ForeColor = Color.White,
                Font = new Font(new FontFamily("Segoe UI"), 12F, FontStyle.Bold),
                AutoSize = true
            };
            
            hoursNumeric = new NumericUpDown
            {
                Location = new System.Drawing.Point(25, 200),
                Width = 70,
                Minimum = 0,
                Maximum = 24,
                Value = 0,
                BackColor = Color.FromArgb(45, 45, 45),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font(new FontFamily("Segoe UI"), 11F, FontStyle.Bold)
            };

            var hoursLabel = new Label 
            { 
                Text = "hours", 
                Location = new System.Drawing.Point(100, 202),
                AutoSize = true,
                ForeColor = Color.White,
                Font = new Font(new FontFamily("Segoe UI"), 11F, FontStyle.Bold)
            };

            minutesNumeric = new NumericUpDown
            {
                Location = new System.Drawing.Point(170, 200),
                Width = 70,
                Minimum = 0,
                Maximum = 59,
                Value = 0,
                BackColor = Color.FromArgb(45, 45, 45),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font(new FontFamily("Segoe UI"), 11F, FontStyle.Bold)
            };

            var minutesLabel = new Label 
            { 
                Text = "minutes", 
                Location = new System.Drawing.Point(245, 202),
                AutoSize = true,
                ForeColor = Color.White,
                Font = new Font(new FontFamily("Segoe UI"), 11F, FontStyle.Bold)
            };

            var caloriesLabel = new Label 
            { 
                Text = "Calories burned", 
                Location = new System.Drawing.Point(25, 250),
                ForeColor = Color.White,
                Font = new Font(new FontFamily("Segoe UI"), 12F, FontStyle.Bold),
                AutoSize = true
            };
            
            caloriesTextBox = new TextBox 
            { 
                Location = new System.Drawing.Point(25, 280), 
                Width = 120,
                BackColor = Color.FromArgb(45, 45, 45),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font(new FontFamily("Segoe UI"), 11F, FontStyle.Bold)
            };

            var notesLabel = new Label 
            { 
                Text = "Notes", 
                Location = new System.Drawing.Point(25, 330),
                ForeColor = Color.White,
                Font = new Font(new FontFamily("Segoe UI"), 12F, FontStyle.Bold),
                AutoSize = true
            };
            
            notesTextBox = new TextBox 
            { 
                Location = new System.Drawing.Point(25, 360),
                Width = 430,
                Height = 80,
                Multiline = true,
                BackColor = Color.FromArgb(45, 45, 45),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font(new FontFamily("Segoe UI"), 11F, FontStyle.Bold)
            };

            var addButton = new Button
            {
                Text = "Add Workout",
                Location = new System.Drawing.Point(25, 460),
                Width = 140,
                Height = 40,
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font(new FontFamily("Segoe UI"), 11F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };

            var listHeaderLabel = new Label
            {
                Text = "Workout History",
                Font = new Font(new FontFamily("Segoe UI"), 18F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new System.Drawing.Point(25, 25)
            };

            workoutList = new ListBox
            {
                Location = new System.Drawing.Point(25, 70),
                Width = 470,
                Height = 400,
                BackColor = Color.FromArgb(45, 45, 45),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font(new FontFamily("Segoe UI"), 11F, FontStyle.Bold),
                IntegralHeight = false
            };

            selectedNotesLabel = new Label
            {
                Text = "Selected Workout Notes",
                Font = new Font(new FontFamily("Segoe UI"), 12F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new System.Drawing.Point(25, 490)
            };

            selectedNotesBox = new TextBox
            {
                Location = new System.Drawing.Point(25, 520),
                Width = 470,
                Height = 130,
                Multiline = true,
                ReadOnly = true,
                BackColor = Color.FromArgb(45, 45, 45),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font(new FontFamily("Segoe UI"), 11F, FontStyle.Bold)
            };

            inputPanel.Controls.AddRange(new Control[] 
            { 
                headerLabel, datePicker, exerciseTypeCombo, durationLabel, 
                hoursNumeric, hoursLabel, minutesNumeric, minutesLabel,
                caloriesLabel, caloriesTextBox, notesLabel, notesTextBox, addButton
            });

            listPanel.Controls.AddRange(new Control[]
            {
                listHeaderLabel, workoutList, selectedNotesLabel, selectedNotesBox
            });

            this.Controls.AddRange(new Control[] { inputPanel, listPanel });

            workoutList.SelectedIndexChanged += (sender, e) =>
            {
                if (workoutList.SelectedIndex != -1)
                {
                    var selectedWorkout = workouts[workoutList.SelectedIndex];
                    selectedNotesBox.Text = selectedWorkout.Notes;
                }
                else
                {
                    selectedNotesBox.Text = string.Empty;
                }
            };

            addButton.Click += (sender, e) =>
            {
                if (string.IsNullOrEmpty(exerciseTypeCombo.Text) ||
                    !int.TryParse(caloriesTextBox.Text, out int calories))
                {
                    MessageBox.Show("Please fill in all required fields with valid values.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int hours = (int)hoursNumeric.Value;
                int minutes = (int)minutesNumeric.Value;
                if (hours == 0 && minutes == 0)
                {
                    MessageBox.Show("Duration must be greater than 0 minutes.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var workout = new Workout(
                    datePicker.Value,
                    exerciseTypeCombo.Text,
                    hours,
                    minutes,
                    calories,
                    notesTextBox.Text
                );

                workouts.Add(workout);
                SaveWorkouts();
                RefreshWorkoutList(workoutList);
                ClearInputs(exerciseTypeCombo, caloriesTextBox, notesTextBox);
            };

            RefreshWorkoutList(workoutList);
            ApplyRoundedCorners();
        }

        private void ApplyRoundedCorners()
        {
            foreach (Control control in this.Controls)
            {
                if (control is Panel panel)
                {
                    panel.Paint += (sender, e) =>
                    {
                        using (var path = new GraphicsPath())
                        {
                            int radius = 25;
                            Rectangle rect = panel.ClientRectangle;
                            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
                            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
                            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
                            path.CloseAllFigures();
                            panel.Region = new Region(path);

                            // Draw border
                            using (var pen = new Pen(Color.FromArgb(60, 60, 60), 2))
                            {
                                e.Graphics.DrawPath(pen, path);
                            }
                        }
                    };

                    foreach (Control childControl in panel.Controls)
                    {
                        if (childControl is TextBox || childControl is ComboBox || childControl is NumericUpDown || childControl is ListBox)
                        {
                            childControl.Paint += (sender, e) =>
                            {
                                using (var path = new GraphicsPath())
                                {
                                    int radius = 2;
                                    Rectangle rect = childControl.ClientRectangle;
                                    path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                                    path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
                                    path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
                                    path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
                                    path.CloseAllFigures();
                                    childControl.Region = new Region(path);

                                    // Draw border
                                    using (var pen = new Pen(Color.FromArgb(80, 80, 80), 2))
                                    {
                                        e.Graphics.DrawPath(pen, path);
                                    }
                                }
                            };
                        }
                        else if (childControl is Button button)
                        {
                            button.Paint += (sender, e) =>
                            {
                                using (var path = new GraphicsPath())
                                {
                                    int radius = 15;
                                    Rectangle rect = button.ClientRectangle;
                                    path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                                    path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
                                    path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
                                    path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
                                    path.CloseAllFigures();
                                    button.Region = new Region(path);

                                    // Draw border
                                    using (var pen = new Pen(Color.FromArgb(0, 102, 184), 2))
                                    {
                                        e.Graphics.DrawPath(pen, path);
                                    }
                                }
                            };
                        }
                    }
                }
            }
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
                    $"{w.Date}|{w.ExerciseType}|{w.Hours}|{w.Minutes}|{w.CaloriesBurned}|{w.Notes}");
                File.WriteAllLines(dataFile, lines);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving workouts: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    if (parts.Length >= 5)
                    {
                        var workout = new Workout(
                            DateTime.Parse(parts[0]),
                            parts[1],
                            int.Parse(parts[2]),
                            int.Parse(parts[3]),
                            int.Parse(parts[4]),
                            parts.Length > 5 ? parts[5] : ""
                        );
                        workouts.Add(workout);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading workouts: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
} 