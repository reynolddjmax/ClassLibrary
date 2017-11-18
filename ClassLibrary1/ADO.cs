using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace DLL
{

    public class ADO
    {

        //IMEX=1 把混合型数据作为文本读取


        //select DISTINCT 承包方编码 From [地块信息$] order by 承包方编码
        //Select 宗地编码,土地类型,承包方名称,承包方编码 From [地块信息$] Order By 承包方编码,宗地编码
        public static DataTable LoadExcel(string FilePath, string Sql)
        {

            ADODB.Connection conn = new ADODB.Connection();
            ADODB.Recordset rs = new ADODB.Recordset();

            try
            {

                //FilePath = "C:\\Users\\Administrator\\Desktop\\测试模版\\Shp\\1\\凤凰观村2组.xls";
                //Sql = "Select 宗地编码,土地类型,承包方名称,承包方编码 From [地块信息$] Order By 承包方编码,宗地编码";

                //Select 宗地编码,土地类型,承包方名称,承包方编码 From [地块信息$] Order By 承包方编码,宗地编码
                conn.Open("Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties='Excel 8.0;IMEX=1';Data Source=" + FilePath);
                rs.Open(Sql, conn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockReadOnly, -1);
                object ary = rs.GetRows();
                object[,] aa = (object[,])ary;

                int UB1 = aa.GetUpperBound(1);
                int UB0 = aa.GetUpperBound(0);


                DataTable dt = new DataTable();



                //读取SQL判断表名
                //string Str1 = Sql.ToLower().Substring(Sql.IndexOf("select") + 6, Sql.IndexOf("from") - Sql.IndexOf("select") - 6);

                //Str1 = Str1.Trim();
                //string[] NameAry = Str1.Split(',');


                //foreach (var item in NameAry)
                //{
                //    dt.Columns.Add(item);
                //}

                for (int i = 0; i <= UB0; i++)
                {
                    dt.Columns.Add(i.ToString());
                }



                for (int i = 0; i <= UB1; i++)
                {
                    DataRow dr = dt.NewRow();

                    for (int j  = 0; j  <= UB0; j ++)
                    {

                        if (Convert.IsDBNull(aa[j, i]))
                        {
                            dr[j] = "";
                        }
                        else
                        {
                            dr[j] = aa[j, i].ToString();
                        }

                    }

                    dt.Rows.Add(dr);
                }

                rs.Close();
                conn.Close();


                return dt;

            }
            catch (Exception)
            {
                rs.Close();
                conn.Close();
                return null;
            }
        }


        public static DataSet ExcelToDS(string Path, string sql)
        {
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + Path + ";" + "Extended Properties=Excel 8.0;";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            OleDbDataAdapter myCommand = null;
            DataSet ds = null;
            myCommand = new OleDbDataAdapter(sql, strConn);
            ds = new DataSet();
            myCommand.Fill(ds, "table1");
            return ds;
        } 




        public static object[,] LoadExcelAry(string FilePath, string Sql)
        {
            ADODB.Connection conn = new ADODB.Connection();
            ADODB.Recordset rs = new ADODB.Recordset();

            try
            {

                //FilePath = "C:\\Users\\Administrator\\Desktop\\测试模版\\Shp\\1\\凤凰观村2组.xls";
                //Sql = "Select 宗地编码,土地类型,承包方名称,承包方编码 From [地块信息$] Order By 承包方编码,宗地编码";

                //Select 宗地编码,土地类型,承包方名称,承包方编码 From [地块信息$] Order By 承包方编码,宗地编码
                //Select * From [发包方信息$A1:BB2000]
                conn.Open("Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Excel 8.0;Data Source=" + FilePath);
                rs.Open(Sql, conn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockReadOnly, -1);
                object ary = rs.GetRows();
                object[,] aa = (object[,])ary;

                rs.Close();
                conn.Close();



                int UB1 = aa.GetUpperBound(1);
                int UB0 = aa.GetUpperBound(0);

                for (int i = 0; i <= UB1; i++)
                {
                    for (int j = 0; j <= UB0; j++)
                    {
                        ErrorMsg = (i + "-" + j);
                        if (Convert.IsDBNull(aa[j, i]) || aa[j, i] == null)
                        {
                            aa[j, i] = "";
                        }
                    }
                }


                //object test0 = aa[1, 3];
                //object test1 = aa[15, 3];
                //object test2 = aa[17, 3];
                return aa;

            }
            catch (Exception)
            {
                
                rs.Close();
                conn.Close();
                return null;
            }
        }


        //可读取xlsx格式  xls兼容性未测试
        public static object[,] LoadExcelAry2(string FilePath, string Sql)
        {
            ADODB.Connection conn = new ADODB.Connection();
            ADODB.Recordset rs = new ADODB.Recordset();

            try
            {

                //FilePath = "C:\\Users\\Administrator\\Desktop\\测试模版\\Shp\\1\\凤凰观村2组.xls";
                //Sql = "Select 宗地编码,土地类型,承包方名称,承包方编码 From [地块信息$] Order By 承包方编码,宗地编码";

                //Select 宗地编码,土地类型,承包方名称,承包方编码 From [地块信息$] Order By 承包方编码,宗地编码
                conn.Open("provider=Microsoft.ACE.OLEDB.12.0;extended properties=excel 12.0;;Data Source=" + FilePath);
                rs.Open(Sql, conn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockReadOnly, -1);
                object ary = rs.GetRows();
                object[,] aa = (object[,])ary;

                rs.Close();
                conn.Close();



                int UB1 = aa.GetUpperBound(1);
                int UB0 = aa.GetUpperBound(0);

                for (int i = 0; i <= UB1; i++)
                {
                    for (int j = 0; j <= UB0; j++)
                    {
                        ErrorMsg = (i + "-" + j);
                        if (Convert.IsDBNull(aa[j, i]) || aa[j, i] == null)
                        {
                            aa[j, i] = "";
                        }
                    }
                }


                //object test0 = aa[1, 3];
                //object test1 = aa[15, 3];
                //object test2 = aa[17, 3];
                return aa;

            }
            catch (Exception)
            {

                rs.Close();
                conn.Close();
                return null;
            }
        }

        public static string ErrorMsg;
        public static DataTable LoadDBF(string FilePath, string Sql)
        {

            ADODB.Recordset rs = new ADODB.Recordset();
            ADODB.Connection conn = new ADODB.Connection();
            try
            {


                conn.Open("Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties='dbase 5.0';Data Source = " + System.IO.Path.GetDirectoryName(FilePath));


                rs.Open(Sql, conn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockReadOnly);
                object ary = rs.GetRows();
                object[,] aa = (object[,])ary;

                int UB1 = aa.GetUpperBound(1);
                int UB0 = aa.GetUpperBound(0);


                DataTable dt = new DataTable();

                for (int i = 0; i <= UB0; i++)
                {
                    dt.Columns.Add(i.ToString());
                }



                for (int i = 0; i <= UB1; i++)
                {
                    DataRow dr = dt.NewRow();

                    for (int j = 0; j <= UB0; j++)
                    {
                        dr[j] = aa[j, i].ToString();
                    }

                    dt.Rows.Add(dr);
                }

                rs.Close();
                conn.Close();


                return dt;
            }
            catch (Exception e)
            {
                rs.Close();
                conn.Close();
                return null;

            }

        }



        public static string ReadExcel(string FilePath , int C, int R)
        {
            ADODB.Connection conn = new ADODB.Connection();
            ADODB.Recordset rs = new ADODB.Recordset();

            string Sql = "Select * From [Sheet1$]";

            //Select 宗地编码,土地类型,承包方名称,承包方编码 From [地块信息$] Order By 承包方编码,宗地编码
            conn.Open("Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Excel 8.0;Data Source=" + FilePath);
            rs.Open(Sql, conn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockReadOnly, -1);
            object ary = rs.GetRows();
            object[,] aa = (object[,])ary;

            rs.Close();
            conn.Close();



            if ((string)aa[C-1, R - 2] == null)
            {
                return "";
            }


            return (string)aa[C-1, R- 2];
        }
    }
}
