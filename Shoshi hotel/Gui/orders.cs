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
using Shoshi_hotel.Gui;
using Shoshi_hotel.BLL;
using Shoshi_hotel.Properties;


namespace Shoshi_hotel.Gui
{
    public partial class orders : Form
    {
        roomforreservationdb rtbl;
        roomforreservation room;
        bool flageOK = true;
        bool flageAdd = true;
        bool flageupdate = false;
        ordersdb tblor;
        Bll.orders ex;
        public Bll.rooms ro;
        public List<Bll.rooms> lstRoomsFor;
        int a;
        public Bll.orders Ex { get => ex; set => ex = value; }
        ordersdb tblors = new ordersdb();
        string o1;
        public orders(string pel)
        {
            InitializeComponent();
            Ex = new Bll.orders();
            room = new roomforreservation();
            rtbl = new roomforreservationdb();
            tblor = new ordersdb();
            o1 = pel;
            dataGridView1.ForeColor = Color.DeepPink;
            dataGridView1.DataSource = tblor.GetList().Select(x => new { מזמין = x.Inviting, תאריך_כניסה = x.Enterydate, תאריך_יציאה = x.Releasedate, סוג_פנסיון = x.Typeofpension, מספר_מיטה = x.Numberofbeds, מספר_הזמנה = x.Ordernumber, מחיר_סופי = x.Totalorder, הנחה = x.Discount, מספר_כרטיס = x.Ticketnumber, תוקף = x.Validity, שלוש_ספרות = x.Threedigits, תעודת_זהות = x.IDnumber1, קוד_כרטיס = x.Cardcode, תשלומים = x.Payments, קוד_תוספות = x.Extracode }).ToList();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox1.Enabled = true;
            button5.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ordering o = new ordering();
            o.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (textBox4.Text != "" && label16.Text != "" && (Validation.IsNum(textBox4.Text)) && (Validation.IsNum(textBox5.Text)))
            {
                pension p = new pension(this);
                p.ShowDialog();
                button3.Enabled = true;
                button8.Enabled = true;
                textBox3.Text = ex.Thispension().Description;
                if (label5.Text == "זוגי")
                    textBox13.Text = (ex.Thispension().Doublecost).ToString();
                if (label5.Text == "משפחתי")
                    textBox13.Text = (ex.Thispension().Familycost).ToString();
                a = ex.Thispension().Code;
            }
            else
            {
                MessageBox.Show("הקש נתונים הגיוניים ובחר שוב את הפנסיון הרצוי");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (label5.Text == "משפחתי" && textBox4.Text == 0.ToString())
            {
                MessageBox.Show("לא הקשת מספר מיטות");
            }
            else
            {
                if (dateTimePicker2.Value.Year % 4 == 0)
                {
                    if (dateTimePicker2.Value.Day > dateTimePicker1.Value.Day)
                    {
                        label16.Text = ((dateTimePicker2.Value.Day) - (dateTimePicker1.Value.Day)).ToString();
                    }
                    if (dateTimePicker1.Value.Day > dateTimePicker2.Value.Day)
                    {
                        if (dateTimePicker1.Value.Month == 01 || dateTimePicker1.Value.Month == 03 || dateTimePicker1.Value.Month == 05 || dateTimePicker1.Value.Month == 07 || dateTimePicker1.Value.Month == 08 || dateTimePicker1.Value.Month == 10 || dateTimePicker1.Value.Month == 12)
                            label16.Text = (((dateTimePicker2.Value.Day) + 31) - (dateTimePicker1.Value.Day)).ToString();
                        if (dateTimePicker1.Value.Month == 04 || dateTimePicker1.Value.Month == 06 || dateTimePicker1.Value.Month == 09 || dateTimePicker1.Value.Month == 11)
                        {
                            label16.Text = (((dateTimePicker2.Value.Day) + 30) - (dateTimePicker1.Value.Day)).ToString();
                        }
                        if (dateTimePicker1.Value.Month == 02)
                        {
                            label16.Text = (((dateTimePicker2.Value.Day) + 29) - (dateTimePicker1.Value.Day)).ToString();
                        }
                    }


                }
                else
                {

                    if (dateTimePicker2.Value.Day > dateTimePicker1.Value.Day)
                    {
                        label16.Text = ((dateTimePicker2.Value.Day) - (dateTimePicker1.Value.Day)).ToString();
                    }
                    if (dateTimePicker1.Value.Day > dateTimePicker2.Value.Day)
                    {
                        if (dateTimePicker1.Value.Month == 01 || dateTimePicker1.Value.Month == 03 || dateTimePicker1.Value.Month == 05 || dateTimePicker1.Value.Month == 07 || dateTimePicker1.Value.Month == 08 || dateTimePicker1.Value.Month == 10 || dateTimePicker1.Value.Month == 12)
                            label16.Text = (((dateTimePicker2.Value.Day) + 31) - (dateTimePicker1.Value.Day)).ToString();
                        if (dateTimePicker1.Value.Month == 04 || dateTimePicker1.Value.Month == 06 || dateTimePicker1.Value.Month == 09 || dateTimePicker1.Value.Month == 11)
                        {
                            label16.Text = (((dateTimePicker2.Value.Day) + 30) - (dateTimePicker1.Value.Day)).ToString();
                        }
                        if (dateTimePicker1.Value.Month == 02)
                        {
                            label16.Text = (((dateTimePicker2.Value.Day) + 28) - (dateTimePicker1.Value.Day)).ToString();
                        }
                    }

                }

                if (lstRoomsFor == null)
                    lstRoomsFor = new List<Bll.rooms>();
                //לבדוק אם התאריכים שבחר חוקיים
                //הוספת התאריכים לעצם ההזמנה
                ex.Enterydate = dateTimePicker1.Value;
                ex.Releasedate = dateTimePicker2.Value;
                ex.Numberofbeds = Convert.ToInt32(textBox4.Text);
                if (textBox4.Text != "" && textBox5.Text != "" && label16.Text != "" && label5.Text != "")
                {
                    button4.Enabled = true;
                }
                rooms r = new rooms(this, l);
                r.ShowDialog();
                dataGridView1.DataSource = lstRoomsFor.Select(x => new { קומה = x.Floor, מספר_מיטות = x.Numberbed, מספר_חדר = x.Numberoom, קוד_מחיר = x.Pricecode }).ToList();

            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.MinDate = dateTimePicker1.Value;
        }

        private void Fill(Bll.orders e)
        {
            textBox4.Text = Convert.ToString(e.Inviting);
            textBox3.Text = Convert.ToString(e.Typeofpension);
            textBox4.Text = (e.Numberofbeds).ToString();
            textBox5.Text = Convert.ToString(e.Ordernumber);
            textBox5.Text = Convert.ToString(e.Totalorder);
            textBox6.Text = Convert.ToString(e.Discount);
            maskedTextBox1.Text = Convert.ToString(e.Ticketnumber);
            maskedTextBox2.Text = Convert.ToString(e.Threedigits);
            maskedTextBox3.Text = Convert.ToString(e.IDnumber1);
            maskedTextBox4.Text = Convert.ToString(e.Cardcode);
            numericUpDown1.Text = Convert.ToString(e.Payments);
            textBox11.Text = Convert.ToString(e.Extracode);
        }
        public void ClearText()
        {
            textBox3.Text = null;
            textBox4.Text = null;
            textBox5.Text = null;
            textBox5.Text = null;
            textBox6.Text = null;
            maskedTextBox1.Text = null;
            maskedTextBox2.Text = null;
            maskedTextBox3.Text = null;
            maskedTextBox4.Text = null;
            numericUpDown1.Text = null;
            textBox11.Text = null;
        }

        private void NotPossible()
        {
            flageOK = false;
            flageupdate = false;
            flageAdd = false;
        }


        private bool CreateFieled(Bll.orders c)
        {
            flageOK = true;
            c.Ordernumber = tblors.GetNextKey();
            c.Inviting = o1;
            errorProvider1.Clear();

            {
                try
                {
                    if (textBox3.Text == "")
                        throw new Exception("שדה חובה");
                    else
                        c.Typeofpension = a;
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
                        c.Numberofbeds = Convert.ToInt32(textBox4.Text);
                }
                catch (Exception ex)
                {
                    errorProvider1.SetError(textBox4, ex.Message);
                    flageOK = false;
                }
                try
                {
                    if (textBox6.Text == "")
                        throw new Exception("שדה חובה");
                    else
                        c.Totalorder = Convert.ToInt32(textBox6.Text);
                }
                catch (Exception ex)
                {
                    errorProvider1.SetError(textBox6, ex.Message);
                    flageOK = false;
                }

                try
                {
                    if (maskedTextBox1.Text == "    -    -    -")
                        throw new Exception("שדה חובה");
                    else
                        c.Ticketnumber = maskedTextBox1.Text;
                }
                catch (Exception ex)
                {
                    errorProvider1.SetError(maskedTextBox1, ex.Message);
                    flageOK = false;
                }
                try
                {
                    if (maskedTextBox2.Text == "  /")
                        throw new Exception("שדה חובה");
                    else
                        c.Validity = maskedTextBox2.Text;
                }
                catch (Exception ex)
                {
                    if (ex.Message == " המחרוזת לא זוהתה כ- DateTime חוקי.")
                        errorProvider1.SetError(maskedTextBox2, "לא תקין");
                    else
                        errorProvider1.SetError(maskedTextBox2, ex.Message);
                    flageOK = false;
                }
                try
                {
                    if (maskedTextBox3.Text == "")
                        throw new Exception("שדה חובה");
                    else
                        c.Threedigits = maskedTextBox3.Text;
                }
                catch (Exception ex)
                {
                    errorProvider1.SetError(maskedTextBox3, ex.Message);
                    flageOK = false;
                }
                try
                {
                    if (textBox11.Text == "")
                        throw new Exception("שדה חובה");
                    else
                        c.Cardcode = textBox11.Text;
                }
                catch (Exception ex)
                {
                    errorProvider1.SetError(textBox11, ex.Message);
                    flageOK = false;
                }
                try
                {
                    if (maskedTextBox4.Text == "")
                        throw new Exception("שדה חובה");
                    else
                        c.IDnumber1 = maskedTextBox4.Text;
                }
                catch (Exception ex)
                {
                    errorProvider1.SetError(maskedTextBox4, ex.Message);
                    flageOK = false;
                }
                try
                {
                    if (maskedTextBox1.Text == "")
                        throw new Exception("שדה חובה");
                    else
                        c.Cardcode = maskedTextBox1.Text;
                }
                catch (Exception ex)
                {
                    errorProvider1.SetError(maskedTextBox1, ex.Message);
                    flageOK = false;
                }
                try
                {
                    if (textBox5.Text == "")
                        throw new Exception("שדה חובה");
                    else
                        c.Discount = Convert.ToInt32(textBox5.Text);
                }
                catch (Exception ex)
                {
                    errorProvider1.SetError(textBox5, ex.Message);
                    flageOK = false;
                }

                try
                {
                    if (numericUpDown1.Value.ToString() == "")
                        throw new Exception("שדה חובה");
                    else
                        c.Payments = Convert.ToInt32(numericUpDown1.Value);
                }
                catch (Exception ex)
                {
                    errorProvider1.SetError(numericUpDown1, ex.Message);
                    flageOK = false;
                }

                return flageOK;

            }
        }
        List<string> l = new List<string>();
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (!Validation.IsNum(textBox4.Text))
                errorProvider1.SetError(textBox4, " הקש מספרים בלבד");
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (!Validation.IsNum(textBox5.Text))
                errorProvider1.SetError(textBox5, " הקש מספרים בלבד");
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (!Validation.IsNum(maskedTextBox1.Text))
                errorProvider1.SetError(maskedTextBox1, " הקש מספרים בלבד");
        }
        private void textBox8_Leave(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (!Validation.IsNum(maskedTextBox2.Text))
                errorProvider1.SetError(maskedTextBox2, " הקש מספרים בלבד");
        }
        private void textBox9_Leave(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (!Validation.IsNum(maskedTextBox3.Text))
                errorProvider1.SetError(maskedTextBox3, " הקש מספרים בלבד");
        }
        private void textBox10_Leave(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (!Validation.IsNum(maskedTextBox4.Text))
                errorProvider1.SetError(maskedTextBox4, " הקש מספרים בלבד");
        }


        private void button16_Click(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
            flageAdd = true;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            flageupdate = true;
            flageAdd = false;

            if (dataGridView1.SelectedRows.Count > 0)
            {
                int code = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                Ex = tblor.Find(code);
                Fill(Ex);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            flageAdd = true;
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int code = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                Ex = tblor.Find(code);
                DialogResult r = MessageBox.Show("אתה בטוח שברצונך למחוק לקוח זה ?", "אישור מחיקה", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (r == DialogResult.Yes)
                {

                    ClearText();

                    String st = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    tblor.DeleteRow(code);

                }

            }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = tblor.GetList().Select(x => new { מזמין = x.Inviting, תאריך_כניסה = x.Enterydate, תאריך_יציאה = x.Releasedate, סוג_פנסיון = x.Typeofpension, מספר_מיטה = x.Numberofbeds, מספר_הזמנה = x.Ordernumber, מחיר_סופי = x.Totalorder, הנחה = x.Discount, מספר_כרטיס = x.Ticketnumber, תוקף = x.Validity, שלוש_ספרות = x.Threedigits, תעודת_זהות = x.IDnumber1, קוד_כרטיס = x.Cardcode, תשלומים = x.Payments,  קוד_תוספות = x.Extracode }).ToList();

        }

        private void button13_Click(object sender, EventArgs e)
        {
            button5.Visible = true;
            button13.Visible = false;
            if (flageAdd)
            {
                 if (maskedTextBox2.Text == "  /")
                {
                    MessageBox.Show(" שדה חובה");
                    button13.Visible = true;
                }
                else
                {
                    if (Convert.ToInt32(maskedTextBox2.Text.Substring(0, 2)) > 12 || Convert.ToInt32(maskedTextBox2.Text.Substring(0, 2)) < 1)
                    {
                        MessageBox.Show(" החודש בתוקף לא תקין");
                        button13.Visible = true;
                    }
                    else
                    {
                        if (Convert.ToInt32(maskedTextBox2.Text.Substring(3, 4)) < 2023)
                        {
                            MessageBox.Show(" השנה בתוקף אינה תקינה  ");
                            button13.Visible = true;
                        }
                        else
                        {

                            if (!Validation.CheckId(maskedTextBox4.Text))
                            {
                                MessageBox.Show("תעודת זהות לא תקינה");
                                button13.Visible = true;
                            }
                            else
                            {
                                if (CreateFieled(Ex))
                                {
                                    tblor.AddNew(Ex);
                                    NotPossible();
                                    MessageBox.Show("ההזמנה נוספה בהצלחה");
                                    foreach (string item in l)
                                    {
                                        roomforreservation rf = new roomforreservation();
                                        rf.Roomnumber = item;
                                        rf.Ordernomber = Ex.Ordernumber;
                                        rtbl.AddNew(rf);
                                        dataGridView1.DataSource = tblor.GetList().Select(ex => new { מזמין = ex.Inviting, תאריך_כניסה = ex.Enterydate, תאריך_יציאה = ex.Releasedate, סוג_פנסיון = ex.Typeofpension, מספר_מיטה = ex.Numberofbeds, מספר_הזמנה = ex.Ordernumber, מחיר_סופי = ex.Totalorder, הנחה = ex.Discount, מספר_כרטיס = ex.Ticketnumber, תוקף = ex.Validity, שלוש_ספרות = ex.Threedigits, תעודת_זהות = ex.IDnumber1, קוד_כרטיס = ex.Cardcode, תשלומים = ex.Payments, קוד_תוספות = ex.Extracode }).ToList();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("בדוק האם כל הנתונים נכונים");
                                    button13.Visible = true;
                                }
                                if (flageupdate)
                                {
                                    if (CreateFieled(Ex))
                                    {
                                        DialogResult r = MessageBox.Show("האם לעדכן לקוח זה ? ", "אישור עידכון", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                                        if (r == DialogResult.Yes)
                                        {
                                            tblor.UpdateRow(Ex);
                                            NotPossible();
                                        }
                                        else
                                        {
                                            button13.Visible = true;
                                        }
                                        foreach (string item in l)
                                        {
                                            roomforreservation rf = new roomforreservation();
                                            rf.Roomnumber = item;
                                            rf.Ordernomber = Ex.Ordernumber;
                                            rtbl.AddNew(rf);
                                        }
                                        dataGridView1.DataSource = tblor.GetList().Select(ex => new { מזמין = ex.Inviting, תאריך_כניסה = ex.Enterydate, תאריך_יציאה = ex.Releasedate, סוג_פנסיון = ex.Typeofpension, מספר_מיטה = ex.Numberofbeds, מספר_הזמנה = ex.Ordernumber, מחיר_סופי = ex.Totalorder, הנחה = ex.Discount, מספר_כרטיס = ex.Ticketnumber, תוקף = ex.Validity, שלוש_ספרות = ex.Threedigits, תעודת_זהות = ex.IDnumber1, קוד_כרטיס = ex.Cardcode, תשלומים = ex.Payments, קוד_תוספות = ex.Extracode }).ToList();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            button6.Visible = false;
            button13.Visible = true;
            button8.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button7.Visible = false;
            button15.Visible = false;
            button9.Visible = false;
            dateTimePicker1.Visible = false;
            dateTimePicker2.Visible = false;
            label3.Visible = false;
            label2.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label16.Visible = false;
            label15.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox13.Visible = false;
            textBox5.Visible = false;
            textBox12.Visible = false;
            textBox6.Visible = false;
            groupBox1.Visible = true;
            groupBox1.Enabled = true;
            button13.Visible = true;
            button5.Visible = true;
            button5.Enabled = true;          
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount == 0)
            {
                MessageBox.Show("אינך בחרת חדרים");
            }
            else
            {
                ex.Numberofbeds = Convert.ToInt32(textBox4.Text);
                extras p = new extras(this);
                p.ShowDialog();
                button7.Enabled = true;
                textBox12.Text = ex.Thisextras().Extraprice.ToString();
                if (textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox12.Text != "" && textBox13.Text != "")
                {
                    button7.Enabled = true;
                }

            }
        }

        int x;
        
        private void button7_Click(object sender, EventArgs e)
        {
           x = Convert.ToInt32(textBox12.Text) + Convert.ToInt32(textBox13.Text) - Convert.ToInt32(textBox5.Text);
            textBox6.Text = (x + Convert.ToInt32(label16.Text)*Convert.ToInt32(textBox1.Text)).ToString();
            if (textBox3.Text!=""&& textBox4.Text != ""&& textBox12.Text != "")
            {
                button6.Visible =true;
            }
        }
     //חישוב כמה לילות הוזמנו ע"י חישוב בין 2 התאריכים הנתונים  
        private void button9_Click_1(object sender, EventArgs e)
        {
            label5.Text = "זוגי";
            button2.Enabled = true;
            textBox4.Text = "2";
            if (dateTimePicker2.Value.Year % 4 == 0)
            {


                if (dateTimePicker2.Value.Day > dateTimePicker1.Value.Day)
                {
                    label16.Text = ((dateTimePicker2.Value.Day) - (dateTimePicker1.Value.Day)).ToString();
                }
                if (dateTimePicker1.Value.Day > dateTimePicker2.Value.Day)
                {
                    if (dateTimePicker1.Value.Month == 01 || dateTimePicker1.Value.Month == 03 || dateTimePicker1.Value.Month == 05 || dateTimePicker1.Value.Month == 07 || dateTimePicker1.Value.Month == 08 || dateTimePicker1.Value.Month == 10 || dateTimePicker1.Value.Month == 12)
                        label16.Text = (((dateTimePicker2.Value.Day) + 31) - (dateTimePicker1.Value.Day)).ToString();
                    if (dateTimePicker1.Value.Month == 04 || dateTimePicker1.Value.Month == 06 || dateTimePicker1.Value.Month == 09 || dateTimePicker1.Value.Month == 11)
                    {
                        label16.Text = (((dateTimePicker2.Value.Day) + 30) - (dateTimePicker1.Value.Day)).ToString();
                    }
                    if (dateTimePicker1.Value.Month == 02)
                    {
                        label16.Text = (((dateTimePicker2.Value.Day) + 29) - (dateTimePicker1.Value.Day)).ToString();
                    }
                }


            }
            else
            {

                if (dateTimePicker2.Value.Day > dateTimePicker1.Value.Day)
                {
                    label16.Text = ((dateTimePicker2.Value.Day) - (dateTimePicker1.Value.Day)).ToString();
                }
                if (dateTimePicker1.Value.Day > dateTimePicker2.Value.Day)
                {
                    if (dateTimePicker1.Value.Month == 01 || dateTimePicker1.Value.Month == 03 || dateTimePicker1.Value.Month == 05 || dateTimePicker1.Value.Month == 07 || dateTimePicker1.Value.Month == 08 || dateTimePicker1.Value.Month == 10 || dateTimePicker1.Value.Month == 12)
                        label16.Text = (((dateTimePicker2.Value.Day) + 31) - (dateTimePicker1.Value.Day)).ToString();
                    if (dateTimePicker1.Value.Month == 04 || dateTimePicker1.Value.Month == 06 || dateTimePicker1.Value.Month == 09 || dateTimePicker1.Value.Month == 11)
                    {
                        label16.Text = (((dateTimePicker2.Value.Day) + 30) - (dateTimePicker1.Value.Day)).ToString();
                    }
                    if (dateTimePicker1.Value.Month == 02)
                    {
                        label16.Text = (((dateTimePicker2.Value.Day) + 28) - (dateTimePicker1.Value.Day)).ToString();
                    }
                }

            }

        }

        private void button15_Click_1(object sender, EventArgs e)
        {
            label5.Text = "משפחתי";
            button2.Enabled = true;
            textBox4.Text = "0";
            if (dateTimePicker2.Value.Year % 4 == 0)
            {


                if (dateTimePicker2.Value.Day > dateTimePicker1.Value.Day)
                {
                    label16.Text = ((dateTimePicker2.Value.Day) - (dateTimePicker1.Value.Day)).ToString();
                }
                if (dateTimePicker1.Value.Day > dateTimePicker2.Value.Day)
                {
                    if (dateTimePicker1.Value.Month == 01 || dateTimePicker1.Value.Month == 03 || dateTimePicker1.Value.Month == 05 || dateTimePicker1.Value.Month == 07 || dateTimePicker1.Value.Month == 08 || dateTimePicker1.Value.Month == 10 || dateTimePicker1.Value.Month == 12)
                        label16.Text = (((dateTimePicker2.Value.Day) + 31) - (dateTimePicker1.Value.Day)).ToString();
                    if (dateTimePicker1.Value.Month == 04 || dateTimePicker1.Value.Month == 06 || dateTimePicker1.Value.Month == 09 || dateTimePicker1.Value.Month == 11)
                    {
                        label16.Text = (((dateTimePicker2.Value.Day) + 30) - (dateTimePicker1.Value.Day)).ToString();
                    }
                    if (dateTimePicker1.Value.Month == 02)
                    {
                        label16.Text = (((dateTimePicker2.Value.Day) + 29) - (dateTimePicker1.Value.Day)).ToString();
                    }
                }


            }
            else
            {

                if (dateTimePicker2.Value.Day > dateTimePicker1.Value.Day)
                {
                    label16.Text = ((dateTimePicker2.Value.Day) - (dateTimePicker1.Value.Day)).ToString();
                }
                if (dateTimePicker1.Value.Day > dateTimePicker2.Value.Day)
                {
                    if (dateTimePicker1.Value.Month == 01 || dateTimePicker1.Value.Month == 03 || dateTimePicker1.Value.Month == 05 || dateTimePicker1.Value.Month == 07 || dateTimePicker1.Value.Month == 08 || dateTimePicker1.Value.Month == 10 || dateTimePicker1.Value.Month == 12)
                        label16.Text = (((dateTimePicker2.Value.Day) + 31) - (dateTimePicker1.Value.Day)).ToString();
                    if (dateTimePicker1.Value.Month == 04 || dateTimePicker1.Value.Month == 06 || dateTimePicker1.Value.Month == 09 || dateTimePicker1.Value.Month == 11)
                    {
                        label16.Text = (((dateTimePicker2.Value.Day) + 30) - (dateTimePicker1.Value.Day)).ToString();
                    }
                    if (dateTimePicker1.Value.Month == 02)
                    {
                        label16.Text = (((dateTimePicker2.Value.Day) + 28) - (dateTimePicker1.Value.Day)).ToString();
                    }
                }

            }
        }

       
        private void button8_Click_1(object sender, EventArgs e)
        {
            Priceperroom o = new Priceperroom();
            o.Show();
        }

        private void maskedTextBox1_Leave(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (!Validation.IsNum(maskedTextBox1.Text))
                errorProvider1.SetError(maskedTextBox1, " הקש מספרים בלבד");
        }

        private void maskedTextBox2_Leave(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (!Validation.IsNum(maskedTextBox2.Text))
                errorProvider1.SetError(maskedTextBox2, " הקש מספרים בלבד");
        }

        private void maskedTextBox3_Leave(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (!Validation.IsNum(maskedTextBox3.Text))
                errorProvider1.SetError(maskedTextBox3, " הקש מספרים בלבד");
        }

        private void maskedTextBox4_Leave(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (!Validation.IsNum(maskedTextBox4.Text))
                errorProvider1.SetError(maskedTextBox4, " הקש מספרים בלבד");
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            roomforreservationdb rtbl = new roomforreservationdb();
            int f = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[5].Value);
            dataGridView2.DataSource = rtbl.GetList().Where(x => x.Thisorders().Ordernumber == f).Select(x =>new { מספר_הזמנה=x.Ordernomber, מספר_חדר=x.Roomnumber }).ToList();
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (!Validation.IsNum(textBox4.Text))
                errorProvider1.SetError(textBox4, " הקש מספרים בלבד");
        }

        private void textBox5_Leave_1(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (!Validation.IsNum(textBox5.Text))
                errorProvider1.SetError(textBox5, " הקש מספרים בלבד");
        }

        private void maskedTextBox1_Leave_1(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (!Validation.IsNum(maskedTextBox1.Text))
                errorProvider1.SetError(maskedTextBox1, " הקש מספרים בלבד");
        }

        private void maskedTextBox2_Leave_1(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (Validation.IsNum(maskedTextBox2.Text))
                errorProvider1.SetError(maskedTextBox2, " הקש מספרים בלבד");
            
           
        }

        private void maskedTextBox3_Leave_1(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (!Validation.IsNum(maskedTextBox3.Text))
                errorProvider1.SetError(maskedTextBox3, " הקש מספרים בלבד");
        }

        private void maskedTextBox4_Leave_1(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (!Validation.CheckId(maskedTextBox4.Text))
                errorProvider1.SetError(maskedTextBox4, " הקש תעודת זהות תקינה");
        }

        private void textBox11_Leave(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (!Validation.IsNum(textBox11.Text))
                errorProvider1.SetError(textBox11, " הקש מספרים בלבד");
        }

    }
    }

