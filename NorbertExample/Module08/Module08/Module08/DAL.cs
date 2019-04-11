using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Module08
{
    public class DAL
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader rdr;

        string connectionString = "Data Source=ServerName;Initial Catalog=FileWatcherDetails;Persist Security Info=True;User ID=sa;Password=Conestoga1;Pooling=False";

        public DAL()
        {
            conn = new SqlConnection(connectionString);
            cmd = new SqlCommand();
            cmd.Connection = conn;
        }

        public void AddFileData(FileDataEntity fde)
        {
            string sqlCmd = "INSERT INTO Data (Action, PathName, OldPathName, TimeAffected) "
                          + "VALUES ('"
                          + fde.Action + "', '"
                          + fde.PathName + "', '"
                          + fde.OldPathName + "', '"
                          + fde.TimeAffected.ToString() + "');";
            try
            {
                cmd.CommandText = sqlCmd;
                conn.Open();
                int result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.Log("Exception: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public List<FileDataEntity> GetFDEList()
        {
            string sqlCmd = "SELECT Action, PathName, OldPathName, TimeAffected FROM Data ORDER BY TimeAffected DESC;";
            List<FileDataEntity> fdeList = new List<FileDataEntity>();

            try
            {
                cmd.CommandText = sqlCmd;
                conn.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    FileDataEntity fde = new FileDataEntity();
                    fde.Action = rdr["Action"].ToString();
                    fde.PathName = rdr["PathName"].ToString();
                    fde.OldPathName = rdr["OldPathName"].ToString();
                    fde.TimeAffected = (DateTime)rdr["TimeAffected"];
                    fdeList.Add(fde);
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Exception: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return fdeList;
        }


    }
}
