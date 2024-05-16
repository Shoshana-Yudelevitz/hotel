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
    public partial class newrooms : rooms
    {
        bool flageupdate = false;
        roomsdb tblnew;
        bool flageAdd = false;
        public newrooms()
        {
            
            InitializeComponent();
            tblnew = new roomsdb();
            dataGridView1.DataSource = tblnew.GetList().Select(x => new { מספר_חדר = x.Numberoom, מספר_מיטה = x.Numberoom, קומה = x.Floor, סטטוס = x.Status, קוד_מחיר = x.Pricecode }).ToList();
        }
        bool flageOK;
        

        private bool CreateFieled(rooms r)
        {
            flageOK = true;
            errorProvider1.Clear();
            try
            {
                if (textBox1.Text == "")
                    throw new Exception("שדה חובה");
                else
                    r.Text = textBox1.Text;
            }
            catch (Exception ex)
            {
                errorProvider1.SetError(textBox1, ex.Message);
                flageOK = false;
            }
            try
            {
                if (textBox2.Text == "")
                    throw new Exception("שדה חובה");
                else
                    r.Text= textBox2.Text;
            }
            catch (Exception ex)
            {
                errorProvider1.SetError(textBox2, ex.Message);
                flageOK = false;
            }
            try
            {
                if (textBox3.Text == "")
                    throw new Exception("שדה חובה");
                else
                   r.Text= textBox3.Text;
            }
            catch (Exception ex)
            {
                errorProvider1.SetError(textBox3, ex.Message);
                flageOK = false;
            }
            try
            {
                if (textBox4.Text == "")
                    throw new Exception("שדה חובה");
                else
                   r.Text= textBox4.Text;
            }
            catch (Exception ex)
            {
                errorProvider1.SetError(textBox4, ex.Message);
                flageOK = false;
            }
           
            
           

            return flageOK;

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (flageAdd)
            {
                rooms r = new rooms();
                if (CreateFieled(r))
                {
                    DialogResult d = MessageBox.Show("האם להוסיף לקוח זה ? ", "אישור הוספה", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (d == DialogResult.Yes)
                    {
                    //tblr.AddNew(r);
                       //NotPossible();
                    }
                }
            }
            else
              if(flageupdate)
            {
               // if (CreateFieled(r))
                {
                    DialogResult d = MessageBox.Show("האם לעדכן לקוח זה ? ", "אישור עידכון", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (d == DialogResult.Yes)
                    {
                      //  tblcustomer.UpdateRow(c);
                       // NotPossible();
                    }
                }
            }
        }
    }
}

