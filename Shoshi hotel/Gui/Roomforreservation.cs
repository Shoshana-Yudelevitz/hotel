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
    public partial class Roomforreservation : Form
    {
        public Roomforreservation()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            rooms r = new rooms();
            r.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
