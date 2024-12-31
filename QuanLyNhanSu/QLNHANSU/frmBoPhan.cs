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
    public partial class frmBoPhan : DevExpress.XtraEditors.XtraForm
    {
        public frmBoPhan()
        {
            InitializeComponent();
        }

        BOPHAN _bophan;
        Boolean _them;
        int _id;

        void _showHide(Boolean kt)
        {
            btnLuu.Enabled = !kt;
            btnHuy.Enabled = !kt;

            btnThem.Enabled = kt;
            btnSua.Enabled = kt;
            btnXoa.Enabled = kt;
            btnDong.Enabled = kt;

            txtTen.Enabled = !kt;
        }

        void loadData()
        {
            gcDanhSach.DataSource = _bophan.getList();
            gvDanhSach.OptionsBehavior.Editable = false;
        }

        void SaveData()
        {
            if (_them)
            {
                tb_BOPHAN bp = new tb_BOPHAN();
                bp.TENBP = txtTen.Text;
                _bophan.Add(bp);
            }
            else
            {
                var bp = _bophan.getItem(_id);
                bp.TENBP = txtTen.Text;
                _bophan.Update(bp);
            }
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtTen.Text = null;
            _showHide(false);
            _them = true;
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
                _bophan.Delete(_id);
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

        }

        private void frmBoPhan_Load(object sender, EventArgs e)
        {
            _them = false;
            _bophan = new BOPHAN();
            _showHide(true);
            loadData();
        }

        private void gvDanhSach_Click(object sender, EventArgs e)
        {
            if (gvDanhSach.RowCount > 0)
            {
                _id = int.Parse(gvDanhSach.GetFocusedRowCellValue("IDBP").ToString());
                txtTen.Text = gvDanhSach.GetFocusedRowCellValue("TENBP").ToString();
            }
        }
    }
}