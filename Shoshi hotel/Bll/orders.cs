using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Shoshi_hotel.Dal;
using Shoshi_hotel.Properties;
using Shoshi_hotel.Bll;
using Shoshi_hotel.BLL;



namespace Shoshi_hotel.Bll
{
    public class orders
    {
        private string inviting;
        private DateTime enterydate;
        private DateTime releasedate;
        private int typeofpension;
        private int numberofbeds;
        private int ordernumber;
        private int totalorder;
        private int discount;
        private string ticketnumber;
        private string validity;
        private string threedigits;
        private string IDnumber;
        private string  cardcode;
        private int payments;
        private int extracode;
        
        public DataRow dr;

        public orders()
        {
        }

        public orders(string inviting, DateTime enterydate, DateTime releasedate, int numberoom, int typeofpension, int numberofbeds, int ordernumber, int totalorder, int discount, string ticketnumber, string validity, string threedigits, string iDnumber, string code, int payments, bool status, int extracode, DataRow dr)
        {
            this.inviting = inviting;
            this.enterydate = enterydate;
            this.releasedate = releasedate;
            this.typeofpension = typeofpension;
            this.numberofbeds = numberofbeds;
            this.ordernumber = ordernumber;
            this.totalorder = totalorder;
            this.discount = discount;
            this.ticketnumber = ticketnumber;
            this.validity = validity;
            this.threedigits = threedigits;
            this.IDnumber = iDnumber;
            this.cardcode = code;
            this.payments = payments;
            this.extracode = extracode;
            
            this.dr = dr;
        }
      
        
        public string Inviting { get => inviting; set => inviting = value;  }
        public DateTime Enterydate { get => enterydate; set => enterydate = value; }
        public DateTime Releasedate { get => releasedate; set => releasedate = value; }
        public int Typeofpension { get => typeofpension;
            set {
                if (!Validation.IsNum(value.ToString()))

                    throw new Exception(" שגיאה");
                typeofpension = value; } }
        public int Numberofbeds { get => numberofbeds;

            set
            {
                if (!Validation.IsNum(value.ToString()))

                    throw new Exception(" שגיאה");
                numberofbeds = value; } }
        public int Ordernumber { get => ordernumber; set => ordernumber = value; }
        public int Totalorder { get => totalorder;
            set {
                if (!Validation.IsNum(value.ToString()))

                    throw new Exception(" שגיאה");
                totalorder = value; } }
        public int Discount { get => discount;
            set
            {
                if (!Validation.IsNum(value.ToString()))

                    throw new Exception(" שגיאה");
                discount = value; } }
        public string Ticketnumber
        {
            get => ticketnumber;
            set {ticketnumber = value; }
        }
        public string Validity
        {
            get => validity;
            set {
                
                validity = value; }
        }
        public string Threedigits { get => threedigits;
            set {
               
                threedigits = value; }
        }
        public string IDnumber1
        {
            get => IDnumber;
            set {
                if (!Validation.CheckId(value))
                    throw new Exception("שגיאה");
                IDnumber = value; }
        }
        public string Cardcode
        {
            get => cardcode;
            set {
                //if (!Validation.IsNum(value.ToString()))

//throw new Exception("שגיאה");
                cardcode = value; }
        }
        public int Payments { get => payments; set => payments = value; }
        public int Extracode { get => extracode;
            set {
                if (!Validation.IsNum(value.ToString()))

                    throw new Exception(" שגיאה");
                extracode = value; } }
        public DataRow Dr { get => dr; set => dr = value; }
       

        public override string ToString()
        {
            return inviting.ToString();
        }
        public void FillDataRow()
        {
            dr["inviting"] = this.Inviting;
            dr["entrydate"] = this.Enterydate;
            dr["releasedate"] = this.Releasedate;
            dr["typeofpension"] = this.Typeofpension;
            dr["numberofbeds"] = this.Numberofbeds;
            dr["ordernumber"] = this.Ordernumber;
            dr["totalorder"] = this.Totalorder;
            dr["ticketnumber"] = this.Ticketnumber;
            dr["validity"] = this.Validity;
            dr["threedigits"] = this.Threedigits;
            dr["discount"] = this.Discount;
            dr["IDnumber"] = this.IDnumber1;
            dr["cardcode"] = this.cardcode;
            dr["payments"] = this.Payments;
            dr["extracode"] = this.Extracode;
          

        }
        public orders(DataRow dr)
        {
            this.dr = dr;
            this.inviting = (dr["inviting"]).ToString();
            this.enterydate = Convert.ToDateTime(dr["entrydate"]);
            this.releasedate = Convert.ToDateTime(dr["releasedate"]);
            this.typeofpension = Convert.ToInt32(dr["typeofpension"]);
            this.numberofbeds = Convert.ToInt32(dr["numberofbeds"]);
            this.ordernumber = Convert.ToInt32(dr["ordernumber"]);
            this.Totalorder = Convert.ToInt32(dr["totalorder"]);
            this.Discount = Convert.ToInt32(dr["discount"]);
            this.Ticketnumber = Convert.ToString(dr["ticketnumber"]);
            this.Validity = Convert.ToString(dr["validity"]);
            this.Threedigits = Convert.ToString(dr["threedigits"]);
            this.IDnumber1 = Convert.ToString(dr["IDnumber"]);
            this.cardcode = Convert.ToString(dr["cardcode"]);
            this.Payments = Convert.ToInt32(dr["payments"]);
            this.extracode = Convert.ToInt32(dr["extracode"]);
         


        }
        public extras Thisextras()
        {
            extradb tbl = new extradb();
            return tbl.Find(this.extracode);
        }
        public ordering Thisordering()
        {
            orderingdb tbl = new orderingdb();
            return tbl.Find(this.Inviting);
        }
        public pension Thispension()
        {
            pensiondb tbl = new pensiondb();
            return tbl.Find(this.Typeofpension);
        }
        public rooms Thisrooms()
        {
            roomsdb tbl = new roomsdb();

            return tbl.Find(this.Ordernumber);
        }
    }
    
}
