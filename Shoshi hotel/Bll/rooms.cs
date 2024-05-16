using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Shoshi_hotel.Dal;
using Shoshi_hotel.Bll;
using Shoshi_hotel.Properties;
using Shoshi_hotel.BLL;

namespace Shoshi_hotel.Bll
{
   public  class rooms
    {
        private int numberoom;
        private int numberbed;
        private string floor;
        private bool status;
        private int pricecode;
        public DataRow dr;

        public rooms()
        {
        }

        public rooms(int numberoom, int numberbed, string floor, bool status, int pricecode, DataRow dr)
        {
            this.numberoom = numberoom;
            this.numberbed = numberbed;
            this.floor = floor;
            this.status = status;
            this.pricecode = pricecode;
            this.dr = dr;

        }
        
        public int Numberoom
        {
            get => numberoom;
            set
            {
                if (!Validation.IsNum(value.ToString()))

                    throw new Exception(" שגיאה");
                numberoom = value;
            }
        }
        public int Numberbed
        {
            get => numberbed;
            set
            {
                if (!Validation.IsNum(value.ToString()))

                    throw new Exception(" שגיאה");
                numberbed = value;
            }
        }
        public string Floor { get => floor;
            set
            {
                if (!Validation.IsNum(value.ToString()))

                    throw new Exception(" שגיאה");
                floor = value; } }
        public bool Status
        {
            get => status;
            set=>
            
                status = value;
            
        }
        public int Pricecode
        {
            get => pricecode;
            set
            {
                if (!Validation.IsNum(value.ToString()))

                    throw new Exception(" שגיאה");
                pricecode = value;
            }
        }
        public DataRow Dr { get => dr; set => dr = value; }



        public override string ToString()
        {
            return this.Numberoom.ToString();
        }
        public void FillDataRow()
        {
            dr["numberoom"] = this.numberoom;
            dr["numberbed"] = this.numberbed;
            dr["floor"] = this.floor;
            dr["status"] = this.status;
            dr["pricecode"] = this.pricecode;
        }
        public rooms(DataRow dr)
        {
            this.dr = dr;
            this.Numberoom = Convert.ToInt32(dr["numberoom"]);
            this.Numberbed = Convert.ToInt32(dr["numberbed"]);
            this.floor =Convert.ToString(dr["floor"]);
            this.Status = Convert.ToBoolean (dr["status"]);
            this.Pricecode = Convert.ToInt32(dr["pricecode"]);
        }
        public priceroom Thispriceroom()
        {
            priceroomdb tbl = new priceroomdb();
            return tbl.Find(this.Pricecode);
        }
        
    }

    
}

