using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QUẢN_LÝ_THÔNG_TIN_SINH_VIÊN
{
    public partial class frmMain : Form
    {
        string flag;
        DataTable dtSV;
        int index;
        public frmMain()
        {
            InitializeComponent();
        }
        public DataTable createTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaSV");
            dt.Columns.Add("TenSV");
            dt.Columns.Add("Diem");
            dt.Columns.Add("Lop");
            return dt;
        }
        public void Lockcontrol()
        {
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;

            txtTenSV.ReadOnly = true;
            txtMaSV.ReadOnly = true;
            txtDiem.ReadOnly = true;
            txtLop.ReadOnly = true;

            btnThem.Focus();
        } 
                public void Unlockcontrol()
        {
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;

            txtTenSV.ReadOnly = false;
            txtMaSV.ReadOnly = false;
            txtDiem.ReadOnly = false;
            txtLop.ReadOnly = false;

            txtMaSV.Focus();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Lockcontrol();
            dtSV = createTable();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Unlockcontrol();
            flag = "add";

            txtMaSV.Text = "";
            txtTenSV.Text = "";
            txtDiem.Text = "";
            txtLop.Text = "";
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Unlockcontrol();
            flag = "edit";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if(flag == "add")
            {
                if(checkData())
                {
                    dtSV.Rows.Add(txtMaSV.Text, txtTenSV.Text, txtDiem.Text, txtLop.Text);
                    dataGridSinhVien.DataSource = dtSV;
                    dataGridSinhVien.RefreshEdit();
                }
            }
            else if(flag=="edit")
            {
                if (checkData())
                {
                    dtSV.Rows[index][0] = txtMaSV.Text;
                    dtSV.Rows[index][1] = txtTenSV.Text;
                    dtSV.Rows[index][2] = txtDiem.Text;
                    dtSV.Rows[index][3] = txtLop.Text;
                    dataGridSinhVien.DataSource = dtSV;
                    dataGridSinhVien.RefreshEdit();
                }
            }
            Lockcontrol();
        }
        public bool checkData()
        {
            if(string.IsNullOrWhiteSpace(txtMaSV.Text))
            {
                MessageBox.Show("bạn chưa nhập mã sinh viên", "thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaSV.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTenSV.Text))
            {
                MessageBox.Show("bạn chưa nhập tên sinh viên", "thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenSV.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtDiem.Text))
            {
                MessageBox.Show("bạn chưa nhập điểm", "thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiem.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtLop.Text))
            {
                MessageBox.Show("bạn chưa nhập lớp", "thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLop.Focus();
                return false;
            }
            return true;
        }

        private void dataGridSinhVien_SelectionChanged(object sender, EventArgs e)
        {
            index = dataGridSinhVien.CurrentCell.RowIndex;
            DataTable dt = (DataTable)dataGridSinhVien.DataSource;
            if (dt.Rows.Count > 0)
            {
                txtMaSV.Text = dataGridSinhVien.Rows[index].Cells[0].Value.ToString();
                txtTenSV.Text = dataGridSinhVien.Rows[index].Cells[1].Value.ToString();
                txtDiem.Text = dataGridSinhVien.Rows[index].Cells[2].Value.ToString();
                txtLop.Text = dataGridSinhVien.Rows[index].Cells[3].Value.ToString();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("bạn có muốn xóa sinh viên này?","cảnh báo",MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                dtSV.Rows.RemoveAt(index);
                dataGridSinhVien.DataSource = dtSV;
                dataGridSinhVien.RefreshEdit();
            }
        }
    }
}
