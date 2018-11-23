using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows;
using FarmControl;

namespace FarmControl
{


    public class LoginVerifier
    {
        /// <summary>
        /// Stores object that offers verification methods.
        /// </summary>
        private static LoginVerifier verifier;


        public static LoginVerifier Verifier
        {
            get
            {
                if (verifier == null)
                {
                    verifier = new LoginVerifier();
                }
                return verifier;
            }            
        }

        /// <summary>
        /// Checks username and password against database to verify user and privilege level.
        /// </summary>
        /// <param name="username">User entered Username</param>
        /// <param name="password">User entered Password</param>
        /// <param name="session">session instance to be used for the login session</param>
        /// <returns>True or false and a Session</returns>
        public bool VerifyUser(string username, string password, out Employee user)
        {
            DataTable data = new DataTable();

            user = null;

            data = DatabaseConnection.DataConn.GetUserDetail(username);

            if (data.Rows.Count == 0)
            {
                return false;
            }
            else if (password != data.Rows[0]["password"].ToString())
            {
                return false;
            }
            else
            {
                switch (Convert.ToInt32(data.Rows[0]["privilege_Level"].ToString()))
                {
                    case 1:
                        user = new Manager() { Privilege_Level = 1 };
                        break;
                    default:
                        user = new Employee() { Privilege_Level = 0 };
                        break;
                }

                return true;
            }
        }

    }
}
