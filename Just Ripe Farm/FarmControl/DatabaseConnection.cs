using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using FarmControl.Properties;
using System.Collections.ObjectModel;

namespace FarmControl
{
    public class DatabaseConnection
    {
        private static DatabaseConnection dataConn;

        /// <summary>
        /// String used for the connection to the database.
        /// </summary>
        private readonly string connectionString;

        private readonly SqlConnection dBconnection;

        private Employee user;

        private SqlCommand Command { get; set; }

        private SqlDataAdapter Adapter { get; set; }

        private DataTable DataTable { get; set; }

        public static DatabaseConnection DataConn
        {
            get
            {
                if (dataConn == null)
                {
                    dataConn = new DatabaseConnection();
                }

                return dataConn;
            }
        }

        /// <summary>
        /// Constructs an object that offers services to manipulate the chosen database.
        /// </summary>
        /// <param name="connectionStr"> Contains the selected connection string.</param>
        private DatabaseConnection()
        {
            connectionString = FarmControl.Properties.Settings.Default.FarmDBConnStr;  //System.Configuration.ConfigurationManager.ConnectionStrings["FarmControl.Properties.Settings.FarmDBConnStr"].ToString();

            this.dBconnection = new SqlConnection(connectionString);
        }

        /// <summary>
        /// Open the connection to the database.
        /// </summary>
        public void Open()
        {
            this.dBconnection.Open();
        }

        /// <summary>
        /// Close the connection to the database.
        /// </summary>
        public void Close()
        {
            this.dBconnection.Close();
        }

        public DataTable GetUserList()
        {
            DataTable userList = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(SQLConstant.getUserList, dBconnection);
            adapter.Fill(userList);
            return userList;
        }

        public DataTable GetVehicleList()
        {
            DataTable vehicleList = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(SQLConstant.getVehicleList, dBconnection);
            adapter.Fill(vehicleList);
            return vehicleList;
        }

        public DataTable GetCropList()
        {
            DataTable cropList = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(SQLConstant.getCrops, dBconnection);
            adapter.Fill(cropList);
            return cropList;
        }

        public DataTable GetFertiliserList()
        {
            DataTable _fertiliserList = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(SQLConstant.getFertiliser, dBconnection);
            adapter.Fill(_fertiliserList);
            return _fertiliserList;
        }

        public DataTable GetStorageList()
        {
            DataTable storageList = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(SQLConstant.getCropStorage, dBconnection);
            adapter.Fill(storageList);
            return storageList;
        }

        public DataTable GetUpcomingHarvests(string startDate, string endDate)
        {
            DataTable = new DataTable();
            Adapter = new SqlDataAdapter(SQLConstant.getUpcomingHarvests, dBconnection);
            Adapter.SelectCommand.Parameters.Add("@startDate", SqlDbType.VarChar).Value = startDate;
            Adapter.SelectCommand.Parameters.Add("@endDate", SqlDbType.VarChar).Value = endDate;
            Adapter.Fill(DataTable);
            return DataTable;
        }

        public DataTable GetPlannedHarvests(string startDate, string endDate)
        {
            DataTable = new DataTable();
            Adapter = new SqlDataAdapter(SQLConstant.getPlannedHarvests, dBconnection);
            Adapter.SelectCommand.Parameters.Add("@startDate", SqlDbType.VarChar).Value = startDate;
            Adapter.SelectCommand.Parameters.Add("@endDate", SqlDbType.VarChar).Value = endDate;
            Adapter.Fill(DataTable);
            return DataTable;
        }

        public DataTable GetPlannedSowing(string startDate, string endDate)
        {
            DataTable = new DataTable();
            Adapter = new SqlDataAdapter(SQLConstant.getPlannedSowing, dBconnection);
            Adapter.SelectCommand.Parameters.Add("@startDate", SqlDbType.VarChar).Value = startDate;
            Adapter.SelectCommand.Parameters.Add("@endDate", SqlDbType.VarChar).Value = endDate;
            Adapter.Fill(DataTable);
            return DataTable;
        }

