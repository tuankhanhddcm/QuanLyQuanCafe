using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class BillInfoDAO
    {
        private static BillInfoDAO instance;

        public static BillInfoDAO Instance
        {
            get { if (instance == null) instance = new BillInfoDAO(); return BillInfoDAO.instance; }
            private set { BillInfoDAO.instance = value; }
        }

        private BillInfoDAO() { }


        public int getidbill(int idtable, int id)

        {
            int kq = 0;
            int idbill = 0;
            DataTable data = DataProvider.Instance.ExecuteQuery("select * from Bill as a  join BillInfo as b on a.id = b.idBill join TableFood as c on a.idTable = c.id where c.id=" +idtable);
            foreach (DataRow item in data.Rows)
            {
                idbill = (int)item["idBill"];
            }
            if (this.DeleteBillInfoByFoodID(id, idbill) == true)
            {
                kq = 1;
            }
            return kq;


        }
        public bool DeleteBillInfoByFoodID(int id, int idbill=0)
            
        {
            int kq = 0;
            if(idbill > 0)
            {
                kq = DataProvider.Instance.ExecuteNonQuery("delete dbo.BillInfo WHERE idBill=" + idbill + " and idFood = " + id);
            }
            else
            {
                kq =DataProvider.Instance.ExecuteNonQuery("delete dbo.BillInfo WHERE idFood = " + id);
            }

            return kq > 0 ;
        }

        public List<BillInfo> GetListBillInfo(int id)
        {
            List<BillInfo> listBillInfo = new List<BillInfo>();

            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.BillInfo WHERE idBill = " + id);

            foreach (DataRow item in data.Rows)
            {
                BillInfo info = new BillInfo(item);
                listBillInfo.Add(info);
            }

            return listBillInfo;
        }

        public void InsertBillInfo(int idBill, int idFood, int count)
        {
            DataProvider.Instance.ExecuteNonQuery("USP_InsertBillInfo @idBill , @idFood , @count", new object[] { idBill, idFood, count });
        }


    }
}
