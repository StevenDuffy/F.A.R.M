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


namespace F.A.R.M
{
    /// <summary>
    /// UI logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        /// <summary>
        /// String for used for the connection to the database.
        /// </summary>
        private readonly string connectionString = Settings.Default.ConnectFarmDB;

        /// <summary>
        /// Stores connection to Farm database.
        /// </summary>
        private readonly DatabaseConnection connectionToDB;

        /// <summary>
        /// Stores object that offers verification methods.
        /// </summary>
        private readonly LoginVerifier loginVerifier;

        /// <summary>
        /// Creates new Login form.
        /// </summary>
        public Login()
        {
            InitializeComponent();

            // create a new object to manipulate data from the 
            connectionToDB = new DatabaseConnection(connectionString);

            // create object that offers verification methods
            loginVerifier = new LoginVerifier(connectionToDB);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (!loginVerifier.VerifyUser(usernameBox.Text, passwordBox.Password, out Employee user))
            {
                MessageBox.Show("Invalid credentials entered. Please try again.", "Invalid Credentials", MessageBoxButton.OK, MessageBoxImage.Information);
                usernameBox.Text = null;
                passwordBox.Password = null;
            }
            else
            {
                MainWindow mainWindow = new MainWindow(connectionToDB, user);
                mainWindow.Show();
                this.Close();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }
    }
}

