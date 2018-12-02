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
        public static string getUserList = "SELECT employee_ID, first_Name, second_Name, user_Name, privilege_Level FROM dbo.Employee";
        public static string getPlannedHarvests = "SELECT * FROM Job WHERE start_date BETWEEN @startDate AND @endDate AND status = 'PENDING'";
        public static string getCrops = "SELECT * FROM Crop";
        public static string getCropQuantity = "SELECT * FROM CropQuantity";
        public static string getStorageType = "SELECT * FROM Storage";
        public static string getEmployeeID = "SELECT * FROM Employee";
        public static string getFieldNumber = "SELECT * FROM Field";
        public static string getUser = "SELECT * FROM dbo.Employee WHERE user_name = @username";
        public static string getCropStorage = "SELECT * FROM Storage WHERE storage_type = 'Crop' ";
        public static string updateCropStorage = "UPDATE Storage SET used_capacity = @usedCapacity WHERE storage_number = @storageNumber";

    }
}
