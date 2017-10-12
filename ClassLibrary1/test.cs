using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DLL
{
    public class test
    {
        public static void testX()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("index");
            dt.Columns.Add("x");
            dt.Columns.Add("y");



            /////
            for (int i = 0; i < 10000000; i++)
            {



                dt.Rows.Add("453454554r454456456456545345545" + i.ToString(), "3453454545434564564564645346534534" + i.ToString(), "34564534534545645654645634543545545" + i.ToString());
            }
        }
    }
}
