using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLL
{
    public class ExcelClass
    {
        public Microsoft.Office.Interop.Excel.Application ExcelApp;
        public ExcelClass()
        {
            ExcelApp = new Microsoft.Office.Interop.Excel.Application();

            ExcelApp.DisplayAlerts = false;  //禁止弹出一些确认窗口

            
        }
        public void ExcelQuit()
        {
            ExcelApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(ExcelApp);
            ExcelApp = null;
            GC.Collect();
        }


        public void ExcelShow()
        {
            ExcelApp.Visible = true;
            ExcelApp.ScreenUpdating = true;
        }

        public Microsoft.Office.Interop.Excel.Workbook Xls;
        public void XlsOpen(string FilePath, bool Visable)
        {

            ExcelApp.Visible = Visable;
            ExcelApp.ScreenUpdating = Visable;
            Xls = ExcelApp.Workbooks.Open(FilePath);

            ExcelApp.ActiveWorkbook.CheckCompatibility = false; //兼容性检查
        }

        public void XlsCreat(string FilePath, bool Visable)
        {

            ExcelApp.Visible = Visable;
            ExcelApp.ScreenUpdating = Visable;


            Xls = ExcelApp.Workbooks.Add();



            Xls.SaveAs(FilePath);

        }

        public void XlsSave()
        {
            Xls.Save();
        }
        public void XlsClose()
        {
            Xls.Close();
            
        }



        public Microsoft.Office.Interop.Excel.Workbook Xls1;
        public void Xls1Open(string FilePath, bool Visable)
        {

            ExcelApp.Visible = Visable;
            ExcelApp.ScreenUpdating = Visable;
            Xls1 = ExcelApp.Workbooks.Open(FilePath);
        }

        public void Xls1Save()
        {
            Xls1.Save();
        }
        public void Xls1Close()
        {
            Xls1.Close();

        }



        //读写Excel
        public string LoadVaule(object T,int C ,int R)
        {



            string Str = ((Microsoft.Office.Interop.Excel.Range)Xls.Worksheets[T].Cells[R, C]).Text.ToString();
            return Str;
        }

        public void WriteVaule(object T, int C, int R, object Vaule)
        {
            Xls.Worksheets[T].Cells[R, C] = Vaule;
        }

        public void WriteDataTable(object Sheet,System.Data.DataTable dt)
        {
            int rowCount = dt.Rows.Count + 1;
            int colCount = dt.Columns.Count;
            object[,] dataArray = new object[rowCount, colCount];
            for (int k = 0; k < colCount; k++)
            {
                dataArray[0, k] = dt.Columns[k].ColumnName;
            }
            for (int i = 0; i < rowCount - 1; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    dataArray[i + 1, j] = dt.Rows[i][j].ToString();
                }
            }

            Microsoft.Office.Interop.Excel.Worksheet excelSheet = Xls.Worksheets[Sheet];
            Microsoft.Office.Interop.Excel.Range range = excelSheet.Range["A1", excelSheet.Cells[rowCount, colCount]];

            range.Cells.Borders.LineStyle = 1;

            range.Value2 = dataArray;
            range.EntireColumn.AutoFit();
            //excelSheet.Range["A1", excelSheet.Cells[rowCount, colCount]].Value2 = dataArray;


        
        }

        public string LoadVaule(object T, int C, int R, Microsoft.Office.Interop.Excel.Workbook XlsFile)
        {
            string Str = ((Microsoft.Office.Interop.Excel.Range)XlsFile.Worksheets[T].Cells[R, C]).Text.ToString();
            return Str;
        }

        public void WriteVaule(object T, int C, int R, object Vaule, Microsoft.Office.Interop.Excel.Workbook XlsFile)
        {
            XlsFile.Worksheets[T].Cells[R, C] = Vaule;
        }


        //删除行列

        //简写EC.Xls.Worksheets["承包方信息"].Columns["R:AA"].Delete();

        //Worksheet ws = EC.Xls.Worksheets[2];
        //Range r = ws.Rows[1];
        //r.Delete();
        //r = ws.Rows["2:6"]; 删除2~6行
        //r.Delete();
        //r = ws.Columns[1];
        //r = ws.Columns["A:Z"]; 删除A~Z列
        //r.Delete();

        //合并单元格
        //Worksheet ws = EC.Xls.Worksheets[1];
        //Range r = ws.get_Range("A1","D1");
        //r.Merge(Type.Missing);


        //新建工作表
        //EC.Xls.Sheets.Add();
        //EC.Xls.ActiveSheet.Name = "1111";
        //Worksheet ws = EC.Xls.ActiveSheet;

        //画边框
        //r.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

    }




    public class ExcelsClass:ExcelClass //XlsList
    {
        public ExcelsClass()
        {
            XlsList = new List<Microsoft.Office.Interop.Excel.Workbook>();
        }

        public List<Microsoft.Office.Interop.Excel.Workbook> XlsList;
        public Microsoft.Office.Interop.Excel.Workbook XlsOpen(string FilePath, bool Visable)
        {
            if (Visable)
            {
                ExcelApp.Visible = true;
                ExcelApp.ScreenUpdating = true;
            }
            else
            {
                ExcelApp.Visible = false;
                ExcelApp.ScreenUpdating = false;
            }
            return ExcelApp.Workbooks.Open(FilePath);
        }
    }

}
