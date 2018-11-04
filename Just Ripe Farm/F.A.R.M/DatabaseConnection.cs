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
       private SqlConnection dBconnection;

        private string connectionStr;

        private SqlDataAdapter adapter;

        private DataSet dataSet;

        private DataTable dataTable;

        /// <summary>
        /// Constructs object that offers services to manipulate the chosen database.
        /// </summary>
        /// <param name="connectionStr">Contains the selected connection string</param>
        public DatabaseConnection(string connectionStr)
        {
            this.connectionStr = connectionStr;
        }

        /// <summary>
        /// Open the connection to the database.
        /// </summary>
        public void Open()
        {
            dBconnection = new SqlConnection(connectionStr);
            dBconnection.Open();
        }

        /// <summary>
        /// Close the connection to the database.
        /// </summary>
        public void Close()
        {
            dBconnection.Close();
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

        public DataTable GetUserDetail(string username)
        {
            dataTable = new DataTable();
            adapter = new SqlDataAdapter(SQLConstant.getUserDetails, dBconnection);
            adapter.SelectCommand.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
            adapter.Fill(dataTable);
            return dataTable;
        }

    }
}
