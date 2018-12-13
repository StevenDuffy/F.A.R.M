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
using F.A.R.M.ChildWindows;


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
                MainMenu.Items.Remove(JobAssignments);
                
            }
            else
            {
                // Remove features unsuitable for an Employee here.
                MainMenu.Items.Remove(DataManagement);
                MainMenu.Items.Remove(CreateJob);
                YourAccountName.Content = CurrentSession.CurrentUser.FirstName + " " + CurrentSession.CurrentUser.SecondName;
            }

            //Fill Data Grids on Data Management
            FillUserList();
            FillVehicleList();
            FillCropList();
            FillStorageList();
            FillInCrops();
            FillInCropQuantity();
            FillInCropStorage();
            FillInStaffJob();
            FillInFieldLocation();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CropStorageComboBox.ItemsSource = DatabaseConnection.DataConn.GetCropStorage();
        }

        private void CalendarSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (HarvestCalendarStartDate.SelectedDate != null && HarvestCalendarEndDate != null) // Log a test this will fail.
            {
                string startDate = HarvestCalendarStartDate.SelectedDate.Value.ToString("yyyy-MM-dd");
                string endDate = HarvestCalendarEndDate.SelectedDate.Value.ToString("yyyy-MM-dd");

                UpcomingHarvestsGrid.ItemsSource = DatabaseConnection.DataConn.GetUpcomingHarvests(startDate, endDate).DefaultView;
                PlannedHarvestGrid.ItemsSource = DatabaseConnection.DataConn.GetPlannedHarvests(startDate, endDate).DefaultView;
                PlannedSowingGrid.ItemsSource = DatabaseConnection.DataConn.GetPlannedSowing(startDate, endDate).DefaultView;
                TotalRequiredFertiliserGrid.ItemsSource = DatabaseConnection.DataConn.GetRequiredFertiliser(startDate, endDate).DefaultView;
            }
            else
            {
                MessageBox.Show("Please enter a valid date.", "Invalid Dates Entered", MessageBoxButton.OK);
            }
        }

        /* Kept in for testing - ignore this method
        private void CropStorageComboBox_DropDownOpened(object sender, EventArgs e)
        {
            
            //cropStorageComboBox.ItemsSource = DatabaseConnection.DataConn.GetCropStorage();

        }*/

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Login loginWindow = new Login();
            loginWindow.Show();
            this.Close();
        }
        private void CropStorageAdd_Click(object sender, RoutedEventArgs e)
        {

            if (!short.TryParse(AddRemoveCropStorageValue.Text, out short amountToAdd))
            {
                MessageBox.Show("Please enter a valid number.", "Invalid data");
            }

            else if (!CurrentSession.CurrentUser.AddCropStock((Storage)CropStorageComboBox.SelectedItem, amountToAdd))
            {
                MessageBox.Show("The amount you have entered exceeds the storage capacity.", "Maximum Storage Limit Exceeded");
                ActivityLogger.Logger.AddCropStockFailed(amountToAdd);
            }
            else if (MessageBox.Show("Are you sure you wish to add " + amountToAdd + "kgs?", "Add Stock", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                DatabaseConnection.DataConn.UpdateCropStorage(((Storage)CropStorageComboBox.SelectedItem).StorageNumber, ((Storage)CropStorageComboBox.SelectedItem).UsedCapacity += amountToAdd);
                ActivityLogger.Logger.AddCropStockSucceeded(amountToAdd);
            }
        }



        private void CropStorageRemove_Click(object sender, RoutedEventArgs e)
        {
            if (!short.TryParse(AddRemoveCropStorageValue.Text, out short amountToRemove))
            {
                MessageBox.Show("Please enter a valid number.", "Invalid data");
            }


            else if (!CurrentSession.CurrentUser.RemoveCropStock((Storage)CropStorageComboBox.SelectedItem, amountToRemove))
            {
                MessageBox.Show("The amount you have entered exceeds the remaining stock level", "Remaining Stock Level Exceeded");
                ActivityLogger.Logger.RemoveCropStockFailed(amountToRemove);
            }
            else if (MessageBox.Show("Are you sure you wish to remove " + amountToRemove + "kgs?", "Add Stock", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                DatabaseConnection.DataConn.UpdateCropStorage(((Storage)CropStorageComboBox.SelectedItem).StorageNumber, ((Storage)CropStorageComboBox.SelectedItem).UsedCapacity -= amountToRemove);
                ActivityLogger.Logger.RemoveCropStockSucceeded(amountToRemove);
            }
        }










        private void FillUserList()
        {
            //Bind each row as a source
            _dmListGridUser.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = DatabaseConnection.DataConn.GetUserList() });
            //_dmListGridUser.ItemsSource = DatabaseConnection.DataConn.GetUserList().DefaultView;
        }

        private void FillVehicleList()
        {
            _dmListGridVehicle.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = DatabaseConnection.DataConn.GetVehicleList() });
        }

        private void FillCropList()
        {
            _dmListGridCrop.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = DatabaseConnection.DataConn.GetCropList() });
        }

        private void FillStorageList()
        {
            _dmListGridStorage.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = DatabaseConnection.DataConn.GetStorageList() });
        }

        public void FillInCrops()
        {
            DataTable dt = DatabaseConnection.DataConn.GetCrops();

            //TO DO iterate through the datatable dt and get the items 

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SelectCrop.Items.Add(dt.Rows[i].ItemArray[0]);

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
            DataTable dt = DatabaseConnection.DataConn.GetCropQuantity();

            //TO DO iterate through the datatable dt and get the items 
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CropQuantity.Items.Add(dt.Rows[i].ItemArray[0]);

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
            DataTable dt = DatabaseConnection.DataConn.GetStorageType();

            //TO DO iterate through the datatable dt and get the items 
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SelectStorage.Items.Add(dt.Rows[i].ItemArray[0]);

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



        public void FillInStaffJob()
        {
            DataTable dt = DatabaseConnection.DataConn.GetEmployeeID();

            //TO DO iterate through the datatable dt and get the items 
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SelectStaffJob.Items.Add(dt.Rows[i].ItemArray[0]);

            }

        }

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



        public void FillInFieldLocation()
        {
            DataTable dt = DatabaseConnection.DataConn.GetFieldNumber();

            // TO DO iterate through the datatable dt and get the items 
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SelectFieldLocation.Items.Add(dt.Rows[i].ItemArray[0]);

            }

        }

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


        //User buttons logic
        private void AddUserClick(object sender, RoutedEventArgs e)
        {
            AddUser addUser = new AddUser();
            addUser.Show();
            this.Close();
        }
        
        private void ModifyUserClick(object sender, RoutedEventArgs e)
        {
            ModifyUser _ModifyUser = new ModifyUser();
            _ModifyUser.Show();
            this.Close();
        }

        private void DeleteUserClick(object sender, RoutedEventArgs e)
        {
            DeleteUser _deleteUser = new DeleteUser();
            _deleteUser.Show();
            this.Close();
        }

        //Vehicle Logic Buttons
        private void AddVehicleClick(object sender, RoutedEventArgs e)
        {
            AddVehicle addVehicle = new AddVehicle();
            addVehicle.Show();
            this.Close();
        }

        private void ModifyVehicleClick(object sender, RoutedEventArgs e)
        {
            ModifyVehicle _ModifyVehicle = new ModifyVehicle();
            _ModifyVehicle.Show();
            this.Close();
        }

        private void DeleteVehicleClick(object sender, RoutedEventArgs e)
        {
            DeleteVehicle _deleteVehicle = new DeleteVehicle();
            _deleteVehicle.Show();
            this.Close();
        }

        //Crop Buttons Logic
        private void AddCropClick(object sender, RoutedEventArgs e)
        {
            AddCrop _addCrop = new AddCrop();
            _addCrop.Show();
            this.Close();
        }

        private void ModifyCropClick(object sender, RoutedEventArgs e)
        {
            ModifyCrop _ModifyCrop = new ModifyCrop();
            _ModifyCrop.Show();
            this.Close();
        }

        private void DeleteCropClick(object sender, RoutedEventArgs e)
        {
            DeleteCrop _deleteCrop = new DeleteCrop();
            _deleteCrop.Show();
            this.Close();
        }

        //Storage Buttons Logic
        private void AddStorageClick(object sender, RoutedEventArgs e)
        {
            AddStorage _addStorage = new AddStorage();
            _addStorage.Show();
            this.Close();
        }

        private void ModifyStorageClick(object sender, RoutedEventArgs e)
        {
            ModifyStorage _ModifyStorage = new ModifyStorage();
            _ModifyStorage.Show();
            this.Close();
        }

        private void DeleteStorageClick(object sender, RoutedEventArgs e)
        {
            DeleteStorage _deleteStorage = new DeleteStorage();
            _deleteStorage.Show();
            this.Close();
        }

    }
}


