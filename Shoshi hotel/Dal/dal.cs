using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shoshi_hotel.Dal;
using System.Data.OleDb;
using System.Data;
using Shoshi_hotel.Bll;

namespace Shoshi_hotel.Dal
{
    public class Dal
    {

        private static Dal instance;
        private OleDbConnection con;
        private DataSet ds;
        private Dal(string connectionString)
        {
            con = new OleDbConnection(connectionString);
            ds = new DataSet();
        }
        public static Dal GetInstance()
        {
            if (instance == null)
            {
                string path = System.IO.Directory.GetCurrentDirectory();
                int x = path.IndexOf("\\bin");
                path = path.Substring(0, x) + "\\Dal\\Shoshi hotel.accdb";
                instance = new Dal(@"provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + path + "';Persist Security Info=True");
            }
            return instance;
        }
        public void AddTable(string tabbleName)
        {
            if (!ds.Tables.Contains(tabbleName))
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter("Select * from " + tabbleName, con);
                adapter.Fill(ds,tabbleName);
            }
        }
        public DataTable GetTable(string tableName)
        {
            if (!ds.Tables.Contains(tableName))
                AddTable(tableName);
            return ds.Tables[tableName];
        }
        public bool RemoveTable(string tableName)
        {
            bool suceed = true;
            try
            {
                ds.Tables.Remove(tableName);
            }
            catch
            {
                suceed = false;
            }
            return suceed;
        }
        public DataTable GetSelectTable(string sql)
        {
            DataTable dt = new DataTable();
            OleDbDataAdapter adapter = new OleDbDataAdapter(sql, con);
            adapter.Fill(dt);
            return dt;
        }
        public void Update(string tableName)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter("select * from " + tableName, con);
            OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter);
            adapter.InsertCommand = builder.GetInsertCommand();
            adapter.UpdateCommand = builder.GetUpdateCommand();
            adapter.DeleteCommand = builder.GetDeleteCommand();
            adapter.Update(ds, tableName);
        }
        public void Update()
        {
            foreach (DataTable table in ds.Tables)
            {
                Update(table.TableName);
            }
        }
    }
}


