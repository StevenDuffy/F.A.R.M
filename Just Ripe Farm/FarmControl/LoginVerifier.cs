using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows;

namespace F.A.R.M
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


        public bool VerifyUser(string username, string password, out Employee user)
        {
            DataTable data = new DataTable();

            user = null;

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
                        user = new Manager();
                        break;
                    default:
                        user = new Employee();
                        break;
                }

                return true;

            }
        }

    }
}
