﻿using System;
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

        public MainWindow()
        {
            InitializeComponent();


            if (CurrentSession.CurrentUser.PrivilegeLevel == 1)
            {
                // Remove features unsuitable for a manager here.
                MessageBox.Show("You are logged in as a manager. Welcome back " + CurrentSession.CurrentUser.FirstName + " " + CurrentSession.CurrentUser.SecondName + ".");
            }
            else
            {
                // Remove features unsuitable for an Employee here.
                MessageBox.Show("You are logged in as a labourer. Welcome back " + CurrentSession.CurrentUser.FirstName + " " + CurrentSession.CurrentUser.SecondName + ".");

                MainMenu.Items.Remove(DataManagement);
            }

            FillUserList();
            FillInCrops();
            FillInCropQuantity();
            FillInCropStorage();
            // FillInStaffJob();
            // FillInFieldLocation();


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

        private void cropStorageComboBox_DropDownOpened(object sender, EventArgs e)
        {
            cropStorageComboBox.ItemsSource = DatabaseConnection.DataConn.GetCropStorage();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Login loginWindow = new Login();
            loginWindow.Show();
            this.Close();
        }





        private void FillUserList()
        {
            //Bind each row as a source
            _dmListGridUser.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = DatabaseConnection.DataConn.GetUserList() });
            //_dmListGridUser.ItemsSource = DatabaseConnection.DataConn.GetUserList().DefaultView;
        }

        public void FillInCrops()
        {
            DataTable dt = DatabaseConnection.DataConn.GetCrops();

            //TO DO iterate through the datatable dt and get the items 

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SelectCrop.Items.Add(SelectCrop.Items.Add(dt.Rows[i].ItemArray[0]));

            }



        }

        public void SelectCrop_Load(object sender, EventArgs e)
        {

            // Create the connection to the database
            SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-381U2JOA\sqlexpress;Initial Catalog=Northwind;User ID=sa;Password=xyz");

            conn.Open();
            //Declare what you would like to be shown in the combo box
            SqlCommand sc = new SqlCommand("select crop_name", conn); // add the sql statement
            SqlDataReader reader;



            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("SelectCrop", typeof(string));

            dt.Load(reader);

            SelectCrop.Items.Add("aaa");
            SelectCrop.Items.Add("bbb");
            /* SelectCrop.DataSource = dt;
             SelectCrop.ValueMember = "";
             SelectCrop.DisplayMember = "";
             */
            conn.Close();
        }

        public void FillInCropQuantity()
        {
            //      DataTable dt = DatabaseConnection.DataConn.GetCropQuantity();

            //TO DO iterate through the datatable dt and get the items 
            //      for (int i = 0; i < dt.Rows.Count; i++)
            {
                //          SelectCrop.Items.Add(SelectCrop.Items.Add(dt.Rows[i].ItemArray[0]));

            }
        }

        private void SelectCropQuantiy_SelectedIndexChanged(object sender, EventArgs e)


        {
            string ID = CropQuantity.SelectedValue.ToString();
        }

        public void CropQuantity_Load(object sender, EventArgs e)
        {

            SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-381U2JOA\sqlexpress;Initial Catalog=Northwind;User ID=sa;Password=xyz");
            conn.Open();
            SqlCommand sc = new SqlCommand("Select Crop_Quantity", conn);
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CropQuantity", typeof(string));

            dt.Load(reader);

            CropQuantity.Items.Add("111");
            CropQuantity.Items.Add("222");

            /* CropQuantity.ValueMember = "customerid";
             CropQuantity.DisplayMember = "contactname";
             CropQuantity.DataSource = dt;
             */
            conn.Close();


        }

        public void FillInCropStorage()
        {
            //        DataTable dt = DatabaseConnection.DataConn.GetStorageType();

            //TO DO iterate through the datatable dt and get the items 
            //      for (int i = 0; i < dt.Rows.Count; i++)
            {
                //          SelectCrop.Items.Add(SelectCrop.Items.Add(dt.Rows[i].ItemArray[0]));

            }

        }



        private void SelectStorage_Load(object sender, EventArgs e)
        {

            SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-381U2JOA\sqlexpress;Initial Catalog=Northwind;User ID=sa;Password=xyz");
            conn.Open();
            SqlCommand sc = new SqlCommand("Select storage_type", conn);
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("SlectStorage", typeof(string));

            dt.Load(reader);
            SelectStorage.Items.Add("ccc");
            SelectStorage.Items.Add("ddd");

            /*
            SelectStorage.ValueMember = "customerid";
            SelectStorage.DisplayMember = "contactname";
            SelectStorage.DataSource = dt;
            */
            conn.Close();


        }



        /* public void FillInStaffJob()
         {
             //       DataTable dt = DatabaseConnection.DataConn.GetEmployeeID();

             //TO DO iterate through the datatable dt and get the items 
             //      for (int i = 0; i < dt.Rows.Count; i++)
             {
                 //          SelectCrop.Items.Add(SelectCrop.Items.Add(dt.Rows[i].ItemArray[0]));

             }

         }*/

        public void SelectStaffJob_Load(object sender, EventArgs e)
        {
            //Theo, try to move this to the connection class, I will help.
            SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-381U2JOA\sqlexpress;Initial Catalog=Northwind;User ID=sa;Password=xyz");
            conn.Open();
            SqlCommand sc = new SqlCommand("employee-ID", conn);
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("SlectStaffJob", typeof(string));

            dt.Load(reader);
            SelectStaffJob.Items.Add("ccc");
            SelectStaffJob.Items.Add("ddd");

            /*
            SelectStaffJob.ValueMember = "customerid";
            SelectStaffJob.DisplayMember = "contactname";
            SelectStaffJob.DataSource = dt;
            */
            conn.Close();


        }



        /* public void FillInFieldLocation()
         {
            // DataTable dt = DatabaseConnection.DataConn.GetFieldNumber();

             TO DO iterate through the datatable dt and get the items 
          for (int i = 0; i < dt.Rows.Count; i++)
             {
                 SelectCrop.Items.Add(SelectCrop.Items.Add(dt.Rows[i].ItemArray[0]));

             }

         }*/

        private void SelectFieldLocation_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-381U2JOA\sqlexpress;Initial Catalog=Northwind;User ID=sa;Password=xyz");
            conn.Open();
            SqlCommand sc = new SqlCommand("Field_number", conn);
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("SelectFieldLocation", typeof(string));

            dt.Load(reader);
            SelectFieldLocation.Items.Add("ccc");
            SelectFieldLocation.Items.Add("ddd");

            /*
            SelectFieldLocation.ValueMember = "customerid";
            SelectFieldLocation.DisplayMember = "contactname";
            SelectFieldLocation.DataSource = dt;
            */
            conn.Close();
        }
    }
}


