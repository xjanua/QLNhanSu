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
    public partial class frmCapNhatNgayCong : DevExpress.XtraEditors.XtraForm
    {
        public frmCapNhatNgayCong()
        {
            InitializeComponent();
        }

        public int _manv;
        public String _hoten;
        public int _makycong;
        public String _ngay;
        public int _cNgay;
        BangCong_NV_CT _bcct_nv;
        frmBangCongChiTiet frmBCCC = (frmBangCongChiTiet)Application.OpenForms["frmBangCongChiTiet"];

        KYCONGCHITIET _kcct;

        private void frmCapNhatNgayCong_Load(object sender, EventArgs e)
        {
            _kcct = new KYCONGCHITIET();
            _bcct_nv = new BangCong_NV_CT();
            lblID.Text = _manv.ToString();
            lblHoTen.Text = _hoten;
            string nam = _makycong.ToString().Substring(0, 4);
            string thang = _makycong.ToString().Substring(4);
            string ngay = _ngay.Substring(1);
            DateTime _d = DateTime.Parse(nam + "-" + thang + "-" + ngay);
            cldNgayCong.SetDate(_d);
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string _valueChamCong = rdgChamCong.Properties.Items[rdgChamCong.SelectedIndex].Value.ToString();
            string _valueNgayNghi = rdgBuoiNghi.Properties.Items[rdgBuoiNghi.SelectedIndex].Value.ToString();
            string fieldName = "D" + _cNgay.ToString();
            var kcct = _kcct.getItem(_makycong, _manv);

            //double? tongngaycong = kcct.TONGNGAYCONG;
            //double? tongngayphep = kcct.NGAYPHEP;
            //double? tongngaykhongphep = kcct.NGHIKHONGPHEP;
            //double? tongngayle = kcct.CONGNGAYLE;

            // Validate kỳ công
            if (cldNgayCong.SelectionRange.Start.Year * 100 + cldNgayCong.SelectionRange.Start.Month != _makycong)
            {
                MessageBox.Show("Thực hiện chấm công không đúng kỳ công. Vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Cập nhật KYCONGCHITIET => cập nhật BANGCONG_NV_CT
            Funcion.execQuery("UPDATE tb_KYCONGCHITIET SET " + fieldName + "='" + _valueChamCong + "' WHERE MAKYCONG=" + _makycong + " AND MANV=" + _manv);

            tb_BANGCONG_NV_CT bcctnv = _bcct_nv.getItem(_makycong, _manv, cldNgayCong.SelectionStart.Day);

            bcctnv.KYHIEU = _valueChamCong;

            switch (_valueChamCong)
            {
                case "P":
                    
                    if (_valueNgayNghi == "NN")
                    {
                        bcctnv.NGAYPHEP = 1;
                        bcctnv.NGAYCONG = 0;
                    }
                    else
                    {
                        bcctnv.NGAYPHEP = 0.5;
                        bcctnv.NGAYCONG = 0.5;
                    }
                    break;

                case "V":
                    if (_valueNgayNghi == "NN")
                    {
                        bcctnv.NGAYPHEP = 1;
                        bcctnv.NGAYCONG = 0;

                    }
                    else
                    {
                        bcctnv.NGAYPHEP = 0.5;
                        bcctnv.NGAYCONG = 0.5;

                    }
                    break;

                case "VR":
                    if (_valueNgayNghi == "NN")
                    {
                        bcctnv.NGAYPHEP = 1;
                        bcctnv.NGAYCONG = 0;

                    }
                    else
                    {
                        bcctnv.NGAYPHEP = 0.5;
                        bcctnv.NGAYCONG = 0.5;
                    }
                    break;

                default:
                    break;
            }

            // Update tb_BANGCONG_NV_CT
            _bcct_nv.Update(bcctnv);

            // Tính lại tổng các ngày: ngày phép, ngày công, ngày vắng,...
            double tongngaycong = _bcct_nv.tongNgayCong(_makycong, _manv);
            double tongngayphep = _bcct_nv.tongNgayPhep(_makycong, _manv);

            kcct.NGAYPHEP = tongngayphep;
            kcct.TONGNGAYCONG = tongngaycong;

            _kcct.Update(kcct);

            frmBCCC.loadBangCong();

            this.Close();
        }

        private void lbl_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cldNgayCong_DateSelected(object sender, DateRangeEventArgs e)
        {
            _cNgay = cldNgayCong.SelectionRange.Start.Day;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}