        public DataTable GetRequiredFertiliser(string startDate, string endDate)
        {
            DataTable = new DataTable();
            Adapter = new SqlDataAdapter(SQLConstant.getTotalFertiliserRequired, dBconnection);
            Adapter.SelectCommand.Parameters.Add("@startDate", SqlDbType.VarChar).Value = startDate;
            Adapter.SelectCommand.Parameters.Add("@endDate", SqlDbType.VarChar).Value = endDate;
            Adapter.Fill(DataTable);
            return DataTable;
        }
        
        public void UpdateCropStorage(byte storageNumber, short usedCapacity)
        {
            Command = new SqlCommand(SQLConstant.updateCropStorage, dBconnection);
            Command.Parameters.AddWithValue("@usedCapacity", usedCapacity);
            Command.Parameters.AddWithValue("@storageNumber", storageNumber);
            this.Open();
            Command.ExecuteNonQuery();
            this.Close();
        }

        public void UpdateFertiliserStorage(byte storageNumber, short usedCapacity)
        {
            Command = new SqlCommand(SQLConstant.updateFertiliserStorage, dBconnection);
            Command.Parameters.AddWithValue("@usedCapacity", usedCapacity);
            Command.Parameters.AddWithValue("@storageNumber", storageNumber);
            this.Open();
            Command.ExecuteNonQuery();
            this.Close();
        }

        public Employee GetUser(string username)
        {
            user = new Employee();

            Command = new SqlCommand(SQLConstant.getUser, dBconnection);
            Command.Parameters.AddWithValue("@username", username);
            this.Open();

            {
                {
                    SqlDataReader reader = Command.ExecuteReader();

                    while (reader.Read())
                    {
                        user.EmployeeID = (int)reader["employee_ID"];
                        user.FirstName = (string)reader["first_name"];
                        user.SecondName = (string)reader["second_name"];
                        user.Username = (string)reader["username"];
                        user.Password = (string)reader["password"];
                        user.PrivilegeLevel = (byte)reader["privilege_level"];
                    }
                }
            }

            this.Close();

            return user;
        }

        public ObservableCollection<Storage> GetCropStorage()
        {
            ObservableCollection<Storage> cropStorage = new ObservableCollection<Storage>();

            Command = new SqlCommand(SQLConstant.getCropStorage, dBconnection);

            this.Open();

            {
                {
                    SqlDataReader reader = Command.ExecuteReader();

                    int i = 0;

                    while (reader.Read())
                    {
                        cropStorage.Add(new Storage());
                        cropStorage[i].StorageNumber = (byte)reader["storage_number"];
                        cropStorage[i].StorageType = (string)reader["storage_type"];
                        cropStorage[i].CropStored = (string)reader["crop_stored"];
                        cropStorage[i].MaxCapacity = (short)reader["max_capacity"];
                        cropStorage[i].UsedCapacity = (short)reader["used_capacity"];
                        cropStorage[i].StorageTemperature = (byte)reader["storage_temperature"];
                        i++;
                    }
                }
            }

            this.Close();

            return cropStorage;
        }

        public ObservableCollection<Storage> GetFertiliserStorage()
        {
            ObservableCollection<Storage> fertiliserStorage = new ObservableCollection<Storage>();

            Command = new SqlCommand(SQLConstant.getFertiliserStorage, dBconnection);

            this.Open();

            {
                {
                    SqlDataReader reader = Command.ExecuteReader();

                    int i = 0;

                    while (reader.Read())
                    {
                        fertiliserStorage.Add(new Storage());
                        fertiliserStorage[i].StorageNumber = (byte)reader["storage_number"];
                        fertiliserStorage[i].StorageType = (string)reader["storage_type"];
                        fertiliserStorage[i].CropStored = (string)reader["crop_stored"];
                        fertiliserStorage[i].MaxCapacity = (short)reader["max_capacity"];
                        fertiliserStorage[i].UsedCapacity = (short)reader["used_capacity"];
                        fertiliserStorage[i].StorageTemperature = (byte)reader["storage_temperature"];
                        i++;
                    }
                }
            }

            this.Close();

            return fertiliserStorage;
        }


