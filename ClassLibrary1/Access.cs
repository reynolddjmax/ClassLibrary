using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace DLL
{
    //测试
    public class AccessOpen
    {
        OleDbConnection conn;
        public AccessOpen(string DataPath)
        {
            //"Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
            conn = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=" + DataPath);
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


            return Vaule;
        }


        public void WriteData(string Sql)
        {
            OleDbCommand comm = new OleDbCommand(Sql, conn);
            comm.ExecuteNonQuery();
        }





    }




    public class Access
    {

        public static DataTable ReadData(string Sql ,string DataPath)
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
            conn.Close();
            return dt;
        }

        public static string ReadDataS(string Sql, string DataPath)
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

            conn.Close();
            return Vaule;
        }


        public static void WriteData(string Sql, string DataPath)
        {
            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0 ;Data Source=" + DataPath);
            conn.Open();

            OleDbCommand comm = new OleDbCommand(Sql, conn);
            comm.ExecuteNonQuery();

            conn.Close();
        }
    }






}
