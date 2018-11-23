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
using System.Windows.Navigation;
using System.Windows.Shapes;
using F.A.R.M.Properties;
using System.Data.SqlClient;
using System.Data;
using FarmControl;

namespace F.A.R.M
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow(Session session)
        {
            InitializeComponent();

            if (session.Privilege_Level == 0)
            {
                MainMenu.Items.Remove(DataManagement);
            }
            else
            {
                // remove features unsuitable for Manager here i.e. Your Jobs.
            }

            FillUserList();
        }

        private void CalendarSubmit_Click(object sender, RoutedEventArgs e)
        {

            if (datePickerStartDate.SelectedDate != null && datePickerEndDate != null) // Log a test this will fail.
            {
                string startDate = datePickerStartDate.SelectedDate.Value.ToString("yyyy-MM-dd");
                string endDate = datePickerEndDate.SelectedDate.Value.ToString("yyyy-MM-dd");
                
                calendarData.ItemsSource = DatabaseConnection.DataConn.GetFutureHarvests(startDate, endDate).DefaultView;            
            }
            else
            {                
                MessageBox.Show("Please enter a valid date.", "Invalid Dates Entered", MessageBoxButton.OK);
            }

            

        }

        private void FillUserList()
        {
            //Bind each row as a source
            _dmListGridUser.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = DatabaseConnection.DataConn.GetUserList() });
            //_dmListGridUser.ItemsSource = DatabaseConnection.DataConn.GetUserList().DefaultView;
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            Login loginWindow = new Login();
            loginWindow.Show();
            this.Close();
        }
    }
}

