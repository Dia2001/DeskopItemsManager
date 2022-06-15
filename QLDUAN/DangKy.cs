using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace QLDUAN
{
    public partial class frmDangKy : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        string s = @"Data Source=DESKTOP-M3IMJK3\SQLEXPRESS;Initial Catalog=QLHV;Integrated Security=True";
        public frmDangKy()
        {
            InitializeComponent();
        }

        private void bitdangki_Click(object sender, EventArgs e)
        {
            if (txttendangnhap.Text != ""&& txtmatkhau.Text != ""&& txtnhaplaimatkhau.Text != "")
            {
                if (txtmatkhau.Text.Equals(txtnhaplaimatkhau.Text))
                {
                    conn = new SqlConnection(s);
                    conn.Open();
                    string ten = txttendangnhap.Text;
                    string pass = txtmatkhau.Text;
                    string sql="insert into NguoiDung values('"+ten+"','"+pass+"')";
                    cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Đăng kí thành công.");
                }
                else
                {
                    MessageBox.Show("Nhập mật khẩu không đúng");
                    txtnhaplaimatkhau.Focus();
                    txtnhaplaimatkhau.SelectAll();
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa nhập đủ thông tin vui lòng nhập lại.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmDangNhap f = new frmDangNhap();
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txttendangnhap.Clear();
            txtmatkhau.Clear();
            txtnhaplaimatkhau.Clear();
        }
    }
}
