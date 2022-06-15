using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Linq;
namespace QLDUAN
{
    public partial class frmnhacungcap : Form
    {
        quanlimathangDataContext db = new quanlimathangDataContext();
        Table<frmNhaCungCap> NhaCungCaps;
        public frmnhacungcap()
        {
            InitializeComponent();
        }
        private void btnthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnquaylai_Click(object sender, EventArgs e)
        {
            frmQuanLiMatHang f = new frmQuanLiMatHang();
            f.Close();
        }
        public void loadDataGridView()
        {
            NhaCungCaps = db.GetTable<frmNhaCungCap>();
            var query = from ncc in NhaCungCaps
                        select ncc;
            dataGridView1.DataSource = query;
        }
        private void frmnhacungcap_Load(object sender, EventArgs e)
        {
            loadDataGridView();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dataGridView1.CurrentCell.RowIndex;
            txtmacongty.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            txttencongty.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            txttengiaodich.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            txtdiachi.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            txtdienthoai.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
            txtemail.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            frmNhaCungCap ncc = new frmNhaCungCap();
            ncc.MaCongTy = txtmacongty.Text;
            ncc.TenCongTy = txttencongty.Text;
            ncc.TenGiaoDich = txttengiaodich.Text;
            ncc.DiaChi = txtdiachi.Text;
            ncc.DienThoai = txtdienthoai.Text;
            ncc.Email = txtemail.Text;
            NhaCungCaps.InsertOnSubmit(ncc);
            db.SubmitChanges();
            loadDataGridView();
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            try
            {
                frmNhaCungCap ncc = new frmNhaCungCap();
                ncc = db.NhaCungCaps.Where(a => a.MaCongTy == txtmacongty.Text).SingleOrDefault();
                ncc.MaCongTy = txtmacongty.Text;
                ncc.TenCongTy = txttencongty.Text;
                ncc.TenGiaoDich = txttengiaodich.Text;
                ncc.DiaChi = txtdiachi.Text;
                ncc.DienThoai = txtdienthoai.Text;
                ncc.Email = txtemail.Text;
                db.SubmitChanges();
                loadDataGridView();
            }
            catch(Exception e1){
                MessageBox.Show("Mã nhà cung cấp bị trùng");
            }
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            // DialogResult dlr = MessageBox.Show("Thông báo hiển thị lên","tên cửa sổ",chọn thực hiện hay không,)
            DialogResult dlr = MessageBox.Show("Bạn có chắc muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                NhaCungCaps= db.GetTable<frmNhaCungCap>();
                var query = from ncc in NhaCungCaps
                            where ncc.MaCongTy==txtmacongty.Text
                            select ncc;
                foreach (var nv in query)
                {
                    db.NhaCungCaps.DeleteOnSubmit(nv);
                }
                //  SubmitChanges(): thay doi du lieu tren datacontex
                db.SubmitChanges();
                loadDataGridView();
            }
        }
    }
}
