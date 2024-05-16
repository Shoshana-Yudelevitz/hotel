using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shoshi_hotel.Gui
{
    public partial class firstform : Form
    {
        public firstform()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ordering o = new ordering();
            o.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            f o = new f();
            o.Show();
            

        }

        
    }
}
