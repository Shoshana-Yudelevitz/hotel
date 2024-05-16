using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Shoshi_hotel.Dal;
using Shoshi_hotel.BLL;
using Shoshi_hotel.Bll;


namespace Shoshi_hotel.Bll
{
    public class pensiondb:GeneralDB
    {
        public pensiondb() : base("pension") { }
        protected List<pension> list = new List<pension>();
        private void DataTableToList()
        {
            foreach (DataRow dr in table.Rows)
            {
                list.Add(new pension(dr));
            }
        }

        public pension Find(int code)
        {
            return this.GetList().Find(x => x.Code == code);
        }
        public List<pension> GetList()
        {
            list.Clear();
            DataTableToList();
            return list;
        }

        public void AddNew(pension p)
        {
            p.Dr = table.NewRow();
            p.FillDataRow();
            this.Add(p.Dr);
        }

        public void UpdateRow(pension p)
        {
            p.FillDataRow();
            this.UpDate();
        }
        public int GetNextKey()
        {
            if (this.Size() == 0)
                return 1;
            return this.GetList().Max(x => x.Code) + 1;
        }
        public void UpDate()
        {
            Dal.Dal.GetInstance().Update(table.TableName);
        }
        public void DeleteRow(int code)
        {
            pension exr = this.Find(code);
            if (exr != null)
            {
                exr.Dr.Delete();
                this.UpDate();
            }
        }
    }
}

