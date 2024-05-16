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
using Shoshi_hotel.Properties;
using Shoshi_hotel.BLL;

namespace Shoshi_hotel.Gui
{
    public partial class rooms : Form
    {
       
      
        bool flageAdd = false;
        roomsdb tblrooms;
        Bll.rooms ro;
        Bll.rooms bo;
        bool flageupdate = false;
        bool flageOK = false;
        priceroomdb tblpri;
        roomforreservationdb tblRoomforInv;
        orders fo;
        int sum;
        priceroom p;
        public rooms(f f2)
        {
            InitializeComponent();
            if(f2 != null)
            {
                button1.Visible = false;
                button11.Visible = false;
                listBox1.Visible = false;
                label12.Visible = false;
                label13.Visible = false;
                label14.Visible = false;
                
            }

            tblrooms = new roomsdb();
            ro = new Bll.rooms();
            tblpri = new priceroomdb();
            comboBox2.DataSource = tblpri.GetList().FindAll(x => x.Status).ToList(); 
            comboBox7.DataSource = tblpri.GetList().FindAll(x => x.Status).ToList(); 
            comboBox3.DataSource = tblpri.GetList().ToList();
            tblRoomforInv = new roomforreservationdb();
            dataGridView1.ForeColor = Color.Purple;
            dataGridView1.DataSource = tblrooms.GetList().Where(x => x.Status == true).Select(x => new { מספר_חדר = x.Numberoom, מספר_מיטה = x.Numberbed, קומה = x.Floor, קוד_מחיר = x.Pricecode }).ToList();

        }
        DateTime enterI ;
        DateTime leaveI ;
        List<string> l = new List<string>();
        public rooms(orders fo, List<string> l1) :this(null)
        {
            int count = 0;
            if (fo.lstRoomsFor.Count!=0)
            {
                foreach (Bll.rooms item in fo.lstRoomsFor)
                {
                    listBox1.Items.Add( item.Numberoom.ToString() );
                    count += item.Numberbed;
                }
                label14.Text = count.ToString();
            }
            l = l1;
            this.fo = fo;
             enterI = fo.Ex.Enterydate;
             leaveI = fo.Ex.Releasedate;
            var lst = tblrooms.GetList().Select(x => new { מספר_חדר = x.Numberoom, מספר_מיטה = x.Numberbed, קומה = x.Floor, סטטוס = x.Status, קוד_מחיר = x.Pricecode, תפוסה = tblRoomforInv.GetList().Count(j => (x.Numberoom == Convert.ToInt32(j.Roomnumber))&& ((j.Thisorders().Enterydate < leaveI && j.Thisorders().Releasedate >= leaveI) || (j.Thisorders().Releasedate > enterI && j.Thisorders().Enterydate < enterI))) });// > Count(y=>y.Roomnumber==x.Numberoom)
            dataGridView1.DataSource = lst.ToList().Where(x => x.תפוסה == 0).ToList().Select(x => new { x.מספר_חדר, x.מספר_מיטה, x.קוד_מחיר, x.קומה ,x.סטטוס}).ToList();

          
        }

        private void NotPossible()
        {

        }

