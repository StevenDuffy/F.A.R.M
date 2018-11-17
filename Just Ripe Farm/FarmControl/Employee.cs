using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmControl
{
    public class Employee
    {
        private int privilege_Level;

        public int Privilege_Level
        {
            get
            {
                return privilege_Level;
            }
            set
            {
                privilege_Level = value;
            }
        }
    }
}
