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

        public void SelectCrop_Load(object sender, EventArgs e)
        {
            // Create the connection to the database
            SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-381U2JOA\sqlexpress;Initial Catalog=Northwind;User ID=sa;Password=xyz");

            conn.Open();
            //Declare what you would like to be shown in the combo box
            SqlCommand sc = new SqlCommand("select crop_name", conn);
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("SelectCrop", typeof(string));
            dt.Columns.Add("SelectCrop", typeof(string));
            dt.Load(reader);

            //SelectCrop.DataSource = dt;
           // SelectCrop.ValueMember = "";
           // SelectCrop.DisplayMember = "";

            conn.Close();

        }
        private void SelectCrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ID = SelectCrop.SelectedValue.ToString();
        }

      private void CropQuantity_Load(object sender, EventArgs e)
       {

            SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-381U2JOA\sqlexpress;Initial Catalog=Northwind;User ID=sa;Password=xyz");
            conn.Open();
            SqlCommand sc = new SqlCommand("quantity", conn);
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CropQuantity", typeof(string));
            dt.Columns.Add("CropQuantity", typeof(string));
            dt.Load(reader);

          //  CropQuantity.ValueMember = "customerid";
            //CropQuantity.DisplayMember = "contactname";
            //CropQuantity.DataSource = dt;

            conn.Close();


        }
        private void CropQuantity_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ID = CropQuantity.SelectedValue.ToString();
        }

        private void SelectStorage_Load(object sender, EventArgs e)
        {

            SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-381U2JOA\sqlexpress;Initial Catalog=Northwind;User ID=sa;Password=xyz");
            conn.Open();
            SqlCommand sc = new SqlCommand("storage_type", conn);
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("SlectStorage", typeof(string));
            dt.Columns.Add("SelectStorage", typeof(string));
            dt.Load(reader);

           // SelectStorage.ValueMember = "customerid";
           // SelectStorage.DisplayMember = "contactname";
           // SelectStorage.DataSource = dt;

            conn.Close();


        }
        public void SlectStorage_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ID = SelectStorage.SelectedValue.ToString();
        }
        public void SelectStaffJob_Load(object sender, EventArgs e)
        {

            SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-381U2JOA\sqlexpress;Initial Catalog=Northwind;User ID=sa;Password=xyz");
            conn.Open();
            SqlCommand sc = new SqlCommand("employee-ID", conn);
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("SlectStaffJob", typeof(string));
            dt.Columns.Add("SelectStaffJob", typeof(string));
            dt.Load(reader);

          //  SelectStaffJob.ValueMember = "customerid";
          //  SelectStaffJob.DisplayMember = "contactname";
          //  SelectStaffJob.DataSource = dt;

            conn.Close();


        }
        private void SlectStaffJob_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ID = SelectStaffJob.SelectedValue.ToString();
        }

        private void SelectFieldLocation_Load(object sender, EventArgs e)
        {

            SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-381U2JOA\sqlexpress;Initial Catalog=Northwind;User ID=sa;Password=xyz");
            conn.Open();
            SqlCommand sc = new SqlCommand("field_number", conn);
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("SlectFieldLocation", typeof(string));
            dt.Columns.Add("SelectFieldLocation", typeof(string));
            dt.Load(reader);

           // SelectFieldLocation.ValueMember = "customerid";
           // SelectFieldLocation.DisplayMember = "contactname";
          //  SelectFieldLocation.DataSource = dt;

            conn.Close();


        }
        private void SlectFieldLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ID = SelectFieldLocation.SelectedValue.ToString();
        }

    }
}    
   

