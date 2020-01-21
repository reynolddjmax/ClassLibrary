using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace DLL
{
    public class AccessOpen
    {
        //测试
        OleDbConnection conn;
        public AccessOpen(string DataPath)
        {
            //"Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
            //"Provider=Microsoft.Ace.OleDb.12.0;Data Source="
            conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DataPath);
             conn.Open();
        }

        public void AccessClose()
        {
            conn.Close();
        }


        public DataTable ReadData(string Sql)
        {


            OleDbCommand comm = new OleDbCommand(Sql, conn);
            OleDbDataReader dr = comm.ExecuteReader();
            DataTable dt = new DataTable();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                DataColumn dc = new DataColumn(dr.GetName(i), dr.GetFieldType(i));
                dt.Columns.Add(dc);
            }

            while (dr.Read())
            {
                DataRow ddr = dt.NewRow();
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    ddr[i] = dr.GetValue(i).ToString();
                }
                dt.Rows.Add(ddr);
            }

            dr.Close();
            return dt;
        }

        public String ReadDataS(string Sql)
        {
            OleDbCommand comm = new OleDbCommand(Sql, conn);
            OleDbDataReader dr = comm.ExecuteReader();
            DataTable dt = new DataTable();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                DataColumn dc = new DataColumn(dr.GetName(i), dr.GetFieldType(i));
                dt.Columns.Add(dc);
            }

            dr.Read();

            string Vaule = dr.GetValue(0).ToString();

            dr.Close();
            return Vaule;
        }


        public void WriteData(string Sql)
        {
            OleDbCommand comm = new OleDbCommand(Sql, conn);
            comm.ExecuteNonQuery();
        }


        public bool ExistData(string Sql)
        {

            OleDbCommand comm = new OleDbCommand(Sql, conn);

            OleDbDataReader dr = comm.ExecuteReader();

            bool result = dr.HasRows;

            dr.Close();

            return result;
        }


    }




    public class Access
    {

        #region 通用方法
        public static DataTable ReadDataTable(string Sql ,string DataPath)
        {
            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0 ;Data Source=" + DataPath);
            conn.Open();

            OleDbCommand comm = new OleDbCommand(Sql, conn);
            OleDbDataReader dr = comm.ExecuteReader();
            DataTable dt = new DataTable();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                DataColumn dc = new DataColumn(dr.GetName(i), dr.GetFieldType(i));
                dt.Columns.Add(dc);
            }

            while (dr.Read())
            {
                DataRow ddr = dt.NewRow();
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    ddr[i] = dr.GetValue(i).ToString();
                }
                dt.Rows.Add(ddr);
            }

            dr.Close();
            conn.Close();
            return dt;
        }

        public static string ReadString(string Sql, string DataPath)
        {
            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0 ;Data Source=" + DataPath);
            conn.Open();

            OleDbCommand comm = new OleDbCommand(Sql, conn);
            OleDbDataReader dr = comm.ExecuteReader();
            DataTable dt = new DataTable();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                DataColumn dc = new DataColumn(dr.GetName(i), dr.GetFieldType(i));
                dt.Columns.Add(dc);
            }

            dr.Read();

            string Vaule = dr.GetValue(0).ToString();

            dr.Close();
            conn.Close();
            return Vaule;
        }

        public static void Execute(string Sql, string DataPath)
        {
            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0 ;Data Source=" + DataPath);
            conn.Open();

            OleDbCommand comm = new OleDbCommand(Sql, conn);
            comm.ExecuteNonQuery();

            conn.Close();
        }

        public static bool CheckExist(string Sql, string DataPath)
        {
            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0 ;Data Source=" + DataPath);
            conn.Open();

            OleDbCommand comm = new OleDbCommand(Sql, conn);
            
            OleDbDataReader dr = comm.ExecuteReader();

            conn.Close();
            dr.Close();

            return dr.HasRows;
        }
        #endregion


        #region 拓展方法

        //插入列
        //递增列名用Index，自动排除Index递增列
        public static void InsertRow(string DataPath ,string TableName, params object[] objects)
        {
            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0 ;Data Source=" + DataPath);
            conn.Open();

            string ColLst = "";
            string vaules = "";

            using (OleDbCommand cmd = new OleDbCommand())
            {
                cmd.CommandText = "SELECT TOP 1 * FROM [" + TableName + "]";
                cmd.Connection = conn;
                OleDbDataReader dr = cmd.ExecuteReader();
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    if (dr.GetName(i).ToString().ToLower() == "index") continue;
                    ColLst += ",[" + dr.GetName(i) + "]";
                }

                dr.Close();
            }

            ColLst = ColLst.Substring(1);

            foreach (var item in objects)
            {
                vaules += ",'" + item.ToString() + "'";
            }
            vaules = vaules.Substring(1);


            string Sql = "insert into ["+TableName+"]("+ColLst+") values("+vaules+")";

            using (OleDbCommand cmd = new OleDbCommand(Sql, conn))
            {
                cmd.ExecuteReader();
            }

            
            conn.Close();
        }

        #endregion
    }






}
