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
          //   (datePickerStartDate.SelectedDate.Value.ToString("yyyy-MM-dd"));
            calendarData.ItemsSource = DatabaseConnection.DataConn.GetFutureHarvests(datePickerStartDate.SelectedDate.Value.ToString("yyyy-MM-dd"), datePickerEndDate.SelectedDate.Value.ToString("yyyy-MM-dd")).DefaultView;
        }
    }
}
