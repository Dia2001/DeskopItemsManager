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
    public partial class frmLoaiHang : Form
    {
        quanlimathangDataContext db = new quanlimathangDataContext();
        Table<LoaiHang> loaiHangs;
        public frmLoaiHang()
        {
            InitializeComponent();
        }

        private void frmLoaiHang_Load(object sender, EventArgs e)
        {
            loadDataGridview();
        }
        public void loadDataGridview()
        {
            loaiHangs = db.GetTable<LoaiHang>();
            var query = from lh in loaiHangs
                        select lh;
            dataGridView1.DataSource = query;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dataGridView1.CurrentCell.RowIndex;
            txtmaloaihang.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            txttenloaihang.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            LoaiHang lh = new LoaiHang();
            lh.MaLoaihang = txtmaloaihang.Text;
            lh.TenLoaihang = txttenloaihang.Text;
            loaiHangs.InsertOnSubmit(lh);
            db.SubmitChanges();
            loadDataGridview();
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            LoaiHang lh = new LoaiHang();
            lh = db.LoaiHangs.Where(a => a.MaLoaihang == txtmaloaihang.Text).SingleOrDefault();
            lh.MaLoaihang = txtmaloaihang.Text;
            lh.TenLoaihang = txttenloaihang.Text;
            db.SubmitChanges();
            loadDataGridview();
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            // DialogResult dlr = MessageBox.Show("Thông báo hiển thị lên","tên cửa sổ",chọn thực hiện hay không,)
            DialogResult dlr = MessageBox.Show("Bạn có chắc muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {

                loaiHangs = db.GetTable<LoaiHang>();
                var query = from lh in loaiHangs
                            where lh.MaLoaihang == txtmaloaihang.Text
                            select lh;
                foreach (var lh in query)
                {
                    db.LoaiHangs.DeleteOnSubmit(lh);
                }
                db.SubmitChanges();
                loadDataGridview();
            }
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnquaylai_Click(object sender, EventArgs e)
        {
            frmQuanLiMatHang f = new frmQuanLiMatHang();
            f.Show();
        }
    }
}
