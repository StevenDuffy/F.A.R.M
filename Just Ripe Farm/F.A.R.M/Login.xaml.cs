using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using F.A.R.M.Properties;
using System.Data.SqlClient;
using System.Threading;
using System.Data;
using FarmControl;


namespace F.A.R.M
{
    /// <summary>
    /// UI logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        /// <summary>
        /// Creates new Login form.
        /// </summary>
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (!LoginVerifier.Verifier.VerifyUser(usernameBox.Text, passwordBox.Password))
            {
                MessageBox.Show("Invalid credentials entered. Please try again.", "Invalid Credentials", MessageBoxButton.OK, MessageBoxImage.Information);
                usernameBox.Text = null;
                passwordBox.Password = null;
            }
            else
            {

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }

            ActivityLogger.Logger.RecordLoginAttempt();
        }
    }
}