        List<Bll.rooms> lll = new List<Bll.rooms>();
        private void button1_Click(object sender, EventArgs e)
        { 
            
           // dataGridView2.DataSource = (tblrooms.GetList().Where(x => x.Pricecode == Convert.ToInt32(listBox1.SelectedItem)).Select({x => new { מספר_חדר = x. , מספר_מיטה = x.Numberbed, קומה = x.Floor, קוד_מחיר = x.Pricecode }).ToList();
            // button2.Visible = true;
            if (dataGridView1.RowCount == 0)
            {
                button10.Visible = true;
                MessageBox.Show("  בחר תאריך אחר " +
                    "בתאריך זה אין חדרים פנויים" );
            }
            else
            {
                button2.Visible = true;
                button10.Visible = false;
                if (fo != null)
                {

                    ro = tblrooms.Find(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
                    fo.lstRoomsFor.Add(ro);

                    label14.Text = (Convert.ToInt32(label14.Text) + Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[1].Value)).ToString();
                  //dataGridView1.SelectedRows[0].Cells[4].Value = false.ToString();
                    Bll.rooms rr = new Bll.rooms();
                    rr = tblrooms.Find(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                    rr.Status = false;
                    tblrooms.UpdateRow(rr);
                    lll.Add(rr);
                    listBox1.DataSource = null;
                    listBox1.DataSource = fo.lstRoomsFor;
                    if (dataGridView1.Rows.Count == 0)
                    {
                        return;
                    }

                }
                if (dataGridView1.DataSource == null)
                {
                    MessageBox.Show("נגמרו החדרים");
                }
                else
                {
                    int codePrice = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[2].Value);
                    p = tblpri.Find(codePrice);
                    sum += p.Pricemight;
                    l.Add(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());


                }
                var lst = tblrooms.GetList().Select(x => new { מספר_חדר = x.Numberoom, מספר_מיטה = x.Numberbed, קומה = x.Floor, סטטוס = x.Status, קוד_מחיר = x.Pricecode, תפוסה = tblRoomforInv.GetList().Count(j => (x.Numberoom == Convert.ToInt32(j.Roomnumber)) && ((j.Thisorders().Enterydate < leaveI && j.Thisorders().Releasedate >= leaveI) || (j.Thisorders().Releasedate > enterI && j.Thisorders().Enterydate < enterI))) });// > Count(y=>y.Roomnumber==x.Numberoom)
                dataGridView1.DataSource = lst.ToList().Where(x => x.תפוסה == 0).ToList().FindAll(r => r.סטטוס == true).Select(x => new { x.מספר_חדר, x.מספר_מיטה, x.קוד_מחיר, x.קומה }).ToList();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Priceperroom p = new Priceperroom();
            p.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            groupBox3.Visible = true;           
            button10.Visible =true;
            button2.Visible = false;
        }
        public void ClearText()
        {
            
            textBox6.Text = null;
            textBox7.Text = null;
            comboBox1.Text = null;
            comboBox2.Text = null;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox9.Text == "shosh")
            {
                groupBox2.Visible = true;
                groupBox1.Enabled = true;
                button5.Visible = true;
                button9.Enabled = true;
                button6.Visible = true;
                button7.Visible = true;
                button8.Visible = true;
                button1.Visible =false;
                button10.Visible = true;

            }
            else
                MessageBox.Show("קוד לא נכון");
           
        }

       
        private void Fill(Bll.rooms r)
        {
            textBox5.Text = Convert.ToString(r.Numberoom);
            textBox6.Text = Convert.ToString(r.Numberbed);
            textBox7.Text = Convert.ToString(r.Floor);
            comboBox1.SelectedItem = Convert.ToString(r.Status);
            comboBox2.SelectedItem = Convert.ToInt32(r.Pricecode);
        }
            private void button7_Click(object sender, EventArgs e)
        {
            flageupdate = true;
            flageAdd = false;
            groupBox2.Enabled = true;
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int code = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                ro = tblrooms.Find(code);
                Fill(ro);


            }
        }   

        private void textBox6_Leave(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (!Validation.IsNum(textBox6.Text))
                errorProvider1.SetError(textBox6, " הקש מספרים בלבד");
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (!Validation.IsNum(textBox7.Text))
                errorProvider1.SetError(textBox7, " הקש מספרים בלבד");
        }
        private bool CreateFieled(Bll.rooms r)
        {
            flageOK = true;
            errorProvider1.Clear();
            
                   r.Numberoom = Convert.ToInt32(textBox5.Text);
            
            try
            {
                if (textBox6.Text == "")
                    throw new Exception("שדה חובה");
                else
                    r.Numberbed = Convert.ToInt32(textBox6.Text);
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
                    r.Floor = (textBox7.Text);
            }
            catch (Exception ex)
            {
                errorProvider1.SetError(textBox7, ex.Message);
                flageOK = false;
            }
            if (comboBox1.Text=="פעיל")
            {
                r.Status = true;
            }
            if (comboBox1.Text == "לא פעיל")
            {
                r.Status = false;
            }
            r.Pricecode =Convert.ToInt32( comboBox2.Text);
          
            
            
            return flageOK;
        }


