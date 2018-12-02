using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmControl
{
    public class Employee
    {
        public int EmployeeID { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int PrivilegeLevel { get; set; }

        public bool addCropStock(Storage storage, short amountToAdd)
        {
            if ((storage.UsedCapacity + amountToAdd) > (storage.MaxCapacity))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool removeCropStock(Storage storage, short amountToRemove)
        {
            if ((storage.UsedCapacity - amountToRemove) < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
