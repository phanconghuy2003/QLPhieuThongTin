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
    public partial class frmDashBoard : Form
    {
        public frmDashBoard()
        {
            InitializeComponent();
            btn_QLTuyenDung.FlatStyle = FlatStyle.Flat;
            btn_QLTuyenDung.FlatAppearance.BorderSize = 0;
            btn_QLUngVien.FlatStyle = FlatStyle.Flat;
            btn_QLUngVien.FlatAppearance.BorderSize = 0;
            btn_SaoLuuPhucHoi.FlatStyle = FlatStyle.Flat;
            btn_SaoLuuPhucHoi.FlatAppearance.BorderSize = 0;
        }

        private void changBackgroundClick(Button _click)
        {
            Button[] _btn = new Button[] { btn_QLTuyenDung, btn_QLUngVien, btn_SaoLuuPhucHoi };
            foreach (Button i in _btn)
            {
                i.BackColor = Color.FromArgb(60, 112, 253);
            }
            _click.BackColor = Color.FromArgb(42, 98, 250);
        }

        private void icon_hide_Click(object sender, EventArgs e)
        {
            if(panel_navbar.Width != 0)
            {
                icon_hide.Location = new Point(3, -1);
                panel_navbar.Width = 0;
                panel_master.Location = new Point(49, -1);
                panel_master.Width = panel_master.Width + 262;
                pic_bgindex.Location = new Point(186, 188);
            }
            else{
                icon_hide.Location = new Point(196, -1);
                panel_navbar.Width = 195;
                panel_master.Location = new Point(242, -1);
                panel_master.Width = panel_master.Width - 262;
                pic_bgindex.Location = new Point(106, 188);
            }
        }

        private void btn_QLTuyenDung_Click(object sender, EventArgs e)
        {
            changBackgroundClick(btn_QLTuyenDung);
            frmQLTuyenDung _frmTuyenDung = new frmQLTuyenDung();
            _frmTuyenDung.TopLevel = false;
            panel_master.Controls.Clear();
            panel_master.Controls.Add(_frmTuyenDung);
            _frmTuyenDung.Show();
        }

        private void btn_QLUngVien_Click(object sender, EventArgs e)
        {
            changBackgroundClick(btn_QLUngVien);
            frmQLUngVien _frmUngVien = new frmQLUngVien();
            _frmUngVien.TopLevel = false;
            panel_master.Controls.Clear();
            panel_master.Controls.Add(_frmUngVien);
            _frmUngVien.Show();
        }

        private void btn_SaoLuuPhucHoi_Click(object sender, EventArgs e)
        {
            changBackgroundClick(btn_SaoLuuPhucHoi);
            frmSaoLuuPhucHoi _frmSaoLuuPhucHoi = new frmSaoLuuPhucHoi();
            _frmSaoLuuPhucHoi.TopLevel = false;
            panel_master.Controls.Clear();
            panel_master.Controls.Add(_frmSaoLuuPhucHoi);
            _frmSaoLuuPhucHoi.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
