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
    public partial class frmTonGiao : DevExpress.XtraEditors.XtraForm
    {
        public frmTonGiao()
        {
            InitializeComponent();
        }
        TONGIAO _tongiao;
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
            gcDanhSach.DataSource = _tongiao.getList();
            gvDanhSach.OptionsBehavior.Editable = false;
        }

        void SaveData()
        {
            if (_them)
            {
                tb_TONGIAO tg = new tb_TONGIAO();
                tg.TENTG = txtTen.Text;
                _tongiao.Add(tg);
            }
            else
            {
                var tg = _tongiao.getItem(_id);
                tg.TENTG = txtTen.Text;
                _tongiao.Update(tg);
            }
        }

        private void frmTonGiao_Load(object sender, EventArgs e)
        {
            _them = false;
            _tongiao = new TONGIAO();
            _showHide(true);
            loadData();

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
                _tongiao.Delete(_id);
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

        private void gvDanhSach_Click(object sender, EventArgs e)
        {
            if (gvDanhSach.RowCount > 0)
            {
                _id = int.Parse(gvDanhSach.GetFocusedRowCellValue("ID").ToString());
                txtTen.Text = gvDanhSach.GetFocusedRowCellValue("TENTG").ToString();
            }
        }
    }
}