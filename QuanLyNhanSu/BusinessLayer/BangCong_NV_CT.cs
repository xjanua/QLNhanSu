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

        public tb_BANGCONG_NV_CT getItem(int makycong, int manv, int ngay)
        {
            return db.tb_BANGCONG_NV_CT
                     .FirstOrDefault(x => x.MAKYCONG == makycong &&
                                          x.MANV == manv &&
                                          x.NGAY.Value.Day == ngay);
        }

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

        public tb_BANGCONG_NV_CT Update(tb_BANGCONG_NV_CT bcct)
        {
            try
            {
                tb_BANGCONG_NV_CT bcnv = db.tb_BANGCONG_NV_CT.FirstOrDefault(x => x.MAKYCONG == bcct.MAKYCONG && x.MANV == bcct.MANV && x.NGAY == bcct.NGAY);
                bcnv.KYHIEU = bcct.KYHIEU;
                bcnv.GIOVAO = bcct.GIOVAO;
                bcnv.GIORA = bcct.GIORA;
                bcnv.NGAYPHEP = bcct.NGAYPHEP;
                bcnv.GHICHU = bcct.GHICHU;
                bcnv.CONGCHUNHAT = bcct.CONGCHUNHAT;
                bcnv.CONGNGAYLE = bcct.CONGNGAYLE;
                bcnv.NGAYCONG = bcct.NGAYCONG;
                bcnv.UPDATED_BY = bcct.UPDATED_BY;
                bcnv.UPDATED_DATE = bcct.UPDATED_DATE;
                db.SaveChanges();
                return bcct;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }
        public double tongNgayPhep(int makycong, int manv)
        {
            return db.tb_BANGCONG_NV_CT
                     .Where(x => x.MAKYCONG == makycong &&
                                 x.MANV == manv &&
                                 x.NGAYPHEP != null)
                     .ToList()
                     .Sum(p => p.NGAYPHEP.Value);
        }

        public double tongNgayCong(int makycong, int manv)
        {
            return db.tb_BANGCONG_NV_CT
                     .Where(x => x.MAKYCONG == makycong &&
                                 x.MANV == manv &&
                                 x.NGAYCONG != null)
                     .ToList()
                     .Sum(p => p.NGAYCONG.Value);
        }
    }
}
