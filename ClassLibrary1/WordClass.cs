using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace DLL
{
    public class WordClass
    {
        public Microsoft.Office.Interop.Word.Application WordApp;
        public WordClass()
        {
            WordApp = new Microsoft.Office.Interop.Word.Application();
        }
        public void WordQuit()
        {
            WordApp.Quit();
        }

        public Microsoft.Office.Interop.Word.Document Doc;
        public Microsoft.Office.Interop.Word.Document DocOpen(string FilePath, bool Visable)
        {
            WordApp.Visible = Visable;
            WordApp.ScreenUpdating = Visable;
            Doc = WordApp.Documents.Open(FilePath);
            return WordApp.Documents.Open(FilePath);
        }


        public Microsoft.Office.Interop.Word.Document DocCreat(string SavePath,Microsoft.Office.Interop.Word.WdSaveFormat SaveFormat, bool Visable)
        {
            WordApp.Visible = Visable;
            WordApp.ScreenUpdating = Visable;
            Doc = WordApp.Documents.Add();

            if (System.IO.File.Exists(SavePath))
            {
                System.IO.File.Delete(SavePath);
            }

            Doc.SaveAs(SavePath, SaveFormat);
            return Doc;
        }

        public void DocSave()
        {
            Doc.Save();
        }
        public void DocSaveAs(string FilePath ,Microsoft.Office.Interop.Word.WdSaveFormat SaveFormat)
        {
            Doc.SaveAs(FilePath, SaveFormat);
        }
        public void DocClose()
        {
            Doc.Close();
        }


        //Word中的表格操作
        public string GetTableVaule(int Table, int Row, int Column)
        {
            string Str = Doc.Tables[Table].Cell(Row, Column).Range.Text;
            Str = Str.Replace("\r\a", "");
            Str = Str.Replace("\r", "");
            return Str;
        }
        public void SetTableVaule(string Vaule, int Table, int Row, int Column)
        {
            Doc.Tables[Table].Cell(Row, Column).Range.Text = Vaule;
        }

        public void ReplaceStr(string Str, string Vaule)
        {
            object MissingValue = Type.Missing;
            Doc.Content.Find.Execute(Str, MissingValue, MissingValue, MissingValue, MissingValue, MissingValue, MissingValue, MissingValue, MissingValue, Vaule, Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll, MissingValue, MissingValue, MissingValue, MissingValue);
        }


        //Word中的表格操作
        public static string GetTableVaule(int Table, int Row, int Column, Microsoft.Office.Interop.Word.Document WordDoc)
        {
            try
            {
                string Str = WordDoc.Tables[Table].Cell(Row, Column).Range.Text;
                Str = Str.Replace("\r\a", "");
                Str = Str.Replace("\r", "");
                return Str;
            }
            catch (Exception)
            {
                
                return "";
            }

        }
        public static void SetTableVaule(string Vaule, int Table, int Row, int Column, Microsoft.Office.Interop.Word.Document WordDoc)
        {
            WordDoc.Tables[Table].Cell(Row, Column).Range.Text = Vaule;
        }
        public void ReplaceStr(string Str, string Vaule, Microsoft.Office.Interop.Word.Document WordDoc)
        {
            object MissingValue = Type.Missing;
            WordDoc.Content.Find.Execute(Str, MissingValue, MissingValue, MissingValue, MissingValue, MissingValue, MissingValue, MissingValue, MissingValue, Vaule, Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll, MissingValue, MissingValue, MissingValue, MissingValue);
        }

        //Word替换功能
        public static void StrReplace(string StrOld, string StrNew, Microsoft.Office.Interop.Word.Document WordDoc)
        {
            object Object = null;

            WordDoc.Content.Find.Execute(StrOld, Object, Object, Object, Object, Object, Object, Object, Object, StrNew, Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll, Object, Object, Object, Object);
 
        }





    }





    public class WordsClass : WordClass //DocList
    {

        public WordsClass()
        {
            DocList = new List<Microsoft.Office.Interop.Word.Document>();
        }

        public List<Microsoft.Office.Interop.Word.Document> DocList;

    }

}
