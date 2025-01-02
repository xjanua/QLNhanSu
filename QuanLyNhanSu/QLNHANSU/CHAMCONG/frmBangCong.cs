using BusinessLayer;
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

namespace QLNHANSU.CHAMCONG
{
    public partial class frmBangCong : DevExpress.XtraEditors.XtraForm
    {
        public frmBangCong()
        {
            InitializeComponent();
        }


        KYCONG _kycong;
        Boolean _them;
        int _id;
        int _makycong;

        void loadData()
        {
            gcDanhSach.DataSource = _kycong.getList();
            gvDanhSach.OptionsBehavior.Editable = false;
        }

        void _showHide(Boolean kt)
        {
            btnLuu.Enabled = !kt;
            btnHuy.Enabled = !kt;

            btnThem.Enabled = kt;
            btnSua.Enabled = kt;
            btnXoa.Enabled = kt;
            btnDong.Enabled = kt;
        }

        void SaveData()
        {
            if (_them)
            {
                tb_KYCONG kc = new tb_KYCONG();
                kc.MAKYCONG = int.Parse(cboNam.Text) * 100 + int.Parse(cboThang.Text); // Mã kỳ công = 202201
                kc.NAM = int.Parse(cboNam.Text);
                kc.THANG = int.Parse(cboThang.Text);
                kc.KHOA = chkKhoa.Checked;
                kc.TRANGTHAI = chkTrangThai.Checked;
                kc.NGAYCONGTRONGTHANG = Funcion.demSoNgayLamViecTrongThang(int.Parse(cboThang.Text), int.Parse(cboNam.Text));
                kc.NGAYTINHCONG = DateTime.Now;
                kc.CREATED_BY = 1;
                kc.CREATED_DATE = DateTime.Now;
                _kycong.Add(kc);
                kc.MACTY = 1;
            }
            else
            {
                var kc = _kycong.getItem(_makycong);
                kc.MAKYCONG = int.Parse(cboNam.Text) * 100 + int.Parse(cboThang.Text); // Mã kỳ công = 202201
                kc.NAM = int.Parse(cboNam.Text);
                kc.THANG = int.Parse(cboThang.Text);
                kc.KHOA = chkKhoa.Checked;
                kc.TRANGTHAI = chkTrangThai.Checked;
                kc.NGAYCONGTRONGTHANG = Funcion.demSoNgayLamViecTrongThang(int.Parse(cboThang.Text), int.Parse(cboNam.Text));
                kc.NGAYTINHCONG = DateTime.Now;
                kc.CREATED_BY = 1;
                kc.CREATED_DATE = DateTime.Now;
                _kycong.Update(kc);
            }

        }

        private void frmBangCong_Load(object sender, EventArgs e)
        {
            cboNam.Text = DateTime.Now.Year.ToString();
            cboThang.Text = DateTime.Now.Month.ToString();
            _them = false;
            _kycong = new KYCONG();
            _showHide(true);
            loadData();
        }

        private void gvDanhSach_Click(object sender, EventArgs e)
        {
            if (gvDanhSach.RowCount > 0)
            {
                _makycong = int.Parse(gvDanhSach.GetFocusedRowCellValue("MAKYCONG").ToString());
                cboNam.Text = gvDanhSach.GetFocusedRowCellValue("NAM").ToString();
                cboThang.Text = gvDanhSach.GetFocusedRowCellValue("THANG").ToString();
                chkKhoa.Checked = bool.Parse(gvDanhSach.GetFocusedRowCellValue("KHOA").ToString());
                chkTrangThai.Checked = bool.Parse(gvDanhSach.GetFocusedRowCellValue("TRANGTHAI").ToString());
            }
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _showHide(false);
            _them = true;
            cboNam.Text = DateTime.Now.Year.ToString();
            cboThang.Text = DateTime.Now.Month.ToString();
            chkKhoa.Checked = false;
            chkTrangThai.Checked = false;
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _showHide(false);
            _them = false;
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Thông báo",
MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _kycong.Delete(_makycong, 1);
                loadData();
            }
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveData();
            loadData();
            _makycong = int.Parse(cboNam.Text) * 100 + int.Parse(cboThang.Text);
            _them = false;
            _showHide(true);
        }

        private void btnHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _them = false;
            _showHide(true);
        }

        private void btnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_makycong == 0)
            {
                MessageBox.Show("Vui lòng chọn một kỳ công để xem chi tiết.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            frmBangCongChiTiet frm = new frmBangCongChiTiet();
            frm._makycong = _makycong;
            frm._thang = int.Parse(cboThang.Text);
            frm._nam = int.Parse(cboNam.Text);
            frm.ShowDialog();
        }
    }
}