using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DLL
{
    public class TxtStr
    {


        public static string Read(string FilePath)
        {
            FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            StreamReader m_streamReader = new StreamReader(fs, Encoding.Default);
            string strLine = m_streamReader.ReadToEnd();
            fs.Close();
            m_streamReader.Close();
            return strLine;
        }

        public static string[] ReadLine(string FilePath, Encoding encoding)
        {
            FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            StreamReader sr = new StreamReader(fs, encoding);
            string s;
            List<string> l = new List<string>();
            while ((s = sr.ReadLine()) != null)
            {

                l.Add(s);
            }

            return l.ToArray();
        }

        public static void Write(string Str, string FilePath ,bool Override)
        {
            if (!Override)
            {
                int i = 1;
                string DirName = System.IO.Path.GetDirectoryName(FilePath);
                string FileName = System.IO.Path.GetFileNameWithoutExtension(FilePath);
                string Extension = System.IO.Path.GetExtension(FilePath);

                while (System.IO.File.Exists(FilePath))
                {


                    FilePath = DirName + "\\" +FileName + "(" + i.ToString() + ")" + Extension;
                    i++;
                }

                
            }



            FileStream fs = new FileStream(FilePath, FileMode.Create);
            //获得字节数组
            byte[] data = System.Text.Encoding.Default.GetBytes(Str);
            //开始写入
            fs.Write(data, 0, data.Length);
            //清空缓冲区、关闭流
            fs.Flush();
            fs.Close();
        }
        public static void Write(string Str, string FilePath, bool Override , System.Text.Encoding Encoding)
        {
            if (!Override)
            {
                int i = 1;
                string DirName = System.IO.Path.GetDirectoryName(FilePath);
                string FileName = System.IO.Path.GetFileNameWithoutExtension(FilePath);
                string Extension = System.IO.Path.GetExtension(FilePath);

                while (System.IO.File.Exists(FilePath))
                {


                    FilePath = DirName + "\\" + FileName + "(" + i.ToString() + ")" + Extension;
                    i++;
                }


            }



            


            StreamWriter sw = new StreamWriter(FilePath,false ,Encoding);
            sw.WriteLine(Str);
            sw.Close();


            //FileStream fs = new FileStream(FilePath, FileMode.Create);
            ////获得字节数组
            //byte[] data = System.Text.Encoding.Default.GetBytes(Str);
            //data = Encoding.Convert(System.Text.Encoding.Default, Encoding, data);

            ////开始写入
            //fs.Write(data, 0, data.Length);
            ////清空缓冲区、关闭流
            //fs.Flush();



            //fs.Close();
        }

    }
}
