using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace DLL
{
    public class WebGet
    {
        //ContentType = gb2312  UTF-8
        public static string HttpGet(string Url, string postDataStr ,string Proxy,string ContentType)
        {
            Thread.Sleep(1000);
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
                int aa = request.ServicePoint.ConnectionLimit;
                request.Timeout = 2000;


                if (Proxy != "")
                {
                    //request.Proxy = new WebProxy("127.0.0.1:1098");
                    request.Proxy = new WebProxy(Proxy);
                }


                //request.KeepAlive = false;
                request.Method = "GET";

                request.ContentType = "text/html;charset=" + ContentType;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();

                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding(ContentType));

                string retString = myStreamReader.ReadToEnd();

                myStreamReader.Close();
                myResponseStream.Close();

                return retString;
            }
            catch (Exception)
            {

                return "";
            }

        }



    }
}
