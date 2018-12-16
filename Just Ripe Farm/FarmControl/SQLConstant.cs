using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmControl
{
    class SQLConstant
    {
        public static string getUserDetails = "SELECT password, privilege_Level FROM dbo.Employee WHERE username = @username";
        public static string getUpcomingHarvests = "SELECT * FROM Field WHERE harvest_date BETWEEN @startDate AND @endDate";
        // DATA MANAGEMENT PAGE STATEMENTS BELOW
        public static string getUserList = "SELECT employee_ID, first_name, second_name, username, privilege_level FROM dbo.Employee";
        public static string getVehicleList = "SELECT registration_number, type, carry_capacity FROM dbo.Vehicle";
        // DATA MANAGEMENT PAGE STATEMENTS ABOVE
        public static string getPlannedHarvests = "SELECT * FROM Job WHERE job_Type = 'Harvest' AND start_date BETWEEN @startDate AND @endDate AND status = 'PENDING'";
        public static string getPlannedSowing = "SELECT * FROM Job WHERE job_type = 'Sow' AND start_date BETWEEN @startDate AND @endDate AND status = 'PENDING'";
        public static string getCrops = "SELECT * FROM Crop";
        public static string getFertiliser = "SELECT * FROM Fertiliser";
        public static string getCropQuantity = "SELECT * FROM CropQuantity";
        public static string getStorageType = "SELECT * FROM Storage";
        public static string getEmployeeID = "SELECT * FROM Employee";
        public static string getFieldNumber = "SELECT * FROM Field";
        public static string getUser = "SELECT * FROM dbo.Employee WHERE username = @username";
        public static string getCropStorage = "SELECT * FROM Storage WHERE storage_type = 'Crop' ";
        public static string getFertiliserStorage = "SELECT * FROM Storage WHERE storage_type = 'fertiliser' ";
        public static string updateCropStorage = "UPDATE Storage SET used_capacity = @usedCapacity WHERE storage_number = @storageNumber";
        public static string updateFertiliserStorage = "UPDATE Storage SET used_capacity = @usedCapacity WHERE storage_number = @storageNumber";
        public static string getTotalFertiliserRequired = "SELECT SUM(individual_total) as total, fertiliser_type FROM (SELECT (crop_quantity * amount_per_kilo) AS individual_total, fertiliser_type FROM (SELECT crop_quantity, amount_per_kilo, fertiliser_type FROM job JOIN RequiredFertiliser ON crop_name = assigned_crop WHERE job_type = 'Sow' AND start_date BETWEEN @startDate AND @endDate ) AS a) AS b GROUP BY fertiliser_type";
        //Add to DB below
        public static string addUserToDB = "SET IDENTITY_INSERT dbo.Employee ON; INSERT INTO dbo.Employee(employee_ID, first_Name, second_Name, user_Name, password, privilege_Level) VALUES(@employee_ID, @first_Name, @second_Name, @user_Name, @password, @privilege_Level) SET IDENTITY_INSERT dbo.Employee OFF;";
        public static string editUserInDB = "UPDATE Employee SET first_Name = @first_Name, second_Name = @second_Name, user_Name = @user_Name, privilege_Level = @privilege_LevelWHERE employee_ID = @employee_ID;";
        public static string AddNewJob = "SET IDENTITY_INSERT dbo.Crop ON; INSERT INTO dbo.Crop(Crop_name, sowing_method, harvest_method, cultivation_length, storage_max_length, storage_min_length, price_per_kilo) VALUES(@Crop_name, @sowing_method, @harvest_method, @cultivation_length, @storage_max_length, @storage_min_length, @price_per_kilo) SET IDENTITY_INSERT dbo.Crop OFF;";
        public static string deleteUserFromDB = "DELETE FROM dbo.Employee WHERE username=@user_Name AND password=@password";
       
    }
}