        public DataTable GetCrops()
        {
            DataTable dtCrops = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(SQLConstant.getCrops, dBconnection);
            adapter.Fill(dtCrops);
            return dtCrops;
        }

        public DataTable GetCropQuantity()
        {
            DataTable dtCropQuantity = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(SQLConstant.getCropQuantity, dBconnection);
            adapter.Fill(dtCropQuantity);
            return dtCropQuantity;
        }

        public DataTable GetStorageType()
        {
            DataTable dtStorageType = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(SQLConstant.getStorageType, dBconnection);
            adapter.Fill(dtStorageType);
            return dtStorageType;
        }

        public DataTable GetEmployeeID()
        {
            DataTable dtEmployeeID = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(SQLConstant.getEmployeeID, dBconnection);
            adapter.Fill(dtEmployeeID);
            return dtEmployeeID;
        }

        public DataTable GetFieldNumber()
        {
            DataTable dtFieldNumber = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(SQLConstant.getFieldNumber, dBconnection);
            adapter.Fill(dtFieldNumber);
            return dtFieldNumber;
        }

        public void AddUserToDB(int _userID, string _firstName, string _lastName, string _uName, string _pWord, int _priv)
        {
            Command = new SqlCommand(SQLConstant.addUserToDB, dBconnection);
            Command.Parameters.AddWithValue("@employee_ID", _userID);
            Command.Parameters.AddWithValue("@first_Name", _firstName);
            Command.Parameters.AddWithValue("@second_Name", _lastName);
            Command.Parameters.AddWithValue("@user_Name", _uName);
            Command.Parameters.AddWithValue("@password", _pWord);
            Command.Parameters.AddWithValue("@privilege_Level", _priv);
            this.Open();
            Command.ExecuteNonQuery();
        }

        public void EditUserInDB(int _userID, string _firstName, string _lastName, string _uName, int _priv)
        {
            Command = new SqlCommand(SQLConstant.editUserInDB, dBconnection);
            Command.Parameters.AddWithValue("@employee_ID", _userID);
            Command.Parameters.AddWithValue("@first_Name", _firstName);
            Command.Parameters.AddWithValue("@second_Name", _lastName);
            Command.Parameters.AddWithValue("@user_Name", _uName);
            Command.Parameters.AddWithValue("@privilege_Level", _priv);
            this.Open();
            Command.ExecuteNonQuery();
        }


        public void AddNewJob(string _Crops, string _CropQuantity, string _CropStorage, string _StaffMember, string _FieldLocation)
        {
            Command = new SqlCommand(SQLConstant.AddNewJob, dBconnection);
            Command.Parameters.AddWithValue("@Crop_name", _Crops);
            Command.Parameters.AddWithValue("@sowing_method", _CropQuantity);          
            Command.Parameters.AddWithValue("@harvest_method", _CropStorage);       
            Command.Parameters.AddWithValue("@cultivation_legnth", _StaffMember);          
            Command.Parameters.AddWithValue("@storage_max_length", _FieldLocation);
            Command.Parameters.AddWithValue("@storage_min_length", _FieldLocation);
            Command.Parameters.AddWithValue("@price_per_kilo", _FieldLocation);

            this.Open();                                                     
            Command.ExecuteNonQuery();
        }

        public void DeleteUserFromDB( string _uName, string _pWord)
        {
            Command = new SqlCommand(SQLConstant.deleteUserFromDB, dBconnection);
            Command.Parameters.AddWithValue("@user_Name", _uName);
            Command.Parameters.AddWithValue("@password", _pWord);
            this.Open();
            Command.ExecuteNonQuery();
            this.Close();
        }
    }
}
