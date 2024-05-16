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
using Shoshi_hotel.Gui;
using Shoshi_hotel.Dal;
using Shoshi_hotel.Properties;



namespace Shoshi_hotel.Gui
{
    public partial class pension : Form
    {
       bool flageupdate =false;
       bool flageAdd = true;
        pensiondb tblpen;
        Bll.pension pen;
        Bll.pension pe;
        bool flageOK = false;
        orders frmo=null;//עצם מסוג טופס
        public pension()
        {
            InitializeComponent();
            pe = new Bll.pension();
            pen = new Bll.pension();
            tblpen = new pensiondb();
            comboBox1.DataSource = tblpen.GetList().FindAll(x => x.Status).Select(X => X.Code).ToList(); ;
            comboBox2.DataSource = tblpen.GetList().FindAll(x => x.Status).Select(X => X.Description).ToList();
            comboBox3.DataSource = tblpen.GetList().FindAll(x => x.Status).Select(X => X.Doublecost).ToList();
            comboBox4.DataSource = tblpen.GetList().FindAll(x => x.Status).Select(X => X.Familycost).ToList();
            dataGridView1.ForeColor = Color.LightBlue;
            dataGridView1.DataSource = tblpen.GetList().Where(x=> x.Status==true).Select(x => new { קוד = x.Code, תאור = x.Description, מחיר = x.Doublecost, מחיר_משפחתי = x.Familycost }).OrderBy(f => f.קוד).ToList();
        }

