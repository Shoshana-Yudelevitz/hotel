using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Shoshi_hotel.Dal;
using Shoshi_hotel.Bll;
using Shoshi_hotel.Gui;
using Shoshi_hotel.Properties;
using Shoshi_hotel.BLL;


namespace Shoshi_hotel.Bll
{
   public class pension
    {
        private int code;
        private string description;
        private int doublecost;
        private int familycost;
        private bool status;
        public DataRow dr;

        public pension()
        {
        }

        public pension(int code, string description, int doublecost, int familycost, DataRow dr)
        {
            this.Code = code;
            this.Description = description;
            this.Doublecost = doublecost;
            this.Familycost = familycost;
            this.Dr = dr;
        }
        
        public int Code
        {
            get => code; set
            {
                if (!Validation.IsNum(value.ToString()))
                    throw new Exception(" שגיאה");
                code = value;
            }
        }
        public string Description
        {
            get => description; set
            {
                if (!Validation.IsHebrew(value.ToString()))
                    throw new Exception(" שגיאה");
                description = value;
            }
        }
        public int Doublecost
        {
            get => doublecost; set
            {
                if (!Validation.IsNum(value.ToString()))
                    throw new Exception(" שגיאה");
                doublecost = value;
            }
        }
        public int Familycost
        {
            get => familycost; set
            {
                if (!Validation.IsNum(value.ToString()))
                    throw new Exception(" שגיאה");
                familycost = value;
            }
        }
        public DataRow Dr { get => dr; set => dr = value; }
        public bool Status { get => status; set => status = value; }

        public override string ToString()
        {
            return Code.ToString();
        }
        public void FillDataRow()
        {
            dr["code"] = this.Code;
            dr["description"] = this.Description;
            dr["doublecost"] = this.Doublecost;
            dr["familycost"] = this.Familycost;
            dr["status"] = this.status;

        }
        public pension(DataRow dr)
        {
            this.dr = dr;
            this.Code = Convert.ToInt32(dr["code"]);
            this.Description = Convert.ToString(dr["description"]);
            this.Doublecost = Convert.ToInt32(dr["doublecost"]);
            this.Familycost = Convert.ToInt32(dr["familycost"]);
            this.status = Convert.ToBoolean(dr["status"]);

        }
    }
}
