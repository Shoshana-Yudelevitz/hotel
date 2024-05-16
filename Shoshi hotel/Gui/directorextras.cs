using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shoshi_hotel.Dal;
using Shoshi_hotel.Bll;

namespace Shoshi_hotel.Gui
{
    public partial class directorextras : Form
    {
        extradb tblex;
        public directorextras()
        {
            InitializeComponent();
            tblex = new extradb();
            dataGridView1.DataSource = tblex.GetList().Select(x => new { קוד = x.Code, תאור = x.Descrption, מחיר_תוספת= x.Extraprice, }).ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        
    }
}
