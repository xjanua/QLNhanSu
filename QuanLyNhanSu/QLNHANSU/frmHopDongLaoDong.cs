using DataLayer;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataLayer;
using BusinessLayer;
using QLNHANSU.Reports;

namespace QLNHANSU
{
    public partial class frmHopDongLaoDong : DevExpress.XtraEditors.XtraForm
    {
        public frmHopDongLaoDong()
        {
            InitializeComponent();
        }
        NHANVIEN _nhanvien;
        HOPDONGLAODONG _hdld;
        Boolean _them;
        string _soHD;

        void _showHide(Boolean kt)
        {
            btnLuu.Enabled = !kt;
            btnHuy.Enabled = !kt;
            btnThem.Enabled = kt;
            btnSua.Enabled = kt;
            btnXoa.Enabled = kt;
            btnDong.Enabled = kt;
            //gcDanhSach.Enabled = kt;
            txtSoHD.Enabled = !kt;
            dtNgayBatDau.Enabled = !kt;
            dtNgayKetThuc.Enabled = !kt;
            dtNgayKy.Enabled = !kt;
            spHeSoLuong.Enabled = !kt;
            spHeSoLuong.Enabled = !kt;
            spLanKy.Enabled = !kt;
            slkNhanVien.Enabled = !kt;
        }

        void loadData()
        {
            gcDanhSach.DataSource = _hdld.getListFull();
            gvDanhSach.OptionsBehavior.Editable = false;
        }

        void loadNhanVien()
        {
            slkNhanVien.Properties.DataSource = _nhanvien.getList();
            slkNhanVien.Properties.ValueMember = "MaNV";
            slkNhanVien.Properties.DisplayMember = "HOTEN";
        }


        void _reset()
        {
            txtSoHD.Text = string.Empty;
            dtNgayBatDau.Value = DateTime.Now;
            dtNgayBatDau.Value = dtNgayBatDau.Value.AddMonths(6);
            dtNgayKy.Value = DateTime.Now;
            spLanKy.Text = "1";
            spHeSoLuong.Text = "1";

        }

        private void SaveData()
        {
            if (_them)
            {
                var maxSoHD = _hdld.MaxSoHopDong();
                int so = int.Parse(maxSoHD.Substring(0, 5)) + 1;

                tb_HOPDONG hd = new tb_HOPDONG();
                hd.SOHD = so.ToString("00000") + @"/2021/HDLD";
                hd.NGAYBATDAU = dtNgayBatDau.Value;
                hd.NGAYKETTHUC = dtNgayKetThuc.Value;
                hd.NGAYKY = dtNgayKy.Value;
                hd.THOIHAN = cboThoiHan.Text;
                hd.HESOLUONG = double.Parse(spHeSoLuong.EditValue.ToString());
                hd.LANKY = int.Parse(spLanKy.EditValue.ToString());
                hd.MANV = int.Parse(slkNhanVien.EditValue.ToString());
                hd.NOIDUNG = txtNoiDung.RtfText;
                hd.MACTY = 1;
                hd.CREATED_BY = 1;
                hd.CREATED_DATE = DateTime.Now;
                _hdld.Add(hd);
            }
            else
            {
                var hd = _hdld.getItem(_soHD);
                hd.NGAYBATDAU = dtNgayBatDau.Value;
                hd.NGAYKETTHUC = dtNgayKetThuc.Value;
                hd.NGAYKY = dtNgayKy.Value;
                hd.THOIHAN = cboThoiHan.Text;
                hd.HESOLUONG = double.Parse(spHeSoLuong.EditValue.ToString());
                hd.LANKY = int.Parse(spLanKy.EditValue.ToString());
                hd.MANV = int.Parse(slkNhanVien.EditValue.ToString());
                hd.NOIDUNG = txtNoiDung.RtfText;
                hd.MACTY = 1;
                hd.CREATED_BY = 1;
                hd.CREATED_DATE = DateTime.Now;
                _hdld.Update(hd);

            }
        }



        private void frmHopDongLaoDong_Load(object sender, EventArgs e)
        {
            _nhanvien = new NHANVIEN();
            _hdld = new HOPDONGLAODONG();
            _them = false;
            loadData();
            _showHide(true);
            splitContainer1.Panel1Collapsed = true;
            loadNhanVien();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _showHide(false);
            _them = true;
            splitContainer1.Panel1Collapsed = false;
            _reset();
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _showHide(false);
            _them = false;
            splitContainer1.Panel1Collapsed = false;
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Thông báo",
    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _hdld.Delete(_soHD,1);
                loadData();
            }

        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveData();
            loadData();
            _them = false;
            _showHide(true);
            splitContainer1.Panel1Collapsed = true;
        }

        private void btnHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _them = false;
            _showHide(true);
            splitContainer1.Panel1Collapsed = true;
        }

        private void btnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            rptHopDongLaoDong rpt = new rptHopDongLaoDong();
            
        }

        private void btnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void gvDanhSach_Click(object sender, EventArgs e)
        {
            if (gvDanhSach.RowCount > 0)
            {
                _soHD = gvDanhSach.GetFocusedRowCellValue("SOHD").ToString();
                var hd = _hdld.getItem(_soHD);
                txtSoHD.Text = _soHD;
                dtNgayBatDau.Value = hd.NGAYBATDAU.Value;
                dtNgayKetThuc.Value = hd.NGAYKETTHUC.Value;
                dtNgayKy.Value = hd.NGAYKY.Value;
                cboThoiHan.Text = hd.THOIHAN;
                spHeSoLuong.Text = hd.HESOLUONG.ToString();
                txtNoiDung.RtfText = hd.NOIDUNG.ToString();
                spLanKy.Text = hd.LANKY.ToString();
                slkNhanVien.EditValue = hd.MANV;
            }

        }
    }
}