using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DTO;
using DataLayer;

namespace BusinessLayer
{
    public class NHANVIEN_NANGLUONG
    {
        QuanLyNhanSuEntities1 db = new QuanLyNhanSuEntities1();
        public tb_NHANVIEN_NANGLUONG getItem(String soqd)
        {
            return db.tb_NHANVIEN_NANGLUONG.FirstOrDefault(x=>x.SOQD==soqd);
        }

        public List<tb_NHANVIEN_NANGLUONG> getList()
        {
            return db.tb_NHANVIEN_NANGLUONG.ToList();
        }

        public List<NHANVIEN_NANGLUONG_DTO> getListFull()
        {
            var lstNL = db.tb_NHANVIEN_NANGLUONG.ToList();
            List<NHANVIEN_NANGLUONG_DTO> lstDTO = new List<NHANVIEN_NANGLUONG_DTO>();
            NHANVIEN_NANGLUONG_DTO nlDTO;
            foreach (var item in lstNL)
            {
                nlDTO = new NHANVIEN_NANGLUONG_DTO();
                nlDTO.SOHD = item.SOHD;
                nlDTO.SOQD = item.SOQD;
                nlDTO.HESOLUONGHIENTAI = item.HESOLUONGHIENTAI;
                nlDTO.HESOLUONGMOI = item.HESOLUONGMOI;
                nlDTO.MANV = item.MANV;
                var nv = db.tb_NhanVien.FirstOrDefault(x => x.MaNV == item.MANV);
                nlDTO.HOTEN = nv.HOTEN;
                nlDTO.NGAYKY = item.NGAYKY;
                nlDTO.NGAYLENLUONG = item.NGAYLENLUONG;
                nlDTO.CREATED_BY = item.CREATED_BY;
                nlDTO.CREATED_DATE = item.CREATED_DATE;
                nlDTO.UPDATED_BY = item.UPDATED_BY;
                nlDTO.UPDATED_DATE = item.UPDATED_DATE;
                nlDTO.DELETED_BY = item.DELETED_BY;
                nlDTO.DELETED_DATE = item.DELETED_DATE;
                lstDTO.Add(nlDTO);
            }
            return lstDTO;
        }


        public tb_NHANVIEN_NANGLUONG Add(tb_NHANVIEN_NANGLUONG nl)
        {
            try
            {
                db.tb_NHANVIEN_NANGLUONG.Add(nl);
                db.SaveChanges();
                return nl;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }

        public tb_NHANVIEN_NANGLUONG Update(tb_NHANVIEN_NANGLUONG nl)
        {
            try
            {
                var _nl = db.tb_NHANVIEN_NANGLUONG.FirstOrDefault(x => x.SOQD == nl.SOQD);
                _nl.SOHD = nl.SOHD;
                _nl.HESOLUONGHIENTAI = nl.HESOLUONGHIENTAI;
                _nl.HESOLUONGMOI = nl.HESOLUONGMOI;
                _nl.NGAYKY = nl.NGAYKY;
                _nl.NGAYLENLUONG = nl.NGAYLENLUONG;
                _nl.MANV = nl.MANV;
                _nl.UPDATED_BY = nl.UPDATED_BY;
                _nl.UPDATED_DATE = nl.UPDATED_DATE;
                _nl.GHICHU = nl.GHICHU;
                db.SaveChanges();
                return nl;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }

        public void Delete(string soqd, int uid)
        {
            try
            {
                var _nl = db.tb_NHANVIEN_NANGLUONG.FirstOrDefault(x => x.SOQD == soqd);
                _nl.DELETED_BY = uid;
                _nl.DELETED_DATE = DateTime.Now;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }

        public string MaxSoQuyetDinh()
        {
            var _hd = db.tb_NHANVIEN_NANGLUONG.OrderByDescending(x => x.CREATED_DATE).FirstOrDefault();
            if (_hd != null)
            {
                return _hd.SOQD;
            }
            else
            {
                return "00000";
            }
        }

    }
}
