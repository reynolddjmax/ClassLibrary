using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace DLL
{
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();

            this.Show();

            th = new Thread(new ThreadStart(LoadResources));
            th.Priority = ThreadPriority.Highest;
            th.Start();
        }

        Thread th;
        public void Start()
        {

        }


        public void Stop()
        {
            Contine = false;
            th.Abort();
            this.Dispose();
        }


        bool Contine = true;

        void LoadResources()
        {

            while (Contine)
            {
                this.Invoke(new MethodInvoker(delegate 
                    {
                        if (this.label2.Text.Length == 5)
                        {
                            this.label2.Text = "";
                        }

                        this.label2.Text += ".";
                        Application.DoEvents();

                    }));

                Thread.Sleep(200);
            }
            
        }
    }
}
