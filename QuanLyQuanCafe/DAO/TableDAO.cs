using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class TableDAO
    {
        private static TableDAO instance;

        public static TableDAO Instance
        {
            get { if (instance == null) instance = new TableDAO(); return TableDAO.instance; }
            private set { TableDAO.instance = value; }
        }

        public static int TableWidth = 90;
        public static int TableHeight = 90;

        private TableDAO() { }

        public void SwitchTable(int id1, int id2)
        {
            DataProvider.Instance.ExecuteQuery("USP_SwitchTabel @idTable1 , @idTabel2", new object[] { id1, id2 });
        }

        public List<Table> LoadTableList()
        {
            List<Table> tableList = new List<Table>();
            List<Table> list1 = new List<Table>();
            string query = "select * from TableFood";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            Table table;
            foreach (DataRow item in data.Rows)
            {
                table = new Table(item);
                tableList.Add(table);
            }
            foreach (Table item in tableList)
            {
                list1.Add(item);
            }
            return list1;
            
        }
        public List<Table> getTablestatus()
        {
            List<Table> tableList = new List<Table>();
            string query = string.Format("select distinct status from dbo.TableFood ");
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach ( DataRow item in data.Rows)
            {
                string str = item["status"].ToString();
                Table table = new Table(str,0);
                tableList.Add(table);
            }

            return tableList;
        }
        public bool InsertTable(string name, string status)
        {
            string query = string.Format("INSERT dbo.TableFood (name, status)VALUES  (N'{0}', '{1}')", name , status);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }



        public bool delete_tb(int id)
        {

            string query = string.Format("Delete TableFood where id = {0}", id);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool update_table(string name, string status, int id)
        {
            string query = string.Format("update TableFood set name ='"+name+"', status = '"+status+ "' where id = {0}", id);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public void update_tb(int id, string status)
        {
            DataProvider.Instance.ExecuteQuery("update TableFood set status= '"+status+"' where id=" + id + "");
        }
    }
}
