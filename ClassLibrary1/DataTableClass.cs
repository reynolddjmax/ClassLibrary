using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace DLL
{
    public class DataTableClass
    {
        //DataGridView转换为DataTable
        public static DataTable GetDgvToTable(System.Windows.Forms.DataGridView dgv)
        {
            DataTable dt = new DataTable();


            // 列强制转换
            for (int count = 0; count < dgv.Columns.Count; count++)
            {
                DataColumn dc = new DataColumn(dgv.Columns[count].Name.ToString());
                dt.Columns.Add(dc);
            }

            // 循环行
            for (int count = 0; count < dgv.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow();
                for (int countsub = 0; countsub < dgv.Columns.Count; countsub++)
                {
                    dr[countsub] = Convert.ToString(dgv.Rows[count].Cells[countsub].Value);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public static void DtAddRow(DataTable dt, params object[] Vaules)
        {
            DataRow dr = dt.NewRow();
            for (int i = 0; i < Vaules.Length; i++)
            {
                dr[i] = Vaules[i];
            }

            dt.Rows.Add(dr);
        }


        public static string DtSelVaule(DataTable dt, string Sql , string VauleColumn)
        {
            DataRow[] dr = dt.Select(Sql);

            if (dr.Length == 0)
            {
                return "";
            }

            return dr[0][VauleColumn].ToString();

        }


        public static DataTable DtSelect(DataTable dt, string Sql)
        {
            DataTable aa = dt.Clone();


            DataRow[] drAry = dt.Select(Sql);

            foreach (DataRow item in drAry)
            {
                DataRow dr = aa.NewRow();

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dr[i] = item[i];
                }

                aa.Rows.Add(dr);
            }

            return aa;
        }

        //CSV文件转换为Datatable
        public static DataTable CSVToDataTable(string strpath)
        {
            int intColCount = 0;
            bool blnFlag = true;
            DataTable mydt = new DataTable();

            DataColumn mydc;
            DataRow mydr;

            string strline;
            string [] aryline;

            System.IO.StreamReader mysr = new System.IO.StreamReader(strpath , Encoding.Default);

            while((strline = mysr.ReadLine()) != null)
            {
                aryline = strline.Split(',');

                if (blnFlag)
                {
                    blnFlag = false;
                    intColCount = aryline.Length;
                    for (int i = 0; i < aryline.Length; i++ )
                    {
                        mydc = new DataColumn(aryline[i]);
                        mydt.Columns.Add(mydc);
                    }
                }
                else
                {
                    mydr = mydt.NewRow();
                    for (int i = 0; i < intColCount; i++)
                    {
                        mydr[i] = aryline[i];
                    }
                    mydt.Rows.Add(mydr);
                }


            }


            mysr.Dispose();
            return mydt;


        }

    }
}
