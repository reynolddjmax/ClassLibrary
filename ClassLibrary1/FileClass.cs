using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DLL
{
    public class FileClass
    {
        public static List<string> ScanFile(String FilePath, SearchOption Directories, params string[] SeachStr)
        {


            List<string> List = new List<string>();

            if (!Directory.Exists(FilePath)) return List;

            if (System.IO.Directory.Exists(FilePath) == false)
            {
                return List;
            }
            DirectoryInfo oDirectoryInfo = new DirectoryInfo(FilePath);


            foreach (string Seach in SeachStr)
            {
                FileInfo[] oFileInfo = oDirectoryInfo.GetFiles(Seach, Directories);
                foreach (FileInfo filename in oFileInfo)
                {

                    if (List.Contains(filename.FullName) == false)
	                {
                        List.Add(filename.FullName);
	                }
                    
                }



            }







            return List;

        }



        public static List<string> ScanDirectory(String FilePath, String SeachStr, SearchOption Directories)
        {

            List<string> List = new List<string>();
            DirectoryInfo oDirectoryInfo = new DirectoryInfo(FilePath);
            DirectoryInfo[] oFileInfo = oDirectoryInfo.GetDirectories(SeachStr, Directories);
            foreach (DirectoryInfo filename in oFileInfo)
            {
                List.Add(filename.FullName);
            }



            return List;

        }


        [DllImport("kernel32")]//返回0表示失败，非0为成功
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]//返回取得字符串缓冲区的长度
        private static extern long GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public static string ReadIniData(string Section, string Key, string NoText, string iniFilePath)
        {
            if (File.Exists(iniFilePath))
            {
                StringBuilder temp = new StringBuilder(1024);
                GetPrivateProfileString(Section, Key, NoText, temp, 1024, iniFilePath);
                return temp.ToString();
            }
            else
            {
                return "";
            }
        }

        public static bool WriteIniData(string Section, string Key, string Value, string iniFilePath)
        {
            if (File.Exists(iniFilePath))
            {
                long OpStation = WritePrivateProfileString(Section, Key, Value, iniFilePath);
                if (OpStation == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public static string OpenFile(string Title,string Filter)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "打开EXCEL文件";
            openFile.Filter = Filter + "|所有文件(*.*)|*.*";
            //openFile.Filter = "Excel|*.xls;*.xlsx|所有文件(*.*)|*.*";
            openFile.ShowDialog();
            return openFile.FileName;
        }

        public static string OpenFolder()
        {

            FolderBrowserDialog FBD = new FolderBrowserDialog();
            FBD.ShowDialog();
            return FBD.SelectedPath;
        }

        public static string ReadTxt(string FilePath)
        {
            FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            StreamReader m_streamReader = new StreamReader(fs, Encoding.Default);
            string strLine = m_streamReader.ReadToEnd();
            fs.Close();
            m_streamReader.Close();
            return strLine;
        }

        public static void WriteTxt(string Str,string FilePath)
        {
            FileStream fs = new FileStream(FilePath, FileMode.Create);
            //获得字节数组
            byte[] data = System.Text.Encoding.Default.GetBytes(Str);
            //开始写入
            fs.Write(data, 0, data.Length);
            //清空缓冲区、关闭流
            fs.Flush();
            fs.Close();
        }

        //检查是否图片
        public static bool CheckPic(string FileName)
        {
            string LastStr = "";

            if (FileName.IndexOf(".") ==-1)
            {
                return false;
            }

            LastStr = FileName.Substring(FileName.LastIndexOf(".") + 1).ToLower();

            if (LastStr == "tif" || LastStr == "tiff" || LastStr == "bmp" || LastStr == "jpeg" || LastStr == "jpg" || LastStr == "jpe" || LastStr == "png")
            {
                return true;
            }

            return false;
        }

        ///
        /// 将文件转为内存流
        ///
        /// 
        /// 
        public static MemoryStream ReadFileMS(string path)
        {
            if (!File.Exists(path))
                return null;

            using (FileStream file = new FileStream(path, FileMode.Open))
            {
                byte[] b = new byte[file.Length];
                file.Read(b, 0, b.Length);

                MemoryStream stream = new MemoryStream(b);
                return stream;
            }
        }

        ///
        /// 将内存流转为图片
        ///
        /// 
        /// 
        public static Image GetImageFileFromMemStream(string path)
        {
            MemoryStream stream = ReadFileMS(path);
            return stream == null ? null : Image.FromStream(stream);
        }





        //打开文件
        public static string OpenFile(string Type)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            //fileDialog.Multiselect = true;

            fileDialog.Title = "请选择文件" + Type;
            fileDialog.Filter = "所有文件(*." + Type + ")|*." + Type;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                return fileDialog.FileName;

            }

            return "";
        }
        //保存文件
        public static string SaveFile(string Type)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            //fileDialog.Multiselect = true;

            fileDialog.Title = "请选择文件" + Type;
            fileDialog.Filter = "所有文件(*."+Type+")|*."+Type;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                return fileDialog.FileName;

            }

            return "";
        }
    }
}
