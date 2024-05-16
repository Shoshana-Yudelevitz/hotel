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
    public partial class f : Form
    {
        public f()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          extras o = new Gui.extras();
            o.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           pension o = new pension();
            o.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            rooms o = new rooms(this);
            o.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ordering o = new ordering();
            o.Show();
        }


        private void button6_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            Priceperroom f = new Priceperroom();
            f.Show();
        } 
    }
}
