using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Shoshi_hotel.Dal;


namespace Shoshi_hotel.Bll
{
    public class roomforreservationdb:GeneralDB
    {

        public roomforreservationdb() : base("roomforreservation") { }
        protected List<roomforreservation> list = new List<roomforreservation>();
        private void DataTableToList()
        {
            foreach (DataRow dr in table.Rows)
            {
                list.Add(new roomforreservation(dr));
            }
        }

        public roomforreservation Find(int code)
        {
            return this.GetList().Find(x => x.Ordernomber  == code);
        }
        public List<roomforreservation> GetList()
        {
            list.Clear();
            DataTableToList();
            return list;
        }

        public void AddNew(roomforreservation f)
        {
            f.Dr = table.NewRow();
            f.FillDataRow();
            this.Add(f.Dr);
        }

        public void UpdateRow(roomforreservation f)
        {
            f.FillDataRow();
            this.UpDate();
        }
        public int GetNextKey()
        {
            if (this.Size() == 0)
                return 1;
            return this.GetList().Max(x => x.Ordernomber) + 1;
        }
        public void DeleteRow(int number)
        {
            roomforreservation exr = this.Find(number);
            if (exr != null)
            {
                exr.Dr.Delete();
                this.UpDate();
            }
        }
    }
}
