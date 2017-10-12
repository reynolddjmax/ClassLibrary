using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DLL
{
    public partial class Procress : UserControl
    {

        //使用说明
        //Procress.Set(最大值)  初始化
        //Procress.Set(文本)    递增
        //Procress.Done()       完成




        public Procress()
        {
            InitializeComponent();
            this.progressBar1.Value = 0;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
        }

        public void Set(int MaxVaule)
        {
            this.progressBar1.Maximum = MaxVaule;
            this.progressBar1.Value = 0;
        }

        public void Set(string Text)
        {
            try
            {
                this.label1.Text = Text;

                this.progressBar1.Value++;

                this.label2.Text = this.progressBar1.Value.ToString() + "/" + this.progressBar1.Maximum.ToString();

                if (this.progressBar1.Value == this.progressBar1.Maximum)
                {
                    Done();
                }
                
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                

            }

        }

        public void Done()
        {
            this.label1.Text = "Done";
            this.label2.Text = "";
            this.progressBar1.Value = this.progressBar1.Maximum;
        }


    }
}
