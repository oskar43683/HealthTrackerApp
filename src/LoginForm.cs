using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace HealthTrackerApp
{
    public partial class LoginForm : Form
    {
        public User CurrentUser { get; private set; }

        public LoginForm()
        {
            InitializeComponent();
            ApplyRoundedCorners();
        }

        private void InitializeComponent()
        {
            this.txtUsername = new TextBox();
            this.txtPassword = new TextBox();
            this.btnLogin = new Button();
            this.btnRegister = new Button();
            this.label1 = new Label();
            this.label2 = new Label();
            this.titleLabel = new Label();
            
            this.Text = "Health Tracker - Login";
            this.Size = new System.Drawing.Size(400, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(18, 18, 18);
            this.ForeColor = Color.White;
            this.Font = new Font(new FontFamily("Segoe UI"), 10F, FontStyle.Regular);
            this.Padding = new Padding(20);

            this.titleLabel.Text = "Welcome to\nHealth Tracker";
            this.titleLabel.Font = new Font(new FontFamily("Segoe UI"), 24F, FontStyle.Bold);
            this.titleLabel.ForeColor = Color.White;
            this.titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.titleLabel.Location = new System.Drawing.Point(20, 40);
            this.titleLabel.Size = new System.Drawing.Size(360, 80);
            this.titleLabel.AutoSize = false;

            this.label1.Text = "Username";
            this.label1.Font = new Font(new FontFamily("Segoe UI"), 12F, FontStyle.Bold);
            this.label1.ForeColor = Color.White;
            this.label1.Location = new System.Drawing.Point(20, 120);
            this.label1.AutoSize = true;
            
            this.txtUsername.Location = new System.Drawing.Point(20, 150);
            this.txtUsername.Size = new System.Drawing.Size(360, 30);
            this.txtUsername.Font = new Font(new FontFamily("Segoe UI"), 12F, FontStyle.Regular);
            this.txtUsername.BackColor = Color.FromArgb(45, 45, 45);
            this.txtUsername.ForeColor = Color.White;
            this.txtUsername.BorderStyle = BorderStyle.FixedSingle;
            
            this.label2.Text = "Password";
            this.label2.Font = new Font(new FontFamily("Segoe UI"), 12F, FontStyle.Bold);
            this.label2.ForeColor = Color.White;
            this.label2.Location = new System.Drawing.Point(20, 200);
            this.label2.AutoSize = true;
            
            this.txtPassword.Location = new System.Drawing.Point(20, 230);
            this.txtPassword.Size = new System.Drawing.Size(360, 30);
            this.txtPassword.Font = new Font(new FontFamily("Segoe UI"), 12F, FontStyle.Regular);
            this.txtPassword.BackColor = Color.FromArgb(45, 45, 45);
            this.txtPassword.ForeColor = Color.White;
            this.txtPassword.BorderStyle = BorderStyle.FixedSingle;
            this.txtPassword.PasswordChar = '●';
            
            this.btnLogin.Text = "Login";
            this.btnLogin.Location = new System.Drawing.Point(20, 300);
            this.btnLogin.Size = new System.Drawing.Size(360, 45);
            this.btnLogin.Font = new Font(new FontFamily("Segoe UI"), 12F, FontStyle.Bold);
            this.btnLogin.BackColor = Color.FromArgb(0, 122, 204);
            this.btnLogin.ForeColor = Color.White;
            this.btnLogin.FlatStyle = FlatStyle.Flat;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.Cursor = Cursors.Hand;
            this.btnLogin.Click += new EventHandler(btnLogin_Click);
            
            this.btnRegister.Text = "Register New Account";
            this.btnRegister.Location = new System.Drawing.Point(20, 360);
            this.btnRegister.Size = new System.Drawing.Size(360, 45);
            this.btnRegister.Font = new Font(new FontFamily("Segoe UI"), 12F, FontStyle.Bold);
            this.btnRegister.BackColor = Color.FromArgb(45, 45, 45);
            this.btnRegister.ForeColor = Color.White;
            this.btnRegister.FlatStyle = FlatStyle.Flat;
            this.btnRegister.FlatAppearance.BorderSize = 0;
            this.btnRegister.Cursor = Cursors.Hand;
            this.btnRegister.Click += new EventHandler(btnRegister_Click);

            this.Controls.AddRange(new Control[] {
                this.titleLabel,
                this.label1, this.txtUsername,
                this.label2, this.txtPassword,
                this.btnLogin, this.btnRegister
            });

            this.btnLogin.MouseEnter += (s, e) => this.btnLogin.BackColor = Color.FromArgb(0, 102, 184);
            this.btnLogin.MouseLeave += (s, e) => this.btnLogin.BackColor = Color.FromArgb(0, 122, 204);
            this.btnRegister.MouseEnter += (s, e) => this.btnRegister.BackColor = Color.FromArgb(55, 55, 55);
            this.btnRegister.MouseLeave += (s, e) => this.btnRegister.BackColor = Color.FromArgb(45, 45, 45);
        }

        private void ApplyRoundedCorners()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 20, 20));
        }

        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnRegister;
        private Label label1;
        private Label label2;
        private Label titleLabel;

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            User user = User.LoadUser(username);
            if (user == null || !user.VerifyPassword(password))
            {
                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CurrentUser = user;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (User.LoadUser(username) != null)
            {
                MessageBox.Show("Username already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CurrentUser = new User(username, password);
            CurrentUser.SaveUser();
            MessageBox.Show("Registration successful! You can now login.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
} 