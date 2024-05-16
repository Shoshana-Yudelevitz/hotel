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
using Shoshi_hotel.BLL;

namespace Shoshi_hotel.Gui
{
    public partial class ordering : Form
    {
       bool  flageOK = true;
        Bll.ordering o;
        Bll.ordering pe;
        Bll.ordering ord;
        orderingdb tblodb;
       bool  flageupdate = false;
        bool flageAdd = true;
        bool flageUpdate = true;
        orders frmo = null;//עצם מסוג טופס
        public ordering()
        {
            InitializeComponent();
            tblodb = new orderingdb();
            o = new Bll.ordering();
            dataGridView1.DataSource = tblodb.GetList().Select(x => new { פלאפון = x.Cellphone , שם_פרטי = x.Firstname,שם_משפחה=x.Lastname}).ToList();
            NotPossible();
        }

        
        private void Possible()
        {
            flageAdd = true;
            flageUpdate = true;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }
            
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("אינך הקשת את כל הפרטים");
            }
            else
            {
                if (frmo != null)
                {
                    pe = tblodb.Find((dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
                    frmo.Ex.Inviting = pe.Cellphone;
                }
                orders o = new orders(textBox1.Text);
                o.Show();
            }

        }
        public ordering(orders f) : this()
        {
            frmo = f;
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (!Validation.IsHebrew(textBox3.Text))
                errorProvider1.SetError(textBox3, " הקש אותיות בעיברית בלבד");
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (!Validation.IsHebrew(textBox2.Text))
                errorProvider1.SetError(textBox2, " הקש אותיות בעיברית בלבד");
        }
        public void ClearText()
        {
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (!Validation.IsNum(textBox1.Text))
                errorProvider1.SetError(textBox1, " הקש מספרים בלבד");
        }

      
        private void Fill(Bll.ordering e)
        {
            textBox1.Text = Convert.ToString(e.Cellphone);
            textBox2.Text = Convert.ToString(e.Firstname);
            textBox3.Text = Convert.ToString(e.Lastname);
        }
               
        private void NotPossible()
        {
            flageOK = false;
            flageupdate = false;
            flageAdd = true;
           
        }
    
        private bool CreateFieled(Bll.ordering c)
        {
           
            flageOK = true;
            Bll.ordering c1 = new Bll.ordering();
            c1 = tblodb.Find(textBox1.Text);
            if(c1 != null)
            {
                MessageBox.Show("לקוח קיים");
                return false;
            }

            errorProvider1.Clear();
            try
            {
                if (textBox1.Text == "")
                    throw new Exception("שדה חובה");
                else
                    c.Cellphone = (textBox1.Text);
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
                if (textBox2.Text.Length ==1 )
                    throw new Exception("לא תקין");
                else
                    c.Firstname = textBox2.Text;
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
                if (textBox3.Text.Length == 1)
                    throw new Exception("לא תקין");
                else
                    c.Lastname= textBox3.Text;
            }
            catch (Exception ex)
            {
                errorProvider1.SetError(textBox3, ex.Message);
                flageOK = false;
            }         
            return flageOK;
        }         


        private void button2_Click_1(object sender, EventArgs e)
        {
            flageAdd = true;
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string  Cellphone = (dataGridView1.SelectedRows[0].Cells[0].Value).ToString();
                o = tblodb.Find(Cellphone);
                DialogResult r = MessageBox.Show("אתה בטוח שברצונך למחוק לקוח זה ?", "אישור מחיקה", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (r == DialogResult.Yes)
                {
                    ClearText();
                    String st = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    tblodb.DeleteRow(Cellphone);
                }
            }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = tblodb.GetList().Select(x => new { פלאפון = x.Cellphone, שם_פרטי = x.Firstname, שם_משפחה = x.Lastname }).OrderBy(f => f.פלאפון).ToList();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox2.Enabled = true;
            textBox3.Enabled = false;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox3.Enabled = true;
            textBox2.Enabled = false;
        }

        roomforreservationdb rf = new roomforreservationdb();
        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ordersdb odb = new ordersdb();
           if (dataGridView1.SelectedRows.Count > 0)
           {
                string cellphone =(dataGridView1.SelectedRows[0].Cells[0].Value).ToString();
                Fill(o);
                if(o.ThisOrder() == null)
                {
                    MessageBox.Show("אינך הזמנת חדרים");
                    return;
                }
                dataGridView3.DataSource = odb.GetList().FindAll(x => x.Thisordering().Cellphone == cellphone).Select(x => new { מספר_הזמנה = x.Ordernumber, תאריך_כניסה = x.Enterydate }).ToList();
           }

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = tblodb.GetList().FindAll(x => x.Firstname.Contains(textBox2.Text)).Select(x => new { פלאפון = x.Cellphone, שם_פרטי = x.Firstname, שם_משפחה = x.Lastname }).ToList();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = tblodb.GetList().FindAll(x => x.Cellphone.Contains(textBox1.Text)).Select(x => new { פלאפון = x.Cellphone, שם_פרטי = x.Firstname, שם_משפחה = x.Lastname }).ToList();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = tblodb.GetList().FindAll(x => x.Lastname.Contains(textBox3.Text)).Select(x => new { פלאפון = x.Cellphone, שם_פרטי = x.Firstname, שם_משפחה = x.Lastname }).ToList();
        }
        

