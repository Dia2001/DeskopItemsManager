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
    public partial class frmQuanLiMatHang : Form
    {
        quanlimathangDataContext db = new quanlimathangDataContext();
        Table<MatHang> matHangs;
        Table<frmNhaCungCap> nhaCungCaps;
        Table<LoaiHang> loaiHangs;
        public frmQuanLiMatHang()
        {
            InitializeComponent();
        }

        private void btnloaihang_Click(object sender, EventArgs e)
        {
            frmLoaiHang f = new frmLoaiHang();
            f.Show();
        }
        public void loadNhaCungCap()
        {
            nhaCungCaps = db.GetTable<frmNhaCungCap>();
            var query = from ncc in nhaCungCaps
                        select new
                        {
                            MaCT = ncc.MaCongTy,
                            TenCT = ncc.TenCongTy
                        };
            cbtencongty.DataSource = query;
            cbtencongty.DisplayMember = "TenCT";
            cbtencongty.ValueMember = "MaCT";
        }   
        public void loadLoaiHang()
        {
            loaiHangs = db.GetTable<LoaiHang>();
            var query = from lh in loaiHangs
                        select new
                        {
                            MaLH = lh.MaLoaihang,
                            TenLH = lh.TenLoaihang
                        };
            cbtenloaihang.DataSource = query;
            cbtenloaihang.DisplayMember = "TenLH";
            cbtenloaihang.ValueMember = "MaLH";
        }
      
        public void loadDaTaGridView()
        {
            matHangs = db.GetTable<MatHang>();
            var query = from mh in matHangs
                        select mh;
            dataGridView1.DataSource = query;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            txtmamathangcantim.Clear();
        }

        private void frmQuanLiMatHang_Click(object sender, EventArgs e)
        {

        }

        private void txtmahang_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmQuanLiMatHang_Load(object sender, EventArgs e)
        {
            loadNhaCungCap();
            loadLoaiHang();
            loadDaTaGridView();
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dataGridView1.CurrentCell.RowIndex;
            txtmahang.Text= dataGridView1.Rows[i].Cells[0].Value.ToString();
            txttenhang.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            cbtencongty.SelectedValue = dataGridView1.Rows[i].Cells[2].Value.ToString();
            cbtenloaihang.SelectedValue = dataGridView1.Rows[i].Cells[3].Value.ToString();
            txtsoluong.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
            txtgiahang.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            MatHang mh = new MatHang();
            mh.MaHang = txtmahang.Text;
            mh.TenHang = txttenhang.Text;
            mh.MaCongTy = cbtencongty.SelectedValue.ToString();
            mh.MaLoaiHang = cbtenloaihang.SelectedValue.ToString();
            mh.SoLuong = Convert.ToInt32(txtsoluong.Text);
            mh.GiaHang = Convert.ToDecimal(txtgiahang.Text);
            matHangs.InsertOnSubmit(mh);
            db.SubmitChanges();
            loadDaTaGridView();
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            try
            {
                MatHang mh = new MatHang();
                mh = db.MatHangs.Where(m => m.MaHang == txtmahang.Text).SingleOrDefault();
                mh.MaHang = txtmahang.Text;
                mh.TenHang = txttenhang.Text;
                mh.MaCongTy = cbtencongty.SelectedValue.ToString();
                mh.MaLoaiHang = cbtenloaihang.SelectedValue.ToString();
                mh.SoLuong = Convert.ToInt32(txtsoluong.Text);
                mh.GiaHang = Convert.ToDecimal(txtgiahang.Text);
                db.SubmitChanges();
                loadDaTaGridView();
            }
            catch(Exception e1)
            {
                MessageBox.Show("Mã nhân viên bị trùng");
            }
        }

        private void btnthongke_Click(object sender, EventArgs e)
        {
            matHangs = db.GetTable<MatHang>();
            loaiHangs = db.GetTable<LoaiHang>();
            var query = from mh in matHangs
                        from lh in loaiHangs
                        where mh.MaLoaiHang == lh.MaLoaihang
                        group lh by lh.TenLoaihang into b
                        orderby b.Count() descending
                        let TenLH = b.Key
                        let SoLuong = b.Count()
                        select new
                        {
                            TenLH,
                            SoLuong,
                        };
            dataGridView1.DataSource = query;
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            // DialogResult dlr = MessageBox.Show("Thông báo hiển thị lên","tên cửa sổ",chọn thực hiện hay không,)
            DialogResult dlr = MessageBox.Show("Bạn có chắc muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {

                matHangs = db.GetTable<MatHang>();
                var query = from mh in matHangs
                            where mh.MaHang == txtmahang.Text
                            select mh;
                foreach (var mh in query)
                {
                    db.MatHangs.DeleteOnSubmit(mh);
                }
                db.SubmitChanges();
                loadDaTaGridView();
            }
        }

        private void btntim_Click(object sender, EventArgs e)
        {
            matHangs = db.GetTable<MatHang>();
            var query = from mh in matHangs
                        where mh.MaHang == txtmamathangcantim.Text
                        select mh;
            dataGridView1.DataSource = query;
        }

        private void btnnhacungcap_Click(object sender, EventArgs e)
        {
            frmnhacungcap f = new frmnhacungcap();
            f.Show();
        }
    }
}
