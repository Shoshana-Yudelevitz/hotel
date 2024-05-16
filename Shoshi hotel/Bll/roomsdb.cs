using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Shoshi_hotel.Dal;

namespace Shoshi_hotel.Bll
{
    public class roomsdb : GeneralDB
    {
        public roomsdb() : base("rooms") { }
        protected List<rooms> list = new List<rooms>();
        private void DataTableToList()
        {
            foreach (DataRow dr in table.Rows)
            {
                list.Add(new rooms(dr));
            }
        }

        public rooms Find(int code)
        {
            return this.GetList().Find(x => x.Numberoom == code);
        }
        public List<rooms> GetList()
        {
            list.Clear();
            DataTableToList();
            return list;
        }

        public void AddNew(rooms s)
        {
            s.Dr = table.NewRow();
            s.FillDataRow();
            this.Add(s.Dr);
        }

        public void UpdateRow(rooms s)
        {
            s.FillDataRow();
            this.UpDate();
        }
        public int GetNextKey()
        {
            if (this.Size() == 0)
                return 1;
            return this.GetList().Max(x => x.Numberoom) + 1;
        }
        public void DeleteRow(int numberoom)
        {
          rooms exr = this.Find(numberoom);
            if (exr != null)
            {
                exr.Dr.Delete();
                this.UpDate();




            }
        }

    }
}
