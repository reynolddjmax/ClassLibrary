using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace DLL
{
    public class SQLClass
    {
        static string oUser = "";
        static string oPassword = "";

        static string oServer = "";

        public static void SetConn(string Server, string User, string Password)
        {
            oUser = User;
            oPassword = Password;
            oServer = Server;
        }

        public static void ExecuteReader(string Database ,string Sql)
        {
            SqlConnection con = new SqlConnection();

            // con.ConnectionString = "server=505-03;database=ttt;user=sa;pwd=123";
            con.ConnectionString = "server=" + oServer + ";database=" + Database + ";uid=" + oUser + ";pwd=" + oPassword;
            con.Open();

            /*
            SqlDataAdapter 对象。 用于填充DataSet （数据集）。
            SqlDataReader 对象。 从数据库中读取流..
            后面要做增删改查还需要用到 DataSet 对象。
            */

            SqlCommand com = new SqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = Sql;
            SqlDataReader dr = com.ExecuteReader();//执行SQL语句
            dr.Close();//关闭执行
            con.Close();//关闭数据库
        }

        /// <summary>
        /// 取所有数据库名,添加到lvDB
        /// </summary>
        /// <returns></returns>
        public static ArrayList GetAllDataBase()
        {
            ArrayList DBNameList = new ArrayList();
            SqlConnection Connection = new SqlConnection(
                String.Format("Data Source={0};Initial Catalog=master;User ID={1};PWD={2}", oServer, oUser, oPassword));
            DataTable DBNameTable = new DataTable();
            SqlDataAdapter Adapter = new SqlDataAdapter("select name from master..sysdatabases", Connection);

            lock (Adapter)
            {
                Adapter.Fill(DBNameTable);
            }

            foreach (DataRow row in DBNameTable.Rows)
            {
                DBNameList.Add(row["name"]);
            }

            return DBNameList;
        }

        public static DataTable GetDataTable(string Database ,string sql)
        {

            SqlConnection Connection = new SqlConnection("Data Source="+oServer+";Initial Catalog=" + Database + ";User ID="+oUser+";PWD="+oPassword);
            DataTable DBTable = new DataTable();
            SqlDataAdapter Adapter = new SqlDataAdapter(sql, Connection);

            lock (Adapter)
            {
                Adapter.Fill(DBTable);
            }

            Connection.Close();
            


            return DBTable;

        }

        public static string GetString(string Database, string sql)
        {

            SqlConnection Connection = new SqlConnection("Data Source=" + oServer + ";Initial Catalog=" + Database + ";User ID=" + oUser + ";PWD=" + oPassword);
            DataTable DBTable = new DataTable();
            SqlDataAdapter Adapter = new SqlDataAdapter(sql, Connection);

            lock (Adapter)
            {
                Adapter.Fill(DBTable);
            }

            Connection.Close();

            if (DBTable.Rows.Count !=0)
            {
                return DBTable.Rows[0][0].ToString();
            }

            return "";

        }

    }
}
