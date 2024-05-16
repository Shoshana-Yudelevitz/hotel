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
using Shoshi_hotel.BLL;
using Shoshi_hotel.Bll;

namespace Shoshi_hotel.Gui
{
    public partial class director : Form
    {
        pensiondb tbldirector;
        public director()
        {
            InitializeComponent();
            tbldirector = new pensiondb();
            dataGridView1.DataSource = tbldirector.GetList().Select(x => new { קןד = x.Code, תאור = x.Description, עלות_זוגי = x.Doublecost, עלות_משפחתי = x.Familycost }).ToList();
        }
    }
}
