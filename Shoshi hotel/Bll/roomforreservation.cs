using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Shoshi_hotel.Dal;

namespace Shoshi_hotel.Bll
{
    public class roomforreservation
    {
        private int ordernomber;
        private string roomnumber;
        public DataRow dr;

        public roomforreservation()
        {
        }

        public roomforreservation(int ordernomber, string roomnumber, DataRow dr)
        {
            this.ordernomber = ordernomber;
            this.roomnumber = roomnumber;
            this.Dr = dr;
        }

        public int Ordernomber { get => ordernomber; set => ordernomber = value; }
        public string Roomnumber { get => roomnumber; set => roomnumber = value; }
        public DataRow Dr { get => dr; set => dr = value; }

        public override string ToString()
        {
            return ordernomber.ToString();
        }
        public void FillDataRow()
        {
            Dr["ordernumber"] = this.Ordernomber;
            Dr["roomnumber"] = this.Roomnumber;
        }
        public roomforreservation(DataRow dr)
        {
            this.Dr = dr;
            this.Ordernomber = Convert.ToInt32(dr["ordernumber"]);
            this.Roomnumber = Convert.ToString(dr["roomnumber"]);
        }
        public rooms Thisrooms()
        {
            roomsdb tbl = new roomsdb();
            return tbl.Find(Convert.ToInt32(this.roomnumber));
        }
        public orders Thisorders()
        {
            ordersdb tbl = new ordersdb();
            return tbl.Find(this.Ordernomber);
        }
    }
}
