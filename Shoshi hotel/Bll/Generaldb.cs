using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Shoshi_hotel.Bll;
using Shoshi_hotel.Dal;
using Shoshi_hotel.Properties;


namespace Shoshi_hotel.Bll
{
  public   class GeneralDB
    {
        protected DataTable table;

        public GeneralDB(string tableName)
        {
            Dal.Dal.GetInstance().AddTable(tableName);
            table = Dal.Dal.GetInstance().GetTable(tableName);
        }
        public void Update()
        {
            Dal.Dal.GetInstance().Update(table.TableName);
        }
        public DataTable GetTable()
        {
            return this.table;
        }

        public int Size()
        {
            return table.Rows.Count;
        }
        public bool IsEmpty()
        {
            return table.Rows.Count == 0;
        }
        public virtual void Save()
        {
            Dal.Dal.GetInstance().Update(table.TableName);
        }
        public void Add(DataRow dr)
        {
            table.Rows.Add(dr);
            this.Save();
        }
        public void UpDate()
        {
            Dal.Dal.GetInstance().Update(table.TableName);
        }

    }
}
    

