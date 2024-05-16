using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Shoshi_hotel.Dal;
using Shoshi_hotel.Gui;



namespace Shoshi_hotel.Bll
{
    public class extradb:GeneralDB
    {
        public extradb() : base("extras") { }
        protected List<extras> list = new List<extras>();
        private void DataTableToList()
        {
            foreach (DataRow dr in table.Rows)
            {
                list.Add(new extras(dr));
            }
        }

        public extras Find(int code)
        {
            return this.GetList().Find(x => x.Code== code);
        }
        public List<extras> GetList()
        {
            list.Clear();
            DataTableToList();
            return list;
        }
        public void UpDate()
        {
            Dal.Dal.GetInstance().Update(table.TableName);
        }

        public void AddNew(Bll.extras e)
        {
            e.Dr = table.NewRow();
            e.FillDataRow();
            this.Add(e.Dr);
            
        }
        private void Possible()
        {
            
        }

        public void UpdateRow(extras e)
        {
            e.FillDataRow();
            this.UpDate();
        }
        public int GetNextKey()
        {
            if (this.Size() == 0)
                return 1;
            return this.GetList().Max(x => x.Code) + 1;
        }
        public void DeleteRow(int code)
        {
            extras exr = this.Find(code);
            if (exr != null)
            {
                exr.Dr.Delete();
                this.UpDate();
            }
        }
    }
}

