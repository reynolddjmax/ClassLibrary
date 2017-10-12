using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLL
{
    public class StrClass
    {
        public static string Left(string param, int length)
        {
            string result = param.Substring(0, length);
            return result;
        }
        public static string Right(string param, int length)
        {
            string result = param.Substring(param.Length - length, length);
            return result;
        }
        public static string Index(string param, string startIndex, string endIndex)
        {
            try
            {
                string result = param.Substring(param.IndexOf(startIndex) + startIndex.Length, param.IndexOf(endIndex) - param.IndexOf(startIndex) - startIndex.Length);
                return result;
            }
            catch (Exception)
            {
                
                return "";
            }

        }

        //取Str中 StrA 和 StrB 中的部分 
        public static string StrMid(string Str, string StrA, string StrB)
        {
            string a = Str.Substring(Str.IndexOf(StrA) + StrA.Length);

            if (StrB == "")
            {
                return a;
            }

            string b = a.Substring(0, a.IndexOf(StrB));
            return b;
        }

        public static string GetBigNum(string Str)
        {
            string TranNum = "";

            switch (Convert.ToInt16(Str.Substring(0, 1)))
            {
                case 0:
                    TranNum = "";
                    break;
                case 1:
                    TranNum = "十";
                    break;
                case 2:
                    TranNum = "二十";
                    break;
                case 3:
                    TranNum = "三十";
                    break;
                case 4:
                    TranNum = "四十";
                    break;
                case 5:
                    TranNum = "五十";
                    break;
                case 6:
                    TranNum = "六十";
                    break;
                case 7:
                    TranNum = "七十";
                    break;
                case 8:
                    TranNum = "八十";
                    break;
                case 9:
                    TranNum = "九十";
                    break;
            }


            switch (Convert.ToInt16(Str.Substring(1, 1)))
            {
                case 0:
                    break;
                case 1:
                    TranNum += "一";
                    break;
                case 2:
                    TranNum += "二";
                    break;
                case 3:
                    TranNum += "三";
                    break;
                case 4:
                    TranNum += "四";
                    break;
                case 5:
                    TranNum += "五";
                    break;
                case 6:
                    TranNum += "六";
                    break;
                case 7:
                    TranNum += "七";
                    break;
                case 8:
                    TranNum += "八";
                    break;
                case 9:
                    TranNum += "九";
                    break;
            }

            return TranNum;
        }
    }
}
