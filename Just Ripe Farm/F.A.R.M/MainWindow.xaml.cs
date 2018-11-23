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
        Employee user;

        public MainWindow(Employee user)
        {
            InitializeComponent();

            if (user is Manager)
            {
                // Remove features unsuitable for a manager here.                
            }
            else
            {
                // Remove features unsuitable for an Employee here.
                MainMenu.Items.Remove(DataManagement);
                this.user = user as Manager;
            }

            FillUserList();
        }

        private void CalendarSubmit_Click(object sender, RoutedEventArgs e)
        {

            if (datePickerStartDate.SelectedDate != null && datePickerEndDate != null) // Log a test this will fail.
            {
                string startDate = datePickerStartDate.SelectedDate.Value.ToString("yyyy-MM-dd");
                string endDate = datePickerEndDate.SelectedDate.Value.ToString("yyyy-MM-dd");

                upcomingHarvestGrid.ItemsSource = DatabaseConnection.DataConn.GetUpcomingHarvests(startDate, endDate).DefaultView;
                plannedHarvestGrid.ItemsSource = DatabaseConnection.DataConn.GetPlannedHarvests(startDate, endDate).DefaultView;

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