        private void button5_Click_1(object sender, EventArgs e)
        {
            flageAdd = true;
            groupBox2.Enabled = true;
            textBox5.Text = tblrooms.GetNextKey().ToString();
            ClearText();
            
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            
            if (flageAdd)
            {
               
                if (CreateFieled(ro))
                {
                    DialogResult r = MessageBox.Show("האם להוסיף חדר זה ? ", "אישור הוספה", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (r == DialogResult.Yes)
                    {
                        tblrooms.AddNew(ro);
                        NotPossible();
                    }
                }
            }
            else
                   if (flageupdate)
            {
                if (CreateFieled(ro))
                {
                    DialogResult r = MessageBox.Show("האם לעדכן חדר זה ? ", "אישור עידכון", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (r == DialogResult.Yes)
                    {
                        tblrooms.UpdateRow(ro);
                        NotPossible();
                    }
                }
            }
            dataGridView1.DataSource = tblrooms.GetList().Where(x => x.Status == true).Select(x => new { מספר_חדר = x.Numberoom, מספר_מיטות = x.Numberbed, קומה = x.Floor, קוד_מחיר = x.Pricecode }).OrderBy(f => f.מספר_חדר).ToList();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            flageAdd = true;
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int code = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                ro = tblrooms.Find(code);
                DialogResult r = MessageBox.Show("אתה בטוח שברצונך למחוק חדר זה ?", "אישור מחיקה", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (r == DialogResult.Yes)
                {
                    ClearText();
                   ro.Status = false;
                    tblrooms.UpdateRow(ro);

                }

            }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = tblrooms.GetList().Where(x => x.Status == true).Select(x => new { מספר_חדר = x.Numberoom, מספר_מיטות = x.Numberbed, קומה = x.Floor, קוד_מחיר = x.Pricecode }).OrderBy(f => f.מספר_חדר).ToList();
        }

        private void button7_Click_2(object sender, EventArgs e)
        {
            flageAdd = true;
            flageupdate = true;
            flageAdd = false;
            groupBox2.Enabled = true;
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int numberoom = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                ro = tblrooms.Find(numberoom);
                Fill(ro);


            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)

                dataGridView1.DataSource = tblrooms.GetList().Where(x => x.ToString() == (comboBox3.Text)).Select(x => new { מספר_חדר = x.Numberoom, מספר_מיטות = x.Numberbed, קומה = x.Floor ,קוד_מחיר=x.Pricecode}).OrderBy(f => f.מספר_חדר).ToList();

            if (radioButton2.Checked == true)

                dataGridView1.DataSource = tblrooms.GetList().Where(x => x.Numberbed.ToString() == (comboBox4.Text)).Select(x => new { מספר_חדר = x.Numberoom, מספר_מיטות = x.Numberbed, קומה = x.Floor, קוד_מחיר = x.Pricecode }).OrderBy(f => f.מספר_חדר).ToList();

            if (radioButton3.Checked == true)

                dataGridView1.DataSource = tblrooms.GetList().Where(x => x.Floor.ToString() == (comboBox5.Text)).Select(x => new { מספר_חדר = x.Numberoom, מספר_מיטות = x.Numberbed, קומה = x.Floor, קוד_מחיר = x.Pricecode }).OrderBy(f => f.מספר_חדר).ToList();

            if (radioButton4.Checked == true)

                dataGridView1.DataSource = tblrooms.GetList().Where(x => x.Status.ToString() == (comboBox6.Text)).Select(x => new { מספר_חדר = x.Numberoom, מספר_מיטות = x.Numberbed, קומה = x.Floor,  קוד_מחיר = x.Pricecode }).OrderBy(f => f.מספר_חדר).ToList();
            if (radioButton5.Checked == true)

                dataGridView1.DataSource = tblrooms.GetList().Where(x => x.Pricecode.ToString() == (comboBox7.Text)).Select(x => new { מספר_חדר = x.Numberoom, מספר_מיטות = x.Numberbed, קומה = x.Floor, סטטוס = x.Status, קוד_מחיר = x.Pricecode }).OrderBy(f => f.מספר_חדר).ToList();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            comboBox3.Enabled = true;
            comboBox4.Enabled = false;
            comboBox5.Enabled = false;
            comboBox6.Enabled = false;
            comboBox7.Enabled = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            comboBox3.Enabled = false;
            comboBox4.Enabled = true;
            comboBox5.Enabled = false;
            comboBox6.Enabled = false;
            comboBox7.Enabled = false;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            comboBox3.Enabled = false;
            comboBox4.Enabled = false;
            comboBox5.Enabled = true;
            comboBox6.Enabled = false;
            comboBox7.Enabled = false;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            comboBox3.Enabled = false;
            comboBox4.Enabled = false;
            comboBox5.Enabled = false;
            comboBox7.Enabled = false;
            comboBox6.Enabled = true;
        }



        private void button2_Click_1(object sender, EventArgs e)
        {
            Bll.rooms rr = new Bll.rooms();
            foreach (Bll.rooms item in fo.lstRoomsFor)
            {
                rr = tblrooms.Find(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                rr.Status = true;
                tblrooms.UpdateRow(rr);

            }
           
            foreach (Bll.rooms item in lll)
            {
                item.Status = true;
                tblrooms.UpdateRow(item);
            }
                bo = tblrooms.Find(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
                if (fo.Ex.Numberofbeds <= Convert.ToInt32(label14.Text))
                {
                    Close();

                    fo.textBox1.Text = sum.ToString();
                }
                else
                    MessageBox.Show("אין לך מספיק מיטות");

        }
 
        private void button12_Click(object sender, EventArgs e)
        {
            Priceperroom o = new Priceperroom();
            o.Show();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            comboBox3.Enabled = false;
            comboBox4.Enabled = false;
            comboBox5.Enabled = false;
            comboBox7.Enabled = true;
            comboBox6.Enabled = false;
        }

        

        private void button13_Click(object sender, EventArgs e)
        {
            DateTime d = enterI;
            for (int i = 0; i < 30; i++)
            {
                enterI.AddDays(i);
                leaveI.AddDays(i);
                var lst = tblrooms.GetList().Select(x => new { מספר_חדר = x.Numberoom, מספר_מיטה = x.Numberbed, קומה = x.Floor, סטטוס = x.Status, קוד_מחיר = x.Pricecode, תפוסה = tblRoomforInv.GetList().Count(j => (x.Numberoom == Convert.ToInt32(j.Roomnumber)) && ((j.Thisorders().Enterydate < leaveI && j.Thisorders().Releasedate >= leaveI) || (j.Thisorders().Releasedate > enterI && j.Thisorders().Enterydate < enterI))) });// > Count(y=>y.Roomnumber==x.Numberoom)
                dataGridView1.DataSource = lst.ToList().Where(x => x.תפוסה == 0).ToList().Select(x => new { x.מספר_חדר, x.מספר_מיטה, x.קוד_מחיר, x.קומה }).ToList();
                if (dataGridView1.Rows.Count > 0)
                    i = 30;
            }

        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count>0)
            {
                label14.Text = (((Convert.ToInt32(label14.Text) )-((Bll.rooms)listBox1.SelectedItems[0]).Numberbed).ToString());
                fo.lstRoomsFor.RemoveAt(listBox1.SelectedIndex); 
                listBox1.DataSource = null;
                listBox1.DataSource = fo.lstRoomsFor;
                Bll.rooms rr = new Bll.rooms();
                rr = tblrooms.Find(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                rr.Status = true;
                tblrooms.UpdateRow(rr);
                var lst = tblrooms.GetList().Select(x => new { מספר_חדר = x.Numberoom, מספר_מיטה = x.Numberbed, קומה = x.Floor, סטטוס = x.Status, קוד_מחיר = x.Pricecode, תפוסה = tblRoomforInv.GetList().Count(j => (x.Numberoom == Convert.ToInt32(j.Roomnumber)) && ((j.Thisorders().Enterydate < leaveI && j.Thisorders().Releasedate >= leaveI) || (j.Thisorders().Releasedate > enterI && j.Thisorders().Enterydate < enterI))) });// > Count(y=>y.Roomnumber==x.Numberoom)
                dataGridView1.DataSource = lst.ToList().Where(x => x.תפוסה == 0).ToList().FindAll(r => r.סטטוס == true).Select(x => new { x.מספר_חדר, x.מספר_מיטה, x.קוד_מחיר, x.קומה, x.סטטוס }).ToList();
            }

        }

        
    }
}


