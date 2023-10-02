using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyTuyenDungNhanVien
{
    public partial class frmQLUngVien : Form
    {
        KetNoi ketNoi = new KetNoi();

        BindingSource bindingSource = new BindingSource();
        List<UngVien> danhSachUngVien = new List<UngVien>();
        public frmQLUngVien()
        {
            InitializeComponent();
        }

        private void btn_xemMaVTTD_Click(object sender, EventArgs e)
        {
            string maVTTD = txt_maVTTD.Text;
            List<UngVien> danhSachUngVien = ketNoi.FillThongTinUngVienTheoMaTTD(maVTTD);
            if (danhSachUngVien != null && danhSachUngVien.Count > 0)
            {
                bindingSource.DataSource = danhSachUngVien; // Gán danh sách vào BindingSource
                dgv_dsUngVien.DataSource = bindingSource; // Gán BindingSource vào DataGridView
                this.dgv_dsUngVien.DefaultCellStyle.ForeColor = Color.Black;
            }
            else
            {
                MessageBox.Show("Không tìm thấy ứng viên.");
            }
        }

        private void frmQLUngVien_Load(object sender, EventArgs e)
        {
            List<string> danhSachTenVTTD = ketNoi.LayDanhSachTenVTTD();
            cbb_tenVTTD.DataSource = danhSachTenVTTD;
        }

        private void btn_xemTenVTTD_Click(object sender, EventArgs e)
        {
            string tenVTTD = cbb_tenVTTD.SelectedValue.ToString();
            List<UngVien> danhSachUngVien = ketNoi.FillThongTinUngVienTheoTenTTD(tenVTTD);
            if (danhSachUngVien != null && danhSachUngVien.Count > 0)
            {
                bindingSource.DataSource = danhSachUngVien; // Gán danh sách vào BindingSource
                dgv_dsUngVien.DataSource = bindingSource; // Gán BindingSource vào DataGridView
                this.dgv_dsUngVien.DefaultCellStyle.ForeColor = Color.Black;
            }
            else
            {
                MessageBox.Show("Không tìm thấy ứng viên.");
            }
        }

        private void dgv_dsUngVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgv_dsUngVien.Rows.Count)
            {
                UngVien selectedUngVien = dgv_dsUngVien.Rows[e.RowIndex].DataBoundItem as UngVien;
                txt_hoTenUV.Text = selectedUngVien.hoTen;
                txt_cccd.Text = selectedUngVien.soCCCD;
                txt_ngaySinh.Text = selectedUngVien.ngaySinh.ToString();
                txt_soDienThoai.Text = selectedUngVien.soDienThoai;
                txt_email.Text = selectedUngVien.email;
                txt_chiTietKyNang.Text = selectedUngVien.chiTietKyNang;
                txt_kinhNghiemLamViec.Text = selectedUngVien.kinhNghiemLamViec;
                txt_cacKyNang.Text = string.Join(", ", selectedUngVien.cacKyNang);
                txt_mucTieuCaNhan.Text = selectedUngVien.mucTieuCaNhan;
            }
        }

        private void btn_xoaHoSo_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có hàng nào được chọn không
            if (dgv_dsUngVien.SelectedRows.Count > 0)
            {
                string soCCCD = dgv_dsUngVien.SelectedRows[0].Cells["SoCCCD"].Value.ToString();

                int result = ketNoi.xoaUngVien(soCCCD);

                if (result == 1)
                {
                    danhSachUngVien.Remove(danhSachUngVien.FirstOrDefault(u => u.soCCCD == soCCCD)); // Xóa ứng viên khỏi danh sách
                    bindingSource.DataSource = danhSachUngVien; // Cập nhật BindingSource
                    dgv_dsUngVien.Refresh(); // Làm mới DataGridView để hiển thị dữ liệu mới

                    MessageBox.Show("Xóa ứng viên thành công.");
                }
                else
                {
                    MessageBox.Show("Xóa ứng viên không thành công.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn ứng viên cần xóa.");
            }
        }
    }
}
