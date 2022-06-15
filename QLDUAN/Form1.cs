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
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void btndangnhap_Click(object sender, EventArgs e)
        {
            bool Ok = false;
            // SqlConnection: dùng để tạo đối tượng
            SqlConnection connn = new SqlConnection(@"Data Source=DESKTOP-M3IMJK3\SQLEXPRESS;Initial Catalog=QLHV;Integrated Security=True");
            SqlDataReader rdr = null;
            try
            {
                connn.Open();
                // SqlCommand: Xác định các thao tác cần xử lí đối với dữ liệu
                SqlCommand cmd = new SqlCommand("select * from NguoiDung", connn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (txtusernam.Text.Trim() == rdr["Tendangnhap"].ToString().Trim() && txtpassword.Text == rdr["Matkhau"].ToString().Trim())
                    {
                        Ok = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi kết nối cơ sở dữ liệu");
                return;
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (connn != null)
                {
                    connn.Close();
                }
            }
            if (Ok == false)
            {
                MessageBox.Show("Tên đăng nhập mật khẩu không hợp lệ.");
            }
            else
            {
                frmQuanLiMatHang f = new frmQuanLiMatHang();
                f.Show();
            }
        }

        private void btndangki_Click(object sender, EventArgs e)
        {
            frmDangKy f = new frmDangKy();
            f.Show();
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        { 
        }
    }
}
