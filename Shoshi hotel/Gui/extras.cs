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
using Shoshi_hotel.Gui;
using Shoshi_hotel.Properties;
using Shoshi_hotel.BLL;


namespace Shoshi_hotel.Gui
{
    public partial class extras : Form
    {
        bool flageOK = false;
        bool flageupdate = false;
        bool flageAdd = false;
        extradb tblextradb;
        Bll.extras ex;
        Bll.extras ext;
        orders frmo = null;//עצם מסוג טופס

        public extras()
        {
           
            InitializeComponent();
            ex = new Bll.extras();
            ext= new Bll.extras();
            tblextradb = new extradb();
            dataGridView1.ForeColor = Color.GreenYellow;
            comboBox1.DataSource = tblextradb.GetList().FindAll(x => x.Status).Select(X => X.Code).ToList(); ;
            comboBox2.DataSource = tblextradb.GetList().FindAll(x => x.Status).Select(X => X.Descrption).ToList();
            comboBox3.DataSource = tblextradb.GetList().FindAll(x => x.Status).Select(X => X.Extraprice).ToList();
            dataGridView1.DataSource = tblextradb.GetList().Where(x => x.Status == true).Select(x => new { קוד = x.Code, תאור = x.Descrption, מחיר = x.Extraprice }).OrderBy(f=> f.קוד).ToList();

        }
        public extras(orders g) : this()
        {
            frmo = g;
        }
        private void NotPossible()
        {
            flageOK = false;
            flageupdate = false;
            flageAdd = true;
            groupBox2.Enabled = true;
        }
        private void Possible()
        {
            flageOK = true;
            flageupdate = true;
            flageAdd = true;
            groupBox2.Enabled = false;
            groupBox2.Visible = false;
        }
        private void cleartext()
        {
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            groupBox3.Visible = true;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if(textBox7.Text=="shosh")
            {
                groupBox2.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
                button6.Visible = true;
                button7.Visible = true;
                button1.Visible = false;
            }
            else
                MessageBox.Show("קוד לא נכון");
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

        private void button4_Click(object sender, EventArgs e)
        {
            groupBox2.Enabled = true;
            textBox4.Text = tblextradb.GetNextKey().ToString();

            flageAdd = true;
        }
       
        private bool CreateFieled(Bll.extras c)
        {   
            flageOK = true;
            errorProvider1.Clear();
            try
            {
                if (textBox4.Text == "")
                    throw new Exception("שדה חובה");
                
                else
                    c.Code = Convert.ToInt32( textBox4.Text);
            }
            catch (Exception ex)
            {
                errorProvider1.SetError(textBox4, ex.Message);
                flageOK = false;
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
                    c.Descrption = textBox5.Text;
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
                    c.Extraprice = Convert.ToInt32(textBox6.Text);
            }
            catch (Exception ex)
            {
                errorProvider1.SetError(textBox6, ex.Message);
                flageOK = false;
            }
            if (comboBox1.Text == "פעיל")
            {
                c.Status = true;
            }
            else
            {
                c.Status = true;
            }
            return flageOK;

        }
        
        private void Fill(Bll.extras e)
        {
            textBox4.Text = Convert.ToString(e.Code);
            textBox5.Text = e.Descrption;
            textBox6.Text = Convert.ToString(e.Extraprice);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int g = tblextradb.GetList().Count(x => x.Descrption == textBox5.Text);  
            {
                if (flageAdd)
                {
                    if (g == 1)
                    { 
                        MessageBox.Show("תוספת קיימת");
                    }

                    else
                    {
                        if (CreateFieled(ex))
                        {
                            DialogResult r = MessageBox.Show("האם להוסיף תוספת זו למאגר התוספות ? ", "אישור הוספה", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                            if (r == DialogResult.Yes)
                            {
                                tblextradb.AddNew(ex);
                                NotPossible();
                            }
                        }
                    }
                }
                else
             if (flageupdate)
                {
                    if (CreateFieled(ex))
                    {
                        DialogResult r = MessageBox.Show("האם לעדכן תוספת זו  ? ", "אישור עידכון", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (r == DialogResult.Yes)
                        {
                            tblextradb.UpdateRow(ex);
                            NotPossible();
                        }
                    }
                }

                dataGridView1.DataSource = tblextradb.GetList().Where(x => x.Status == true).Select(x => new { קוד = x.Code, תאור = x.Descrption, מחיר = x.Extraprice }).OrderBy(f => f.קוד).ToList();
                comboBox1.DataSource = tblextradb.GetList().FindAll(x => x.Status).Select(X => X.Code).ToList(); ;
                comboBox2.DataSource = tblextradb.GetList().FindAll(x => x.Status).Select(X => X.Descrption).ToList();
                comboBox3.DataSource = tblextradb.GetList().FindAll(x => x.Status).Select(X => X.Extraprice).ToList();
            }
        }

        
        public void ClearText()
        {
            textBox4.Text = null;
            textBox5.Text = null;
            textBox6.Text = null;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            int code = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            ex = tblextradb.Find(code);
            DialogResult r = MessageBox.Show("אתה בטוח שברצונך למחוק תוספת זו ?", "אישור מחיקה", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (r == DialogResult.Yes)
            {
                ClearText();
                ex.Status = false;
                tblextradb.UpdateRow(ex);
            }

            dataGridView1.DataSource = tblextradb.GetList().Where(x => x.Status == true).Select(x => new { קוד = x.Code, תאור = x.Descrption, מחיר = x.Extraprice }).OrderBy(f => f.קוד).ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            flageupdate = true;
            flageAdd = false;           
            groupBox2.Enabled = true;
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int code = Convert.ToInt32( dataGridView1.SelectedRows[0].Cells[0].Value);
                ex = tblextradb.Find(code);
                Fill(ex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (frmo != null)
            {
                ex= tblextradb.Find(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
                frmo.Ex.Extracode = ex.Code;
            }
            Close();
        }   
        private void button8_Click(object sender, EventArgs e)
        {
           
            Close();
        }

        private void button9_Click(object sender, EventArgs e)
        { if (radioButton1.Checked == true)
            
                dataGridView1.DataSource = tblextradb.GetList().Where(x => x.ToString() == (comboBox1.Text)).Select(x => new { קוד = x.Code, תאור = x.Descrption, מחיר = x.Extraprice }).OrderBy(f => f.קוד).ToList();

                if (radioButton2.Checked == true)
                
                    dataGridView1.DataSource = tblextradb.GetList().Where(x => x.Descrption.ToString() == (comboBox2.Text)).Select(x => new { קוד = x.Code, תאור = x.Descrption, מחיר = x.Extraprice }).OrderBy(f => f.קוד).ToList();

                    if (radioButton3.Checked == true)
                        dataGridView1.DataSource = tblextradb.GetList().Where(x => x.Extraprice. ToString() == (comboBox3.Text)).Select(x => new { קוד = x.Code, תאור = x.Descrption, מחיר = x.Extraprice }).OrderBy(f => f.קוד).ToList();
                
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = true;
            comboBox2.Enabled = false;
            comboBox3.Enabled = false;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            comboBox3.Enabled = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled =false;
            comboBox2.Enabled = true;
            comboBox3.Enabled = false;
        }   
    }
 }





