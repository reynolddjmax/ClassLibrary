using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DLL.COM
{
    public partial class Log : UserControl
    {
        public Log()
        {
            InitializeComponent();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Text = "";
        }


        public void Clear()
        {
            this.richTextBox1.Text = "";
        }

        public void Add(params string[] Str)
        {

            string time = DateTime.Now.ToString();

            string a = "";
            foreach (var item in Str)
	        {
		        a += "|" + item;
	        }

            a = a.Substring(1);
            
            this.richTextBox1.Text += a + "\r\n";
        }

        private void LogBoxChange(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.TextLength;
            richTextBox1.ScrollToCaret();
            Application.DoEvents();
        }

    }
}
