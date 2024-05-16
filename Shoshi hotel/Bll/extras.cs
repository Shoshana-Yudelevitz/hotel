using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Shoshi_hotel.Dal;
using Shoshi_hotel.Bll;
using Shoshi_hotel.Gui;
using Shoshi_hotel.BLL;


namespace Shoshi_hotel.Bll
{
    public class extras
    {
        private int code;
        private string description;
        private int extraprice;
        private bool status;
        public DataRow dr;

        public int Code
        {
            get => code;
            set
            {
                if (!Validation.IsNum(value.ToString()))
                    throw new Exception(" שגיאה");
                     code = value;
            }
        }


        public string Descrption
        {
            get => description;
            set
            {
                if (!Validation.IsHebrew(value))

                    throw new Exception(" שגיאה,כתוב בעיברית");

                description = value;
            }
        }
        public int Extraprice
        {
            get => extraprice;
            set
            {
                if (!Validation.IsNum(value.ToString()))
                    throw new Exception(" שגיאה");
                extraprice = value;
            }
        }
        public bool Status { get => status; set => status = value; }
        public DataRow Dr { get => dr; set => dr = value; }

        public extras()
        {

        }
        public extras(DataRow dr)
        {
            this.dr = dr;
            this.code = Convert.ToInt32(dr["code"]);
            this.Descrption = Convert.ToString(dr["description"]);
            this.Extraprice = Convert.ToInt32(dr["extraprice"]);
            this.status = Convert.ToBoolean(dr["status"]);


        }

        public extras(int code, string descrption, int extraprice, DataRow dr)
        {
            this.code = code;
            this.description = descrption;
            this.extraprice = extraprice;
            this.dr = dr;
        }

        public override string ToString()
        {
            return code.ToString();
        }
        public void FillDataRow()
        {
            dr["code"] = this.code;
            dr["description"] = this.description;
            dr["extraprice"] = this.extraprice;
            dr["status"] = this.status;

        }
    }
}
        
