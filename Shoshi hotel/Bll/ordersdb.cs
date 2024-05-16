using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Shoshi_hotel.Dal;
using Shoshi_hotel.Bll;

namespace Shoshi_hotel.Bll
{
    public class ordersdb:GeneralDB
    {

        public ordersdb() : base("orders") { }
        protected List<orders> list = new List<orders>();
        private void DataTableToList()
        {
            foreach (DataRow dr in table.Rows)
            {
                list.Add(new orders(dr));
            }
        }

        public orders Find(int code)
        {
            return this.GetList().Find(x => x.Ordernumber == code);
        }

        public orders Find(string code)
        {
            return this.GetList().Find(x => x.Inviting == code);
        }
        public List<orders> GetList()
        {
            list.Clear();
            DataTableToList();
            return list;
        }

        public void AddNew(orders r)
        {
            r.Dr = table.NewRow();
            r.FillDataRow();
            this.Add(r.Dr);
        }

        public void UpdateRow(orders r)
        {
            r.FillDataRow();
            this.UpDate();
        }
        public int GetNextKey()
        {
            if (this.Size() == 0)
                return 1;
            return this.GetList().Max(x => x.Ordernumber) + 1;
        }
        public void DeleteRow(int inviting)
        {
            orders exr = this.Find(inviting);
            if (exr != null)
            {
                exr.Dr.Delete();
                this.UpDate();
            }
        }

    }
}

