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

namespace QLNHANSU
{
    public partial class frmTHOIVIEC : DevExpress.XtraEditors.XtraForm
    {
        public frmTHOIVIEC()
        {
            InitializeComponent();
        }

        bool _them;
        string _soQD;
        NHANVIEN_THOIVIEC _nvtv;
        NHANVIEN _nhanvien;


        private void frmTHOIVIEC_Load(object sender, EventArgs e)
        {
            _nvtv = new NHANVIEN_THOIVIEC();
            _nhanvien = new NHANVIEN();
            _them = false;

            _showHide(true);

            loadNhanVien();

            loadData();

            splitContainer1.Panel1Collapsed = true;
        }

        private void _showHide(bool kt)
        {
            btnLuu.Enabled = !kt;
            btnHuy.Enabled = !kt;
            btnThem.Enabled = kt;
            btnSua.Enabled = kt;
            btnXoa.Enabled = kt;
            btnDong.Enabled = kt;
            btnPrint.Enabled = kt;
            gcDanhSach.Enabled = kt;
            txtSoQD.Enabled = !kt;
            txtGhiChu.Enabled = !kt;
            txtLyDo.Enabled = !kt;
            slkNhanVien.Enabled = !kt;
            dtNgayNopDon.Enabled = !kt;

        }

        private void _reset()
        {
            txtSoQD.Text = string.Empty;
            txtLyDo.Text = string.Empty;
            txtGhiChu.Text = string.Empty;
            dtNgayNopDon.Value = DateTime.Now;
            dtNgayNghi.Value = dtNgayNopDon.Value.AddDays(45);
        }


        void loadNhanVien()
        {
            slkNhanVien.Properties.DataSource = _nhanvien.getListFull();
            slkNhanVien.Properties.ValueMember = "MaNV";
            slkNhanVien.Properties.DisplayMember = "HOTEN";
        }

        private void loadData()
        {
            gcDanhSach.DataSource = _nvtv.getListFull();
            gvDanhSach.OptionsBehavior.Editable = false;
        }

        private void SaveData()
        {
            tb_NHANVIEN_THOIVIEC tv;
            if (_them)
            {
                // Số hợp đồng có dạng: 00001/2021/HDLD
                var maxSoQD = _nvtv.MaxSoQuyetDinh();
                int so = int.Parse(maxSoQD.Substring(0, 5)) + 1;

                tv = new tb_NHANVIEN_THOIVIEC();

                tv.SOQD = so.ToString("00000") + @"/" + DateTime.Now.Year.ToString() + @"/QDTV";
                tv.LYDO = txtLyDo.Text;

                tv.NGAYNOPDON = dtNgayNopDon.Value;

                tv.NGAYNGHI = dtNgayNghi.Value;

                tv.GHICHU = txtGhiChu.Text;

                tv.MANV = int.Parse(slkNhanVien.EditValue.ToString());

                tv.GHICHU = txtGhiChu.Text;

                tv.CREATED_BY = 1;

                tv.CREATED_DATE = DateTime.Now;

                _nvtv.Add(tv);
            }
            else
            {
                tv = _nvtv.getItem(_soQD);

                tv.LYDO = txtLyDo.Text;

                tv.NGAYNOPDON = dtNgayNopDon.Value;

                tv.NGAYNGHI = dtNgayNghi.Value;

                tv.GHICHU = txtGhiChu.Text;

                tv.MANV = int.Parse(slkNhanVien.EditValue.ToString());
                tv.UPDATED_BY = 1;

                tv.UPDATED_DATE = DateTime.Now;

                _nvtv.Update(tv);
            }
            var nv = _nhanvien.getItem(tv.MANV.Value);
            nv.DATHOIVIEC = true;
            _nhanvien.Update(nv);
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
            _them = false;
            _showHide(false);
            splitContainer1.Panel1Collapsed = false;
            gcDanhSach.Enabled = true;

        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _nvtv.Delete(_soQD, 1);
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

        private void gvDanhSach_Click(object sender, EventArgs e)
        {
            if (gvDanhSach.RowCount > 0)
            {
                _soQD = gvDanhSach.GetFocusedRowCellValue("SOQD").ToString();

                var tv = _nvtv.getItem(_soQD);
                txtSoQD.Text = _soQD;

                dtNgayNopDon.Value = tv.NGAYNOPDON.Value;
                dtNgayNghi.Value = tv.NGAYNGHI.Value;
                slkNhanVien.EditValue = tv.MANV;
                txtGhiChu.Text = tv.GHICHU;

                txtLyDo.Text = tv.LYDO;
            }
        }

        private void btnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void dtNgayNopDon_ValueChanged(object sender, EventArgs e)
        {
            dtNgayNghi.Value = dtNgayNopDon.Value.AddDays(45);
        }

        //private void gvDanhSach_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        //{
        //    if (e.Column.Name == "DELETED_BY" && e.CellValue != null)
        //    {
        //        Image img = Properties.Resources.del_Icon_x16;
        //        e.Graphics.DrawImage(img, e.Bounds.X, e.Bounds.Y);
        //        e.Handled = true;
        //    }

        //}
    }
}