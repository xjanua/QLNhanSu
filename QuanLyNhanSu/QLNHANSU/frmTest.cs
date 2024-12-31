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
using BusinessLayer;

namespace QLNHANSU
{
    public partial class frmTest : DevExpress.XtraEditors.XtraForm
    {
        public frmTest()
        {
            InitializeComponent();
        }

        private void frmTest_Load(object sender, EventArgs e)
        {
            TRINHDO td = new TRINHDO();
            gridControl1.DataSource = td.getList();
        }
    }
}