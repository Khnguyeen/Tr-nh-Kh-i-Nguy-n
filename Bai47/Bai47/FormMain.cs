
using System;
using System.Windows.Forms;

namespace Bai47
{
    public class FormMain : Form
    {
        private ToolStripStatusLabel lblThongBao;
        private ToolStripStatusLabel lblThoiGian;
        private System.Windows.Forms.Timer timer;

        public FormMain()
        {
            Text = "He thong quan ly";
            Width = 1000;
            Height = 650;
            IsMdiContainer = true;
            StartPosition = FormStartPosition.CenterScreen;

            MenuStrip menu = new MenuStrip();

            ToolStripMenuItem heThong = new ToolStripMenuItem("He thong");
            ToolStripMenuItem dangXuat = new ToolStripMenuItem("Dang xuat");
            ToolStripMenuItem thoat = new ToolStripMenuItem("Thoat");

            dangXuat.Click += (s, e) =>
            {
                foreach (Form f in MdiChildren)
                    f.Close();

                CapNhatTrangThai("Da dang xuat");
            };

            thoat.Click += (s, e) => Close();

            heThong.DropDownItems.Add(dangXuat);
            heThong.DropDownItems.Add(thoat);

            ToolStripMenuItem danhMuc = new ToolStripMenuItem("Danh muc");
            ToolStripMenuItem sinhVien = new ToolStripMenuItem("Quan ly Sinh vien");
            ToolStripMenuItem sanPham = new ToolStripMenuItem("Quan ly San pham");

            sinhVien.Click += (s, e) => MoSinhVien();
            sanPham.Click += (s, e) => MoSanPham();

            danhMuc.DropDownItems.Add(sinhVien);
            danhMuc.DropDownItems.Add(sanPham);

            ToolStripMenuItem cuaSo = new ToolStripMenuItem("Cua so");
            ToolStripMenuItem cascade = new ToolStripMenuItem("Cascade");
            ToolStripMenuItem ngang = new ToolStripMenuItem("Tile Horizontal");
            ToolStripMenuItem doc = new ToolStripMenuItem("Tile Vertical");

            cascade.Click += (s, e) => LayoutMdi(MdiLayout.Cascade);
            ngang.Click += (s, e) => LayoutMdi(MdiLayout.TileHorizontal);
            doc.Click += (s, e) => LayoutMdi(MdiLayout.TileVertical);

            cuaSo.DropDownItems.Add(cascade);
            cuaSo.DropDownItems.Add(ngang);
            cuaSo.DropDownItems.Add(doc);

            menu.Items.Add(heThong);
            menu.Items.Add(danhMuc);
            menu.Items.Add(cuaSo);

            ToolStrip tool = new ToolStrip();

            ToolStripButton btnSV = new ToolStripButton("Sinh vien");
            ToolStripButton btnSP = new ToolStripButton("San pham");

            btnSV.Click += (s, e) => MoSinhVien();
            btnSP.Click += (s, e) => MoSanPham();

            tool.Items.Add(btnSV);
            tool.Items.Add(btnSP);

            StatusStrip status = new StatusStrip();

            lblThongBao = new ToolStripStatusLabel("San sang");
            lblThoiGian = new ToolStripStatusLabel();

            status.Items.Add(new ToolStripStatusLabel("Nguoi dung: Admin"));
            status.Items.Add(new ToolStripStatusLabel("|"));
            status.Items.Add(lblThongBao);
            status.Items.Add(new ToolStripStatusLabel("|"));
            status.Items.Add(lblThoiGian);

            Controls.Add(menu);
            Controls.Add(tool);
            Controls.Add(status);

            MainMenuStrip = menu;
            menu.Dock = DockStyle.Top;
            tool.Dock = DockStyle.Top;
            status.Dock = DockStyle.Bottom;

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += (s, e) =>
            {
                lblThoiGian.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            };
            timer.Start();
        }

        private void MoSinhVien()
        {
            try
            {
                foreach (Form f in MdiChildren)
                {
                    if (f is FormStudent)
                    {
                        f.Activate();
                        return;
                    }
                }

                FormStudent form = new FormStudent();
                form.BaoTrangThai = CapNhatTrangThai;
                form.MdiParent = this;
                form.Show();

                CapNhatTrangThai("Da mo quan ly sinh vien");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi mo sinh vien: " + ex.Message);
            }
        }

        private void MoSanPham()
        {
            try
            {
                foreach (Form f in MdiChildren)
                {
                    if (f is FormProduct)
                    {
                        f.Activate();
                        return;
                    }
                }

                FormProduct form = new FormProduct();
                form.BaoTrangThai = CapNhatTrangThai;
                form.MdiParent = this;
                form.Show();

                CapNhatTrangThai("Da mo quan ly san pham");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi mo san pham: " + ex.Message);
            }
        }

        public void CapNhatTrangThai(string noiDung)
        {
            lblThongBao.Text = noiDung;
        }
    }
}