        public pension(orders f) : this()
        {
            frmo = f;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (frmo != null)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                { pe = tblpen.Find(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
                  frmo.Ex.Typeofpension = pe.Code;
                } 
            }
            Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {if(textBox9.Text=="shosh")
            {
                groupBox2.Visible = true;
                button3.Visible = true;
                button6.Visible = true;
                button7.Visible = true;
                button8.Visible = true;
                button2.Visible = false;
                button9.Visible = true;

            }
            else
                MessageBox.Show("קוד לא נכון");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            groupBox3.Visible = true;
            
        }
        public void ClearText()
        {
            textBox7.Text = null;
            textBox5.Text = null;
            textBox6.Text = null;
            textBox8.Text = null;
        }

        private void Fill(Bll.pension e)
        {
            textBox5.Text = Convert.ToString(e.Code);
            textBox6.Text = e.Description;
            textBox7.Text = Convert.ToString(e.Doublecost);
            textBox8.Text = Convert.ToString(e.Familycost);
        }
       
        private void NotPossible()
        {
            flageOK = false;
            flageupdate = false;
            flageAdd = true;
        }
        private bool CreateFieled(Bll.pension c)
        {
            flageOK = true;
            errorProvider1.Clear();
            try
            {
                if (textBox5.Text == "")
                    throw new Exception("שדה חובה");
                else
                    c.Code = Convert.ToInt32(textBox5.Text);
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
                if (textBox6.Text.Length == 1)
                {
                    throw new Exception("לא תקין");
                }
                else
                    c.Description = Convert.ToString(textBox6.Text);
            }
            catch (Exception ex)
            {
                errorProvider1.SetError(textBox6, ex.Message);
                flageOK = false;
            }

            try
            {
                if (textBox7.Text == "")
                    throw new Exception("שדה חובה");
                else
                    c.Doublecost = Convert.ToInt32(textBox7.Text);
            }
            catch (Exception ex)
            {
                errorProvider1.SetError(textBox7, ex.Message);
                flageOK = false;
            }
 
            try
            {
                if (textBox8.Text == "")
                    throw new Exception("שדה חובה");
                else
                    c.Familycost = Convert.ToInt32(textBox8.Text);
            }
            catch (Exception ex)
            {
                errorProvider1.SetError(textBox8, ex.Message);
                flageOK = false;
            }
            c.Status = true;
            return flageOK;
        }
       
       
       

        private void button9_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            flageAdd = true;
            textBox7.Text = null;
            textBox6.Text = null;
            textBox8.Text = null;
            textBox5.Text = tblpen.GetNextKey().ToString();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            int code = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            pe = tblpen.Find(code);
            DialogResult r = MessageBox.Show("אתה בטוח שברצונך למחוק פנסיון זה ?", "אישור מחיקה", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (r == DialogResult.Yes)
            {

                ClearText();
                pe.Status = false;
                tblpen.UpdateRow(pe);
            }
                dataGridView1.DataSource = tblpen.GetList().Where(x => x.Status == true).Select(x => new { קוד = x.Code, תאור = x.Description, מחיר = x.Doublecost, מחיר_משפחתי = x.Familycost }).OrderBy(f => f.קוד).ToList();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            flageupdate = true;
            flageAdd = false;
            groupBox2.Enabled = true;
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int code = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                pe = tblpen.Find(code);
                Fill(pe);


            }
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            int g = tblpen.GetList().Count(x => x.Description == textBox6.Text);
            
            {
                if (flageAdd)
                {
                    if (g == 1)
                    {

                        MessageBox.Show("פנסיון קיים");
                    }
                    else
                    {


                        if (CreateFieled(pe))
                        {
                            DialogResult r = MessageBox.Show("האם להוסיף פנסיון זה ? ", "אישור הוספה", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                            if (r == DialogResult.Yes)
                            {
                                tblpen.AddNew(pe);
                                NotPossible();
                            }
                        }
                    }
                }
                else
                    if (flageupdate)
                {
                    if (CreateFieled(pe))
                    {
                        DialogResult r = MessageBox.Show("האם לעדכן פנסיון זה ? ", "אישור עידכון", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (r == DialogResult.Yes)
                        {
                            tblpen.UpdateRow(pe);
                            NotPossible();
                        }
                    }
                }
                dataGridView1.DataSource = tblpen.GetList().Where(x => x.Status == true).Select(x => new { קוד = x.Code, תאור = x.Description, מחיר = x.Doublecost, מחיר_משפחתי = x.Familycost }).OrderBy(f => f.קוד).ToList();
                comboBox1.DataSource = tblpen.GetList().FindAll(x => x.Status).Select(X => X.Code).ToList(); ;
                comboBox2.DataSource = tblpen.GetList().FindAll(x => x.Status).Select(X => X.Description).ToList();
                comboBox3.DataSource = tblpen.GetList().FindAll(x => x.Status).Select(X => X.Doublecost).ToList();
                comboBox4.DataSource = tblpen.GetList().FindAll(x => x.Status).Select(X => X.Familycost).ToList();
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (!Validation.IsNum(textBox5.Text))
                errorProvider1.SetError(textBox5, " הקש מספרים בלבד");
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (!Validation.IsHebrew(textBox6.Text))
                errorProvider1.SetError(textBox6, " הקש אותיות בעיברית בלבד");
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (!Validation.IsNum(textBox7.Text))
                errorProvider1.SetError(textBox7, " הקש מספרים בלבד");
        }

        private void textBox8_Leave(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (!Validation.IsNum(textBox8.Text))
                errorProvider1.SetError(textBox8, " הקש מספרים בלבד");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)

                dataGridView1.DataSource = tblpen.GetList().Where(x => x.ToString() == (comboBox1.Text)).Select(x => new { קוד = x.Code, תאור = x.Description,מחיר_זוגי = x.Doublecost,מחיר_משפחתי=x.Familycost }).OrderBy(f => f.קוד).ToList();

            if (radioButton2.Checked == true)

                dataGridView1.DataSource = tblpen.GetList().Where(x => x.Description.ToString() == (comboBox2.Text)).Select(x => new { קוד = x.Code, תאור = x.Description, מחיר_זוגי = x.Doublecost, מחיר_משפחתי = x.Familycost }).OrderBy(f => f.קוד).ToList();

            if (radioButton3.Checked == true)
                dataGridView1.DataSource = tblpen.GetList().Where(x => x.Doublecost.ToString() == (comboBox3.Text)).Select(x => new { קוד = x.Code, תאור = x.Description, מחיר_זוגי = x.Doublecost, מחיר_משפחתי = x.Familycost }).OrderBy(f => f.קוד).ToList();
          
            if (radioButton4.Checked == true)
                dataGridView1.DataSource = tblpen.GetList().Where(x => x.Familycost.ToString() == (comboBox4.Text)).Select(x => new { קוד = x.Code, תאור = x.Description, מחיר_זוגי = x.Doublecost, מחיר_משפחתי = x.Familycost }).OrderBy(f => f.קוד).ToList();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = true;
            comboBox2.Enabled = false;
            comboBox3.Enabled = false;
            comboBox4.Enabled = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = false;
            comboBox2.Enabled = true;
            comboBox3.Enabled = false;
            comboBox4.Enabled = false;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            comboBox3.Enabled = true;
            comboBox4.Enabled = false;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            comboBox3.Enabled = false;
            comboBox4.Enabled = true;
        }

        
    }
}
