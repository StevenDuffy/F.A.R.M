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
using System.Data;


namespace F.A.R.M
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private DatabaseConnection connectionToDB;

        public Login()
        {
            InitializeComponent();

            // Get the connection string for the database
            string connectionString = Settings.Default.ConnectFarmDB;

            // create a new object to manipulate data 
            connectionToDB = new DatabaseConnection(connectionString);

            // Open the connection to the database
            connectionToDB.Open();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataTable data = new DataTable();

            data = connectionToDB.GetUserDetail(usernameBox.Text);


            if (data.Rows.Count == 0)
            {
                MessageBox.Show("Please enter a valid username.", "Incorrect Username", MessageBoxButton.OK);
            }
            else
            {
                if (passwordBox.Password.ToString() != data.Rows[0]["password"].ToString())
                {
                    MessageBox.Show("The password is incorrect. Please try again.", "Incorrect Password", MessageBoxButton.OK);
                }
                else
                {

                    MainWindow mainWindow = new MainWindow(connectionToDB);
                    mainWindow.Show();
                    this.Hide();

                    switch (Convert.ToInt32(data.Rows[0]["privilege_Level"].ToString()))
                    {
                        case 1:

                            break;
                        case 2:
                            break;

                        default:
                            break;
                    }                    
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            connectionToDB.Close();

        }
    }
}

