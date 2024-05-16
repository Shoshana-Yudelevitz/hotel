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
    public partial class customer : Form
    {
        ordersdb tblor;
        public customer()
        {
            InitializeComponent();
            tblor = new ordersdb();
            dataGridView1.DataSource = tblor.GetList().Select(x => new { מזמין = x.Inviting, תאריך_כניסה = x.Enterydate, תאריך_יציאה = x.Releasedate, סוג_פנסיון = x.Typeofpension, מספר_מיטה = x.Numberofbeds, מספר_הזמנה = x.Ordernumber, מחיר_סופי = x.Totalorder, הנחה = x.Discount, מספר_כרטיס = x.Ticketnumber, תוקף = x.Validity, שלוש_ספרות = x.Threedigits, תעודת_זהות = x.IDnumber1, קוד_כרטיס = x.Cardcode, תשלומים = x.Payments, סטטוס = x.Status, קוד_תוספות = x.Extracode }).ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    } }
