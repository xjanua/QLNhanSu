using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class BangCong_NV_CT
    {
        QuanLyNhanSuEntities1 db = new QuanLyNhanSuEntities1();
        public tb_BANGCONG_NV_CT Add(tb_BANGCONG_NV_CT bcct)
        {
            try
            {
                db.tb_BANGCONG_NV_CT.Add(bcct);
                db.SaveChanges();
                return bcct;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }

    }
}
