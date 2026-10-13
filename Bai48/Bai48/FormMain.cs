
using System;
using System.Windows.Forms;

namespace Bai48
{
    public class FormMain : Form
    {
        public ToolStripStatusLabel BaoTrangThai;

        public FormMain()
        {
            Text = "Bai 4.8 - Async Await";
            Width = 1000;
            Height = 650;
            IsMdiContainer = true;
            StartPosition = FormStartPosition.CenterScreen;

            MenuStrip menu = new MenuStrip();

            ToolStripMenuItem menuSinhVien =
                new ToolStripMenuItem("Quan ly");

            ToolStripMenuItem moSinhVien =
                new ToolStripMenuItem("Danh sach sinh vien");

            moSinhVien.Click += (s, e) =>
            {
                foreach (Form f in MdiChildren)
                {
                    if (f is FormStudent)
                    {
                        f.Activate();
                        return;
                    }
                }

                FormStudent form = new FormStudent(this);
                form.MdiParent = this;
                form.Show();
            };

            menuSinhVien.DropDownItems.Add(moSinhVien);
            menu.Items.Add(menuSinhVien);
            MainMenuStrip = menu;
            Controls.Add(menu);

            StatusStrip status = new StatusStrip();
            BaoTrangThai = new ToolStripStatusLabel("San sang");
            status.Items.Add(BaoTrangThai);
            Controls.Add(status);
        }
    }
}
