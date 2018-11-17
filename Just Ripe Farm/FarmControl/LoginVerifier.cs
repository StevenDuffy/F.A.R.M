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
        /// Stores connection to Farm database.
        /// </summary>
        private readonly DatabaseConnection connectionToDB;

        public LoginVerifier()
        {
            // Create a new object to read/manipulate data from the database.
            connectionToDB = new DatabaseConnection();
        }

        /// <summary>
        /// Checks username and password against database to verify user and privilege level.
        /// </summary>
        /// <param name="username">User entered Username</param>
        /// <param name="password">User entered Password</param>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool VerifyUser(string username, string password, out Session session)
        {
            DataTable data = new DataTable();

            session = null;

            data = connectionToDB.GetUserDetail(username);

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
                        session = new Session() { Privilege_Level = 1 };
                        break;
                    default:
                        session = new Session() { Privilege_Level = 0 };
                        break;
                }

                return true;
            }
        }

    }
}
