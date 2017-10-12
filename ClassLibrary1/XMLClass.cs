using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace DLL
{
    public class XMLClass
    {
        public XMLClass()
        {
            document = new XmlDocument();
            node = document.CreateElement("News");

        }

        XmlDocument document;
        XmlElement node;


        public void Add(string StrA, string StrB)
        {
            node.SetAttribute(StrA, StrB);
        }
        public string Get(string Str)
        {
            string Vaule = node.Attributes[Str].Value;
            return Vaule;
        }
    }
}
