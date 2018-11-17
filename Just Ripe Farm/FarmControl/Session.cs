using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace FarmControl
{
    public class Session
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
