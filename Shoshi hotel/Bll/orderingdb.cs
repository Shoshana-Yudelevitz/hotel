using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Shoshi_hotel.Dal;

namespace Shoshi_hotel.Bll
{
    public class orderingdb:GeneralDB
    {
        public orderingdb() : base("ordering") { }
        protected List<ordering> list = new List<ordering>();
        private void DataTableToList()
        {
            foreach (DataRow dr in table.Rows)
            {
                list.Add(new ordering(dr));
            }
        }

        public ordering Find(string code)
        {
            return this.GetList().Find(x => x.Cellphone == code);
        }
        public List<ordering> GetList()
        {
            list.Clear();
            DataTableToList();
            return list;
        }

        public void AddNew(ordering o)
        {
           o.Dr = table.NewRow();
            o.FillDataRow();
            this.Add(o.Dr);
            
        }

        public void UpdateRow(ordering o)
        {
            o.FillDataRow();
            this.UpDate();
        }

       
        public void DeleteRow(string Cellphone)
        {
            ordering orde = this.Find(Cellphone);
            if (orde != null)
            {
                orde.Dr.Delete();
                this.UpDate();
            }
        }

    }
}

