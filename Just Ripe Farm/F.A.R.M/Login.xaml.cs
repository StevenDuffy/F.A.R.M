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
        /// Connection to Farm database.
        /// </summary>
        private readonly DatabaseConnection connectionToDB;         

        /// <summary>
        /// Creates new Login form.
        /// </summary>
        public Login()
        {
            Thread.Sleep(3000);

            InitializeComponent();

            // create a new object to manipulate data 
            connectionToDB = new DatabaseConnection(connectionString);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Employee user;

            // Open the connection to the database
            connectionToDB.Open();

            DataTable data = new DataTable();

            data = connectionToDB.GetUserDetail(usernameBox.Text);

            connectionToDB.Close();

            if (data.Rows.Count == 0)
            {
                MessageBox.Show("Please enter a valid username.", "Incorrect Username", MessageBoxButton.OK);
                usernameBox.Text = null;
                passwordBox.Password = null;
            }
            else
            {
                if (passwordBox.Password.ToString() != data.Rows[0]["password"].ToString())
                {
                    MessageBox.Show("The password is incorrect. Please try again.", "Incorrect Password", MessageBoxButton.OK);
                    usernameBox.Text = null;
                    passwordBox.Password = null;

                }
                else
                {
                    switch (Convert.ToInt32(data.Rows[0]["privilege_Level"].ToString()))
                    {
                        case 1:
                            user = new Manager();
                            break;
                        default:
                            user = new Employee();
                            break;
                    }

                    MainWindow mainWindow = new MainWindow(connectionToDB, user);
                    mainWindow.Show();
                    this.Close();

                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
          
        }
    }
}

