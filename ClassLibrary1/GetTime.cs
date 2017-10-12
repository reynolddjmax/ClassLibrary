using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace DLL
{
    public partial class GetTime : Form
    {
        public GetTime()
        {
            InitializeComponent();
        }


        Stopwatch sw = null;


        public void Record(string Str)
        {


            if (sw == null)
            {
                sw = new Stopwatch();
                sw.Start();
            }
            else
            {
                sw.Stop();

                string time = sw.Elapsed.TotalSeconds.ToString();

                listBox1.Items.Add(Str + " : " + time);

                sw = new Stopwatch();
                sw.Start();

                Application.DoEvents();

            }



        }

        public void StopTime()
        {
            sw = null;

        }

    }
}
