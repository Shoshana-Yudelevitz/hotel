using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Shoshi_hotel.Dal;

namespace Shoshi_hotel.Bll
{
    public class priceroomdb:GeneralDB
    {
        public priceroomdb() : base("priceroom") { }
        protected List<priceroom> list = new List<priceroom>();
        private void DataTableToList()
        {
            foreach (DataRow dr in table.Rows)
            {
                list.Add(new priceroom(dr));
            }
        }

        public priceroom Find(int code)
        {
            return this.GetList().Find(x => x.Pricecode == code);
        }
        public List<priceroom> GetList()
        {
            list.Clear();
            DataTableToList();
            return list;
        }

        public void AddNew(priceroom m)
        {
            m.Dr = table.NewRow();
            m.Filldatarow();
            this.Add(m.Dr);
        }

        public void UpdateRow(priceroom m)
        {
            m.Filldatarow();
            this.UpDate();
        }
        public int GetNextKey()
        {
            if (this.Size() == 0)
                return 1;
            return this.GetList().Max(x => x.Pricecode) + 1;
        }
        public void DeleteRow(int code)
        {
            priceroom exr = this.Find(code);
            if (exr != null)
            {
                exr.Dr.Delete();
                this.UpDate();
            }
        }
    }
}

