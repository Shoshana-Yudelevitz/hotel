using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shoshi_hotel.Bll;
using Shoshi_hotel.Dal;

namespace Shoshi_hotel.Gui
{
    public partial class directorpension : Form
    {

        pensiondb tblpen;
        public directorpension()
        {
            InitializeComponent();
            tblpen = new pensiondb();
            dataGridView1.DataSource = tblpen.GetList().Select(x => new { קוד = x.Code, תאור = x.Description, עלות_זוגי = x.Doublecost, עלות_משפחתי = x.Familycost }).ToList();
        }
    }
}
