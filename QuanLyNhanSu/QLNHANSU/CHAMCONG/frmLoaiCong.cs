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
    public partial class frmLoaiCong : DevExpress.XtraEditors.XtraForm
    {
        public frmLoaiCong()
        {
            InitializeComponent();
        }


        LOAICONG _loaicong;
        Boolean _them;
        int _id;

        void loadData()
        {
            gcDanhSach.DataSource = _loaicong.getList();
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

            txtTen.Enabled = !kt;
            spHeSo.Enabled = !kt;
        }

        void SaveData()
        {
            if (_them)
            {
                tb_LOAICONG lc = new tb_LOAICONG();
                lc.TENLOAICONG = txtTen.Text;
                lc.HESO = double.Parse(spHeSo.EditValue.ToString());
                lc.CREATED_BY = 1;
                lc.CREATED_DATE = DateTime.Now;
                _loaicong.Add(lc);
            }
            else
            {
                var lc = _loaicong.getItem(_id);
                lc.TENLOAICONG = txtTen.Text;
                lc.HESO = double.Parse(spHeSo.EditValue.ToString());
                lc.UPDATED_BY = 1;
                lc.UPDATED_DATE = DateTime.Now;
                _loaicong.Update(lc);
            }
        }


        private void frmLoaiCong_Load(object sender, EventArgs e)
        {
            _them = false;
            _loaicong = new LOAICONG();
            _showHide(true);
            loadData();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtTen.Text = null;
            _showHide(false);
            _them = true;
            spHeSo.EditValue = 1;
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
                _loaicong.Delete(_id, 1);
                loadData();
            }
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveData();
            loadData();
            _them = false;
            _showHide(true);
        }

        private void btnHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtTen.Text = null;
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

        private void gvDanhSach_Click(object sender, EventArgs e)
        {
            if (gvDanhSach.RowCount > 0)
            {
                _id = int.Parse(gvDanhSach.GetFocusedRowCellValue("IDLC").ToString());
                txtTen.Text = gvDanhSach.GetFocusedRowCellValue("TENLOAICONG").ToString();
                spHeSo.Text = gvDanhSach.GetFocusedRowCellValue("HESO").ToString();
            }

        }

    }
}