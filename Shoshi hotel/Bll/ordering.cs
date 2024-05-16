using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Shoshi_hotel.Dal;
using Shoshi_hotel.Properties;
using Shoshi_hotel.Gui;
using Shoshi_hotel.Bll;
using Shoshi_hotel.BLL;


namespace Shoshi_hotel.Bll
{
    public class ordering
    {
        private string cellphone;
        private string firstname;
        private string lastname;
        public DataRow dr;
     
        public string  Cellphone
        {
            get => cellphone;
            set
            {
                if (!Validation.IsNum(value.ToString()))

                    throw new Exception(" פלאפון לא תקין");

                cellphone = value;
            }
        }
        public string Firstname
        {
            get => firstname;
            set
            {
                if (!Validation.IsHebrew(value))

                    throw new Exception(" תז לא תקינה");
                firstname = value;
            }
        }
        public DataRow Dr { get => dr; set => dr = value; }
        public string Lastname { get => lastname;
            set
            {
                if (!Validation.IsHebrew(value))

                    throw new Exception(" תז לא תקינה");
                 lastname = value; } }

        public ordering()
        {
        }

        public ordering(string  cellphone, string firstname, string lastname, DataRow dr)
        {
            this.Cellphone = cellphone;
            this.Firstname = firstname;
            this.Lastname = lastname;
            this.Dr = dr;
        }

        public override string ToString()
        {
            return Firstname + Lastname;
        }
        public void FillDataRow()
        {
            Dr["cellphone"] = this.Cellphone;
            Dr["firstname"] = this.Firstname;
            Dr["lastname"] = this.Lastname;

        }
        public ordering(DataRow dr)
        {
            this.Dr = dr;
            this.Cellphone = (dr["cellphone"]).ToString();
            this.Firstname = Convert.ToString(dr["firstname"]);
            this.Lastname = Convert.ToString(dr["lastname"]);
        }

        public orders ThisOrder()
        {
            ordersdb odv = new ordersdb();
            return odv.Find(this.cellphone);
        }
        
    } 
}
