using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace FarmControl
{
    public static class CurrentSession
    {
        //Trying to limit access to this class.

        private static Employee currentUser;

        public static Employee CurrentUser
        {
            get
            {
                return currentUser;
            }
            set
            {
                currentUser = value;
            }
        }

        public static void SetCurrentSession(Employee currentUser)
        {
            CurrentUser = currentUser;
        }
    }
}
