using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmControl
{
    class SQLConstant
    {
        public static string getUserDetails = "SELECT password, privilege_Level FROM dbo.Employee WHERE user_name = @username";
        public static string getUpcomingHarvests = "SELECT * FROM Field WHERE harvest_date BETWEEN @startDate AND @endDate";
        // DATA MANAGEMENT PAGE STATEMENTS BELOW
        public static string getUserList = "SELECT employee_ID, first_Name, second_Name, user_Name, privilege_Level FROM dbo.Employee";
        public static string getVehicleList = "SELECT registration_number, type, carry_capacity FROM dbo.Vehicle";
        // DATA MANAGEMENT PAGE STATEMENTS ABOVE
        public static string getPlannedHarvests = "SELECT * FROM Job WHERE job_Type = 'Harvest' AND start_date BETWEEN @startDate AND @endDate AND status = 'PENDING'";
        public static string getPlannedSowing = "SELECT * FROM Job WHERE job_type = 'Sow' AND start_date BETWEEN @startDate AND @endDate AND status = 'PENDING'";
        public static string getCrops = "SELECT * FROM Crop";
        public static string getCropQuantity = "SELECT * FROM CropQuantity";
        public static string getStorageType = "SELECT * FROM Storage";
        public static string getEmployeeID = "SELECT * FROM Employee";
        public static string getFieldNumber = "SELECT * FROM Field";
        public static string getUser = "SELECT * FROM dbo.Employee WHERE user_name = @username";
        public static string getCropStorage = "SELECT * FROM Storage WHERE storage_type = 'Crop' ";
        public static string updateCropStorage = "UPDATE Storage SET used_capacity = @usedCapacity WHERE storage_number = @storageNumber";
        public static string getTotalFertiliserRequired = "SELECT SUM(individual_total) as total, fertiliser_type FROM (SELECT (crop_quantity * amount_per_kilo) AS individual_total, fertiliser_type FROM (SELECT crop_quantity, amount_per_kilo, fertiliser_type FROM job JOIN RequiredFertiliser ON crop_name = assigned_crop WHERE job_type = 'Sow' AND start_date BETWEEN @startDate AND @endDate ) AS a) AS b GROUP BY fertiliser_type";

    }
}
