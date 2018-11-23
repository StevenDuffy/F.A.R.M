using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using FarmControl.Properties;

namespace FarmControl
{
    public class DatabaseConnection
    {
        private static DatabaseConnection dataConn;

        /// <summary>
        /// String used for the connection to the database.
        /// </summary>
        private readonly string connectionString;

        private readonly SqlConnection dBconnection;

        private SqlDataAdapter adapter;

        //  private DataSet dataSet;

        private DataTable dataTable;

        private SqlDataAdapter Adapter { get { return adapter; } set { adapter = value; } }

        //  private DataSet DataSet { get { return dataSet; } set { dataSet = value; } }

        private DataTable DataTable { get { return dataTable; } set { dataTable = value; } }

        public static DatabaseConnection DataConn
        {
            get
            {
                if (dataConn == null)
                {
                    dataConn = new DatabaseConnection();
                }

                return dataConn;
            }
        }

        /// <summary>
        /// Constructs an object that offers services to manipulate the chosen database.
        /// </summary>
        /// <param name="connectionStr"> Contains the selected connection string.</param>
        private DatabaseConnection()
        {
            connectionString = FarmControl.Properties.Settings.Default.FarmDBConnStr;  //System.Configuration.ConfigurationManager.ConnectionStrings["FarmControl.Properties.Settings.FarmDBConnStr"].ToString();

            this.dBconnection = new SqlConnection(connectionString);
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
            DataTable = new DataTable();
            Adapter = new SqlDataAdapter(SQLConstant.getUserDetails, dBconnection);
            Adapter.SelectCommand.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
            Adapter.Fill(DataTable);
            return DataTable;
        }

        public DataTable GetUserList()
        {
            DataTable userList = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(SQLConstant.getUserList, dBconnection);
            adapter.Fill(userList);
            return userList;

        }

        public DataTable GetUpcomingHarvests(string startDate, string endDate)
        {
            DataTable = new DataTable();
            Adapter = new SqlDataAdapter(SQLConstant.getUpcomingHarvests, dBconnection);
            Adapter.SelectCommand.Parameters.Add("@startDate", SqlDbType.VarChar).Value = startDate;
            Adapter.SelectCommand.Parameters.Add("@endDate", SqlDbType.VarChar).Value = endDate;
            Adapter.Fill(DataTable);
            return DataTable;
        }
        public DataTable GetPlannedHarvests(string startDate, string endDate)
        {
            DataTable = new DataTable();
            Adapter = new SqlDataAdapter(SQLConstant.getPlannedHarvests, dBconnection);
            Adapter.SelectCommand.Parameters.Add("@startDate", SqlDbType.VarChar).Value = startDate;
            Adapter.SelectCommand.Parameters.Add("@endDate", SqlDbType.VarChar).Value = endDate;
            Adapter.Fill(DataTable);
            return DataTable;
        }

        /*
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
        } */
    }
}
