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
using Shoshi_hotel.BLL;
using Shoshi_hotel.Dal;
using Shoshi_hotel.Properties;

namespace Shoshi_hotel.Gui
{
    public partial class Priceperroom : Form
    {
        bool flageupdate = false;
        bool flageAdd = true;
        Bll.priceroom ex;
        priceroomdb tblpr;

        bool flageOK = false;
        public Priceperroom()
        {
            InitializeComponent();
            ex = new Bll.priceroom();
            tblpr = new priceroomdb();
            comboBox1.DataSource = tblpr.GetList().FindAll(x => x.Status).Select(X => X.Pricecode).ToList(); ;
            comboBox2.DataSource = tblpr.GetList().FindAll(x => x.Status).Select(X => X.Roomtype).ToList();
            comboBox3.DataSource = tblpr.GetList().FindAll(x => x.Status).Select(X => X.Pricemight).ToList();
            dataGridView1.DataSource = tblpr.GetList().Where(x => x.Status == true).Select(x => new { קוד = x.Pricecode, סוג = x.Roomtype, מחיר = x.Pricemight }).OrderBy(f => f.קוד).ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void ClearText()
        {
            textBox4.Text = null;
            textBox5.Text = null;
            textBox6.Text = null;
        }
       
        private void NotPossible()
        {
            flageOK = false;
            flageupdate = false;
            flageAdd = false;
            groupBox2.Enabled = true;
        }
        private bool CreateFieled(Bll.priceroom c)
        {
            flageOK = true;
            errorProvider1.Clear();
            
            try
            {
                if (textBox4.Text == "")
                    throw new Exception("שדה חובה");
                else
                    c.Pricecode = Convert.ToInt32(textBox4.Text);
            }
            catch (Exception ex)
            {
                errorProvider1.SetError(textBox4, ex.Message);
                flageOK = false;
            }


            if (comboBox4.Text == "פעיל")
            {
                c.Status = true;
            }
            if (comboBox4.Text == "לא פעיל")
            {
                c.Status = false;
            }
            try
            {
                if (textBox5.Text == "")
                    throw new Exception("שדה חובה");
                if (textBox5.Text.Length == 1)
                {
                    throw new Exception("לא תקין");
                }
                else
                    c.Roomtype = Convert.ToString(textBox5.Text);
            }
            catch (Exception ex)
            {
                errorProvider1.SetError(textBox5, ex.Message);
                flageOK = false;
            }
           
            try
            {
                if (textBox6.Text == "")
                    throw new Exception("שדה חובה");
                else
                    c.Pricemight = Convert.ToInt32(textBox6.Text);
            }
            catch (Exception ex)
            {
                errorProvider1.SetError(textBox6, ex.Message);
                flageOK = false;
            }
            
            return flageOK;

        }

        private void Fill(Bll.priceroom e)
        {
            textBox4.Text = Convert.ToString(e.Pricecode);
            textBox5.Text = e.Roomtype;
            textBox6.Text = Convert.ToString(e.Pricemight);
        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            textBox4.Text = tblpr.GetNextKey().ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            flageupdate = true;
            flageAdd = false;
            groupBox2.Enabled = true;
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int pricecode = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                ex = tblpr.Find(pricecode);
                Fill(ex);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            flageAdd = true;
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int code = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                ex = tblpr.Find(code);
                DialogResult r = MessageBox.Show("אתה בטוח שברצונך למחוק מחיר  זה ?", "אישור מחיקה", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (r == DialogResult.Yes)
                {
                    ClearText();
                    ex.Status = false;
                    tblpr.UpdateRow(ex);
                }

            }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = tblpr.GetList().Where(x => x.Status == true).Select(x => new { קוד = x.Pricecode, סוג = x.Roomtype, מחיר = x.Pricemight }).OrderBy(f => f.קוד).ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int g = tblpr.GetList().Count(x => x.Roomtype== textBox5.Text);
            {
                if (flageAdd)
                {
                    if (g == 1)
                    {

                        MessageBox.Show("מחיר קיים");
                    }
                    else
                    {


                        if (CreateFieled(ex))
                        {
                            DialogResult r = MessageBox.Show("האם להוסיף מחיר זה ? ", "אישור הוספה", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                            if (r == DialogResult.Yes)
                            {
                                tblpr.AddNew(ex);
                                NotPossible();
                            }
                        }
                        else
                            MessageBox.Show("בדוק אם הנתונים שהקשת נכונים");

                    }
                }
                else
                    if (flageupdate)
                {
                    if (CreateFieled(ex))
                    {
                        DialogResult r = MessageBox.Show("האם לעדכן מחיר זה ? ", "אישור עידכון", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (r == DialogResult.Yes)
                        {
                            tblpr.UpdateRow(ex);
                            NotPossible();
                        }
                    }
                }
                dataGridView1.DataSource = tblpr.GetList().Where(x => x.Status == true).Select(x => new { קוד = x.Pricecode, סוג = x.Roomtype, מחיר = x.Pricemight }).OrderBy(f => f.קוד).ToList();
            }
        }
        private void button6_Click_1(object sender, EventArgs e)
        {
            if(textBox7.Text=="shosh")
            {
                groupBox1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
                button1.Visible = false;
                button8.Visible = true;
            }
            else
                MessageBox.Show("קוד לא נכון");
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (!Validation.IsNum(textBox4.Text))
                errorProvider1.SetError(textBox4, " הקש מספרים בלבד");
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (!Validation.IsHebrew(textBox5.Text))
                errorProvider1.SetError(textBox5, " הקש אותיות בעיברית בלבד");
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (!Validation.IsNum(textBox6.Text))
                errorProvider1.SetError(textBox6, " הקש מספרים בלבד");
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = true;
            comboBox2.Enabled = false;
            comboBox3.Enabled = false;
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = false;
            comboBox2.Enabled = true;
            comboBox3.Enabled = false;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            comboBox3.Enabled = true;
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)

                dataGridView1.DataSource = tblpr.GetList().Where(x =>x.Pricecode.ToString() == (comboBox1.Text)).Select(x => new { קוד = x, סוג_חדר=x.Roomtype , מחיר_ללילה = x.Pricemight }).OrderBy(f => f.קוד).ToList();

            if (radioButton2.Checked == true)

                dataGridView1.DataSource = tblpr.GetList().Where(x => x.Roomtype.ToString() == (comboBox2.Text)).Select(x => new { קוד = x.Pricecode, סוג_חדר = x.Roomtype, מחיר_ללילה = x.Pricemight }).OrderBy(f => f.קוד).ToList();

            if (radioButton3.Checked == true)
                dataGridView1.DataSource = tblpr.GetList().Where(x => x.Pricemight.ToString() == (comboBox3.Text)).Select(x => new { קוד = x.Pricecode, סוג_חדר = x.Roomtype, מחיר_ללילה = x.Pricemight }).OrderBy(f => f.קוד).ToList();
        }

        
    }
    }
    

