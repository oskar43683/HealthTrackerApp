using System;
using System.Windows.Forms;

namespace HealthTrackerApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var loginForm = new LoginForm())
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    var mainForm = new MainForm(loginForm.CurrentUser);
                    Application.Run(mainForm);
                }
            }
        }
    }
} 