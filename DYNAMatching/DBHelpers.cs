using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DYNAMatching
{

    public class ConnectionStringInfo
    {
        private string servername = string.Empty;
        private string databasename = string.Empty;
        private string userid = string.Empty;
        private string password = string.Empty;

        public string ServerName
        {
            get { return servername; }
            set { servername = value; }
        }

        public string DatabaseName
        {
            get { return databasename; }
            set { databasename = value; }
        }

        public string UserID
        {
            get { return userid; }
            set { userid = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
    }

    /// <summary>
    /// Class Connection And Manage SqlClient
    /// </summary>
    public class DBHelper
    {
        #region Member      
        private SqlConnection sqlConn = null;
        private SqlCommand sqlComm = null;
        private SqlTransaction sqlTrans = null;
        private int timeOut = 3600;
        #endregion

        #region Properties
        /// <summary>
        /// Connection String
        /// </summary>
        public string ConnectionString
        {
            get
            {
                
                return ConfigurationManager.ConnectionStrings["DBDYNA"].ConnectionString;
            }
        }

        /// <summary>
        /// SQL Connection
        /// </summary>
        public SqlConnection Connection
        {
            get
            {
                string sqlConn = ConfigurationManager.ConnectionStrings["DBDYNA"].ConnectionString;

                if (string.IsNullOrEmpty(sqlConn))
                    throw new Exception("Invalid Connection");
                return (new SqlConnection(sqlConn));
            }
        }

        public SqlCommand SqlComm
        {
            get { return sqlComm; }
            set { sqlComm = value; }
        }

        #endregion

        #region Connection and Transaction Management
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connection"></param>
        public DBHelper()
        {
            // new Connection
            sqlConn = Connection;
            if (sqlConn.State == ConnectionState.Open)
                sqlConn.Close();

            sqlConn.Open();

            if (sqlComm == null)
                sqlComm = sqlConn.CreateCommand();

            // begin transaction
            sqlComm.Connection = sqlConn;
            sqlComm.CommandType = CommandType.Text;
            sqlComm.CommandTimeout = timeOut;
        }

        /// <summary>
        /// Begin Transaction
        /// </summary>
        public void BeginTransaction()
        {
            OpenConnection();
            if (this.sqlConn == null)
                throw new Exception("sqlConnection is null");
            if (this.sqlComm == null)
                throw new Exception("sqlCommand is null");
            sqlTrans = sqlConn.BeginTransaction();
            sqlComm.Transaction = sqlTrans;
        }

        /// <summary>
        /// Commit Transaction
        /// </summary>
        public void CommitTransaction()
        {
            if (this.sqlTrans != null)
            {
                this.sqlTrans.Commit();
                this.sqlTrans.Dispose();
            }
            CloseConnection();
        }

        /// <summary>
        /// Rollback Transaction
        /// </summary>
        public void RollbackTrasaction()
        {
            if (this.sqlTrans != null)
            {
                this.sqlTrans.Rollback();
                this.sqlTrans.Dispose();
            }
            CloseConnection();
        }

        /// <summary>
        /// Close Connection
        /// </summary>
        public void CloseConnection()
        {
            if (this.sqlConn != null)
            {
                this.sqlComm.Dispose();
                this.sqlConn.Close();
                this.sqlConn.Dispose();
            }
        }

        /// <summary>
        /// Open Connection
        /// </summary>
        public void OpenConnection()
        {
            sqlConn = Connection;
            if (sqlConn.State == ConnectionState.Open)
                sqlConn.Close();

            sqlConn.Open();

            if (sqlComm == null)
                sqlComm = sqlConn.CreateCommand();

            // begin transaction
            sqlComm.Connection = sqlConn;
            sqlComm.CommandType = CommandType.Text;
            sqlComm.CommandTimeout = timeOut;
        }
        #endregion

    }
}