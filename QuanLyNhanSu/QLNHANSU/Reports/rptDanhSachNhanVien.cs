using BusinessLayer.DTO;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace QLNHANSU.Reports
{
    public partial class rptDanhSachNhanVien : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDanhSachNhanVien()
        {
            InitializeComponent();
        }

        List<NHANVIEN_DTO> _lstNV;

        public rptDanhSachNhanVien(List<NHANVIEN_DTO> lstNV)
        {
            InitializeComponent();
            this._lstNV = lstNV;
            this.DataSource = _lstNV;
            loadData();
        }

        void loadData()
        {
            lblMaNV.DataBindings.Add("Text", _lstNV, "MANV");
            lblHoTen.DataBindings.Add("Text", _lstNV, "HOTEN");
            lblGioiTinh.DataBindings.Add("Text", _lstNV, "GIOITINH");
            lblNgaySinh.DataBindings.Add("Text", _lstNV, "NGAYSINH");
            lblCCCD.DataBindings.Add("Text", _lstNV, "CCCD");
            lblDienThoai.DataBindings.Add("Text", _lstNV, "DIENTHOAI");
            lblPhongBan.DataBindings.Add("Text", _lstNV, "TENPB");
            lblChucVu.DataBindings.Add("Text", _lstNV, "TENCV");
            lblTrinhDo.DataBindings.Add("Text", _lstNV, "TENTD");
            lblDanToc.DataBindings.Add("Text", _lstNV, "TENDT");
            lblTonGiao.DataBindings.Add("Text", _lstNV, "TENTG");
            lblDiaChi.DataBindings.Add("Text", _lstNV, "DIACHI");
        }

    }
}
