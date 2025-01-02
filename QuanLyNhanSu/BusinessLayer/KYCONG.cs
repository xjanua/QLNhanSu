using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class KYCONG
    {
        QuanLyNhanSuEntities1 db = new QuanLyNhanSuEntities1 ();

        public tb_KYCONG getItem(int makycong)
        {
            return db.tb_KYCONG.FirstOrDefault(x => x.MAKYCONG == makycong);
        }

        public List<tb_KYCONG> getList()
        {
            return db.tb_KYCONG.ToList();
        }

        public tb_KYCONG Add(tb_KYCONG kc)
        {
            try
            {
                db.tb_KYCONG.Add(kc);
                db.SaveChanges();
                return kc;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }

        public tb_KYCONG Update(tb_KYCONG kc)
        {
            try
            {
                var _kc = db.tb_KYCONG.FirstOrDefault(x => x.MAKYCONG == kc.MAKYCONG);
                _kc.MAKYCONG = kc.MAKYCONG;
                _kc.NAM = kc.NAM;
                _kc.THANG = kc.THANG;
                _kc.KHOA = kc.KHOA;
                _kc.NGAYCONGTRONGTHANG = kc.NGAYCONGTRONGTHANG;
                _kc.NGAYTINHCONG = kc.NGAYTINHCONG;
                _kc.TRANGTHAI = kc.TRANGTHAI;
                _kc.UPDATED_BY = kc.UPDATED_BY;
                _kc.UPDATED_DATE = kc.UPDATED_DATE;
                db.SaveChanges();
                return kc;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }

        public void Delete(int makycong, int iduser)
        {
            try
            {
                var _kc = db.tb_KYCONG.FirstOrDefault(x => x.MAKYCONG == makycong);
                _kc.DELETED_BY = iduser;
                _kc.DELETED_DATE = DateTime.Now;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }

        public bool KiemTraPhatSinhKyCong(int makycong)
        {
            var kc = db.tb_KYCONG.FirstOrDefault(x => x.MAKYCONG == makycong);
            if (kc == null)
            {
                return false;
            }
            else
            {
                if (kc.TRANGTHAI == true)
                    return true;
                else
                    return false;
            }
        }


    }
}
