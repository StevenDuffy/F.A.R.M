using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows;
using FarmControl;
using System.Security.Cryptography;

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
        public bool VerifyUser(string username, string password)
        {

            Employee user = DatabaseConnection.DataConn.GetUser(username);
            CurrentSession.SetCurrentSession(user);

            if (user.Password == null)
            {
                return false;
            }
            else if (username != user.Username)
            {
                return false;
            }
            else if (GetPasswordHashValue(password) != user.Password)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public string GetPasswordHashValue(string plainPassword)
        {
            byte[] password = Encoding.UTF8.GetBytes(plainPassword);
            SHA256Managed sha = new SHA256Managed();
            byte[] hashedPasword = sha.ComputeHash(password);
            sha.Dispose();
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < hashedPasword.Length; i++)
            {
                stringBuilder.Append(hashedPasword[i].ToString("x2"));
            }

            return stringBuilder.ToString();

        }
    }
}