        private void dataGridView3_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ordersdb odb = new ordersdb();
            if (dataGridView3.SelectedRows.Count > 0)
            {
                int numberOrder = Convert.ToInt32(dataGridView3.SelectedRows[0].Cells[0].Value);
                Fill(o);
                if (o.ThisOrder() == null)
                {
                    MessageBox.Show("אינך הזמנת חדרים");
                    return;
                }
                dataGridView2.DataSource = rf.GetList().FindAll(x => x.Thisorders().Ordernumber == numberOrder).Select(x => new {מספר_חדר = x.Thisrooms().Numberoom }).ToList();// rf.GetList().FindAll(x => x.Ordernomber == o.ThisOrder().Ordernumber).Select(x => new { מספר_חדר = x.Roomnumber }).ToList();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("אינך הקשת את כל הפרטים");
            }
            else
            {
                if (frmo != null)
                {
                    pe = tblodb.Find((dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
                    frmo.Ex.Inviting = pe.Cellphone;
                }
                orders o = new orders(textBox1.Text);
                o.Show();
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            flageupdate = true;
            flageAdd = false;
            
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string Cellphone = (dataGridView1.SelectedRows[0].Cells[0].Value).ToString();
                o = tblodb.Find(Cellphone);
                Fill(o);
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = null;
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
            flageAdd = true;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
                    MessageBox.Show("אינך מלאת את כל הפרטים");

                if (flageAdd)
                {
                    Bll.ordering o = new Bll.ordering();
                    if (CreateFieled(o))
                    {
                        DialogResult r = MessageBox.Show("האם להוסיף לקוח זה ? ", "אישור הוספה", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (r == DialogResult.Yes)
                        {
                            tblodb.AddNew(o);
                            NotPossible();
                        }
                    }
                }
                else
                 if (flageupdate)
                {
                    if (CreateFieled(o))
                    {
                        DialogResult r = MessageBox.Show("האם לעדכן לקוח זה ? ", "אישור עידכון", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (r == DialogResult.Yes)
                        {
                            tblodb.UpdateRow(o);
                            NotPossible();
                        }
                    }
                }
                dataGridView1.DataSource = tblodb.GetList().Select(x => new { פלאפון = x.Cellphone, שם_פרטי = x.Firstname, שם_משפחה = x.Lastname }).OrderBy(f => f.פלאפון).ToList();
            }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            roomforreservationdb tblrf = new roomforreservationdb();
            if (dataGridView3.SelectedRows.Count > 0)
            {
                int numberOrder = Convert.ToInt32(dataGridView3.SelectedRows[0].Cells[0].Value);
                string cellphone = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                foreach (var item in rf.GetList())
                {
                    if (item.Ordernomber == numberOrder)
                        tblrf.DeleteRow(numberOrder);
                }
                Bll.ordersdb odb = new Bll.ordersdb();
                odb.DeleteRow(numberOrder);
                dataGridView3.DataSource = odb.GetList().FindAll(x => x.Thisordering().Cellphone == cellphone).Select(x => new { מספר_הזמנה = x.Ordernumber, תאריך_כניסה = x.Enterydate }).ToList();
                dataGridView2.DataSource = rf.GetList().FindAll(x => x.Thisorders().Ordernumber == numberOrder).Select(x => new { מספר_חדר = x.Thisrooms().Numberoom }).ToList();// rf.GetList().FindAll(x => x.Ordernomber == o.ThisOrder().Ordernumber).Select(x => new { מספר_חדר = x.Roomnumber }).ToList();
                MessageBox.Show("ההזמנה התבטלה");
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string Cellphone = (dataGridView1.SelectedRows[0].Cells[0].Value).ToString();
                o = tblodb.Find(Cellphone);
                Fill(o);
            }
        }
    }
}
