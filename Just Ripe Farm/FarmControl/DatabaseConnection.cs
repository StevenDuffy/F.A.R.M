using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace F.A.R.M
{
    public class DatabaseConnection
    {
        private readonly SqlConnection dBconnection;

        private SqlDataAdapter adapter;

        private DataSet dataSet;

        private DataTable dataTable;

        public SqlDataAdapter Adapter { get; set; } //?

        public DataSet DataSet { get; set; }

        public DataTable DataTable { get; set; }

        /// <summary>
        /// Constructs an object that offers services to manipulate the chosen database.
        /// </summary>
        /// <param name="connectionStr"> Contains the selected connection string. </param>
        public DatabaseConnection(string connectionStr)
        {
            this.dBconnection = new SqlConnection(connectionStr);
        }

        /// <summary>
        /// Open the connection to the database.
        /// </summary>
        public void Open()
        {
            this.dBconnection.Open();
        }

        /// <summary>
        /// Close the connection to the database.
        /// </summary>
        public void Close()
        {
            this.dBconnection.Close();
        }

        /// <summary>
        /// Get a single users username, password and privilege level for the login credentials.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public DataTable GetUserDetail(string username)
        {
            dataTable = new DataTable();
            adapter = new SqlDataAdapter(SQLConstant.getUserDetails, dBconnection);
            adapter.SelectCommand.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
            adapter.Fill(dataTable);
            return dataTable;
        }

        /// <summary>
        /// Read/get data from the database.
        /// </summary>
        /// <param name="readStatement"></param>
        /// <returns></returns>
        public DataSet GetData(string readStatement)
        {
            dataSet = new DataSet();
            adapter = new SqlDataAdapter(readStatement, dBconnection);
            adapter.Fill(dataSet);
            return dataSet;
        }

        /// <summary>
        /// Generic method to execute Update, Insert, Delete from the database.
        /// </summary>
        /// <param name="query"></param>
        public void NonQuery(string query)
        {
            SqlCommand command = new SqlCommand(query, dBconnection);
            command.ExecuteNonQuery();
        }



    }
}
