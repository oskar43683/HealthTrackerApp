using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;

namespace HealthTrackerApp
{
    public class LoginForm : Form
    {
        private TextBox usernameBox;
        private TextBox passwordBox;
        private Button loginButton;
        private Button registerButton;
        private Label statusLabel;
        private List<User> users;
        private const string USERS_FILE = "users.json";

        public User CurrentUser { get; private set; }

        public LoginForm()
        {
            users = new List<User>();
            LoadUsers();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Health Tracker - Login";
            this.Size = new Size(400, 500);
            this.BackColor = Color.FromArgb(18, 18, 18);
            this.ForeColor = Color.White;
            this.Font = new Font("Segoe UI", 10F);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            var titleLabel = new Label
            {
                Text = "Health & Training Tracker",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(50, 40)
            };

            usernameBox = new TextBox
            {
                Location = new Point(50, 120),
                Size = new Size(280, 30),
                BackColor = Color.FromArgb(45, 45, 45),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 12F),
                PlaceholderText = "Username"
            };

            passwordBox = new TextBox
            {
                Location = new Point(50, 170),
                Size = new Size(280, 30),
                BackColor = Color.FromArgb(45, 45, 45),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 12F),
                PlaceholderText = "Password",
                PasswordChar = '•'
            };

            loginButton = new Button
            {
                Text = "Login",
                Location = new Point(50, 230),
                Size = new Size(280, 40),
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            loginButton.Click += LoginButton_Click;

            registerButton = new Button
            {
                Text = "Register New User",
                Location = new Point(50, 290),
                Size = new Size(280, 40),
                BackColor = Color.FromArgb(45, 45, 45),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12F),
                Cursor = Cursors.Hand
            };
            registerButton.Click += RegisterButton_Click;

            statusLabel = new Label
            {
                Location = new Point(50, 350),
                Size = new Size(280, 40),
                ForeColor = Color.Red,
                Font = new Font("Segoe UI", 10F),
                TextAlign = ContentAlignment.MiddleCenter
            };

            this.Controls.AddRange(new Control[] { 
                titleLabel, usernameBox, passwordBox, 
                loginButton, registerButton, statusLabel 
            });

            // Create admin user if no users exist
            if (users.Count == 0)
            {
                users.Add(new User("admin", "admin123", true));
                SaveUsers();
            }
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string username = usernameBox.Text.Trim();
            string password = passwordBox.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                statusLabel.Text = "Please enter both username and password";
                return;
            }

            var user = users.Find(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                CurrentUser = user;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                statusLabel.Text = "Invalid username or password";
            }
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            string username = usernameBox.Text.Trim();
            string password = passwordBox.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                statusLabel.Text = "Please enter both username and password";
                return;
            }

            if (users.Exists(u => u.Username == username))
            {
                statusLabel.Text = "Username already exists";
                return;
            }

            users.Add(new User(username, password));
            SaveUsers();
            statusLabel.Text = "Registration successful! Please login.";
            statusLabel.ForeColor = Color.Green;
        }

        private void LoadUsers()
        {
            if (File.Exists(USERS_FILE))
            {
                string json = File.ReadAllText(USERS_FILE);
                users = JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
            }
        }

        private void SaveUsers()
        {
            string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(USERS_FILE, json);
        }
    }
} 