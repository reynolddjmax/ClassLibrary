using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DLL.COM.DropBox
{
    public partial class DropPanel : UserControl
    {
        public DropPanel()
        {
            InitializeComponent();
        }


        private void Drag_Drop(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                String[] files = (String[])e.Data.GetData(DataFormats.FileDrop);

                foreach (String s in files)
                {
                    Do(s);
                }

            }



        }



        private void Drag_Enter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }



        public virtual void Do(string path)
        {
 
        }

    }
}
