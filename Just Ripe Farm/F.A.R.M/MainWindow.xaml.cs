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

        }

        private void CalendarSubmit_Click(object sender, RoutedEventArgs e)
        {

            if (datePickerStartDate.SelectedDate != null && datePickerEndDate != null)
            {
                string startDate = datePickerStartDate.SelectedDate.Value.ToString("yyyy-MM-dd");
                string endDate = datePickerEndDate.SelectedDate.Value.ToString("yyyy-MM-dd");
                
                calendarData.ItemsSource = DatabaseConnection.DataConn.GetFutureHarvests(startDate, endDate).DefaultView;
                
                //for(int i = 0;i < calendarData.Items. ;))
               // DateTime.ParseExact(calendarData.Columns[3].ToString(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);


                // calendarData.AutoGenerateColumns = true;//calendarData.Visibility = Visibility.Visible;

            }
            else
            {
                //This will be changed to validate on if null
                MessageBox.Show("Please enter a valid date.", "Invalid Dates Entered", MessageBoxButton.OK);
            }

            

        }


    }
}

