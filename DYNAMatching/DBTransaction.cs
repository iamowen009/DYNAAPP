using System;
using System.Data;
using System.Data.SqlClient;

namespace DYNAMatching
{
    class DBTransaction : DBHelper
    {
        public DataTable GetUserInfoLogin(string UserName, string Password)
        {
            DataSet ds = new DataSet();
            using (SqlDataAdapter adapter = new SqlDataAdapter())
            {
                OpenConnection();
                ds = new DataSet();
                ds.Tables.Add("DT");
                SqlComm.CommandType = CommandType.StoredProcedure;
                SqlComm.CommandText = "spGetUserLogin";
                SqlComm.Parameters.Clear();
                SqlComm.Parameters.Add(new SqlParameter("@User_ID", SqlDbType.VarChar));
                SqlComm.Parameters.Add(new SqlParameter("@Password", SqlDbType.VarChar));
                SqlComm.Parameters["@User_ID"].Value = UserName;
                SqlComm.Parameters["@Password"].Value = Password;
                SqlComm.CommandTimeout = 0;
                adapter.SelectCommand = SqlComm;
                adapter.Fill(ds, ds.Tables["DT"].TableName);
                CloseConnection();
            }
            return ds.Tables[0];
        }

        public DataTable GetExcuteDataTable(string SPName)
        {
            DataSet ds = new DataSet();
            using (SqlDataAdapter adapter = new SqlDataAdapter())
            {
                OpenConnection();
                ds = new DataSet();
                ds.Tables.Add("DT");
                SqlComm.CommandType = CommandType.StoredProcedure;
                SqlComm.CommandText = SPName ;
                SqlComm.Parameters.Clear();
                SqlComm.CommandTimeout = 0;
                adapter.SelectCommand = SqlComm;
                adapter.Fill(ds, ds.Tables["DT"].TableName);
                CloseConnection();
            }
            return ds.Tables[0];
        }

        public DataTable GetExcuteDataTableOneCon(string SPName,string Condition)
        {
            DataSet ds = new DataSet();
            using (SqlDataAdapter adapter = new SqlDataAdapter())
            {
                OpenConnection();
                ds = new DataSet();
                ds.Tables.Add("DT");
                SqlComm.CommandType = CommandType.StoredProcedure;
                SqlComm.CommandText = SPName;
                SqlComm.Parameters.Clear();
                SqlComm.Parameters.Add(new SqlParameter("@Condition", SqlDbType.VarChar));
                SqlComm.Parameters["@Condition"].Value = Condition;
                SqlComm.CommandTimeout = 0;
                adapter.SelectCommand = SqlComm;
                adapter.Fill(ds, ds.Tables["DT"].TableName);
                CloseConnection();

            }
            return ds.Tables[0];
        }

        public bool InsertTScanLabel(string @Customer,string @Part_Id,string @Part_No,string @Part_Name, Decimal @Pack_Qty,string @Rank_No,string @Issue_No,string @Serial_No,string @Order_No,string @CreateBy,string @UNIQUEID)
        {
            bool success = true;
            int rownum = 0;
            try
            {
                BeginTransaction();

                SqlComm.CommandType = CommandType.StoredProcedure;
                SqlComm.CommandText = "spInsTScanLabel";
                SqlComm.Parameters.Clear();

                SqlComm.Parameters.Add(new SqlParameter("@Customer", SqlDbType.VarChar));
                SqlComm.Parameters.Add(new SqlParameter("@Part_Id", SqlDbType.VarChar));
                SqlComm.Parameters.Add(new SqlParameter("@Part_No", SqlDbType.VarChar));
                SqlComm.Parameters.Add(new SqlParameter("@Part_Name", SqlDbType.VarChar));
                SqlComm.Parameters.Add(new SqlParameter("@Pack_Qty", SqlDbType.Decimal));
                SqlComm.Parameters.Add(new SqlParameter("@Rank_No", SqlDbType.VarChar));
                SqlComm.Parameters.Add(new SqlParameter("@Issue_No", SqlDbType.VarChar));
                SqlComm.Parameters.Add(new SqlParameter("@Serial_No", SqlDbType.VarChar));
                SqlComm.Parameters.Add(new SqlParameter("@Order_No", SqlDbType.VarChar));
                SqlComm.Parameters.Add(new SqlParameter("@CreateBy", SqlDbType.VarChar));
                SqlComm.Parameters.Add(new SqlParameter("@UNIQUEID", SqlDbType.VarChar));

                SqlComm.Parameters["@Customer"].Value = @Customer;
                SqlComm.Parameters["@Part_Id"].Value = @Part_Id;
                SqlComm.Parameters["@Part_No"].Value = @Part_No;
                SqlComm.Parameters["@Part_Name"].Value = @Part_Name;
                SqlComm.Parameters["@Pack_Qty"].Value = @Pack_Qty;
                SqlComm.Parameters["@Rank_No"].Value = @Rank_No;
                SqlComm.Parameters["@Issue_No"].Value = @Issue_No;
                SqlComm.Parameters["@Serial_No"].Value = @Serial_No;
                SqlComm.Parameters["@Order_No"].Value = @Order_No;
                SqlComm.Parameters["@CreateBy"].Value = @CreateBy;
                SqlComm.Parameters["@UNIQUEID"].Value = @UNIQUEID;

                SqlComm.CommandTimeout = 0;
                rownum = SqlComm.ExecuteNonQuery();
                success = (rownum > 0) ? true : false;

                if (success)
                    CommitTransaction();
                else
                    RollbackTrasaction();

            }
            catch (Exception ex)
            {
                success = false;
                RollbackTrasaction();
            }
            finally
            {
                CloseConnection();
            }
            return success;
        }

    }
}
