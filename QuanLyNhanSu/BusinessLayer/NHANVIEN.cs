using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DTO;
using DataLayer;

namespace BusinessLayer
{
    public class NHANVIEN
    {
        QuanLyNhanSuEntities1 db = new QuanLyNhanSuEntities1();

        public tb_NhanVien getItem(int id)
        {
            return db.tb_NhanVien.FirstOrDefault(x => x.MaNV == id);
        }

        public List<tb_NhanVien> getList()
        {
            return db.tb_NhanVien.ToList();
        }

        public List<NHANVIEN_DTO> getListFull()
        {
            var lstNV = db.tb_NhanVien.ToList();

            List<NHANVIEN_DTO> lstNVDTO = new List<NHANVIEN_DTO>();

            NHANVIEN_DTO nvDTO;

            foreach (var item in lstNV)
            {
                nvDTO = new NHANVIEN_DTO();
                nvDTO.MaNV = item.MaNV;
                nvDTO.HOTEN = item.HOTEN;
                nvDTO.GIOITINH = item.GIOITINH;
                nvDTO.NGAYSINH = item.NGAYSINH;
                nvDTO.CCCD = item.CCCD;
                nvDTO.DIENTHOAI = item.DIENTHOAI;
                nvDTO.DIACHI = item.DIACHI;
                nvDTO.HINHANH = item.HINHANH;
                nvDTO.THOIVIEC =item.DATHOIVIEC;

                nvDTO.IDBP = item.IDBP;
                var bp = db.tb_BOPHAN.FirstOrDefault(b => b.IDBP == item.IDBP);
                nvDTO.TENBP = bp.TENBP;

                nvDTO.IDCV = item.IDCV;
                var cv = db.tb_CHUCVU.FirstOrDefault(b => b.IDCV == item.IDCV);
                nvDTO.TENCV = cv.TENCV;

                nvDTO.IDPB = item.IDPB;
                var pb = db.tb_PHONGBAN.FirstOrDefault(p => p.IDPB == item.IDPB);
                nvDTO.TENPB = pb.TENPB;

                nvDTO.IDTD = item.IDTD;
                var td = db.tb_TRINHDO.FirstOrDefault(t => t.IDTD == item.IDTD);
                nvDTO.TENTD = td.TENTD;

                nvDTO.IDDT = item.IDDANTOC;
                var dt = db.tb_DANTOC.FirstOrDefault(d => d.ID == item.IDDANTOC);
                nvDTO.TENDT = dt.TENDT;

                nvDTO.IDTG = item.IDTONGIAO;
                var tg = db.tb_TONGIAO.FirstOrDefault(g => g.ID == item.IDTONGIAO);
                nvDTO.TENTG = tg.TENTG;

                lstNVDTO.Add(nvDTO);

            }
            return lstNVDTO;
        }

        public tb_NhanVien Add(tb_NhanVien nv)
        {
            try
            {
                db.tb_NhanVien.Add(nv);
                db.SaveChanges();
                return nv;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }

        }

        public tb_NhanVien Update(tb_NhanVien nv)
        {
            try
            {
                var _nv = db.tb_NhanVien.FirstOrDefault(x => x.MaNV == nv.MaNV);
                _nv.HOTEN = nv.HOTEN;
                _nv.GIOITINH = nv.GIOITINH;
                _nv.CCCD = nv.CCCD;
                _nv.NGAYSINH = nv.NGAYSINH;
                _nv.DIACHI = nv.DIACHI;
                _nv.DIENTHOAI = nv.DIENTHOAI;
                _nv.IDBP = nv.IDBP;
                _nv.IDCV = nv.IDCV;
                _nv.IDPB = nv.IDPB;
                _nv.IDTD = nv.IDTD;
                _nv.IDDANTOC = nv.IDDANTOC;
                _nv.IDTONGIAO = nv.IDTONGIAO;
                _nv.MACTY = nv.MACTY;
                _nv.HINHANH = nv.HINHANH;
                _nv.DATHOIVIEC = nv.DATHOIVIEC;
                db.SaveChanges();
                return nv;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var _nv = db.tb_NhanVien.FirstOrDefault(x => x.MaNV == id);
                db.tb_NhanVien.Remove(_nv);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }

        }
    }
}
