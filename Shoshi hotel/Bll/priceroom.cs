using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Shoshi_hotel.Dal;
using Shoshi_hotel.BLL;
using Shoshi_hotel.Gui;
using Shoshi_hotel.Properties;

namespace Shoshi_hotel.Bll
{
   public class priceroom
    {
        private int pricecode;
        private string roomtype;
        private int pricemight;
        private bool status;
        public DataRow dr;

        public priceroom()
        {
        }

        public priceroom(int pricecode, string roomtype, int pricemight,bool status, DataRow dr)
        {
            this.pricecode = pricecode;
            this.roomtype = roomtype;
            this.pricemight = pricemight;
            this.status = status;
            this.dr = dr;
        }
      
        public int Pricecode
        {
            get => pricecode; set
            {
                if (!Validation.IsNum(value.ToString()))
                    throw new Exception(" שגיאה");
                pricecode = value;
            }
        }
        public string Roomtype
        {
            get => roomtype; set
            {
                if (!Validation.IsHebrew(value.ToString()))
                    throw new Exception(" שגיאה");
                roomtype = value;
            }
        }
        public int Pricemight
        {
            get => pricemight; set
            {

                if (!Validation.IsNum(value.ToString()))
                    throw new Exception(" שגיאה");
                pricemight = value;
            }
        }
        public bool Status { get => status; set => status = value; }
        public DataRow Dr { get => dr; set => dr = value; }

        public override string ToString()
        {
            return pricecode.ToString();
        }
        public void Filldatarow()
        {
            dr["pricecode"] = this.Pricecode;
            dr["roomtype"] = this.Roomtype;
            dr["pricepernight"] = this.Pricemight;
            dr["status"] = this.status;
        }
        public priceroom(DataRow dr)
        {
            this.dr = dr;
            this.Pricecode = Convert.ToInt32(dr["pricecode"]);
            this.Roomtype = Convert.ToString(dr["roomtype"]);
            this.Pricemight = Convert.ToInt32(dr["pricepernight"]);
            this.status = Convert.ToBoolean(dr["status"]);
        }
    }

        
}
