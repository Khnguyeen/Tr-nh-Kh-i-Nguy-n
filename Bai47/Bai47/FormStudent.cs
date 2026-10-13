
using System;
using System.Data;
using System.ComponentModel;
using System.Windows.Forms;

namespace Bai47
{
    public class FormStudent : Form
    {
        private DataTable dt;
        private DataGridView dgv;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Action<string>? BaoTrangThai { get; set; }

        public FormStudent()
        {
            Text = "Quan ly Sinh vien";
            Width = 700;
            Height = 400;

            dt = new DataTable();
            dt.Columns.Add("MaSV");
            dt.Columns.Add("HoTen");
            dt.Columns.Add("Lop");

            dt.Rows.Add("SV01", "Nguyen Van A", "DCT01");
            dt.Rows.Add("SV02", "Tran Van B", "DCT02");

            dgv = new DataGridView();
            dgv.Dock = DockStyle.Fill;
            dgv.DataSource = dt;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AllowUserToAddRows = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            Button btnThem = new Button();
            btnThem.Text = "Them sinh vien";
            btnThem.Dock = DockStyle.Top;
            btnThem.Height = 35;

            btnThem.Click += (s, e) =>
            {
                try
                {
                    string ma = "SV" + (dt.Rows.Count + 1).ToString("D2");
                    dt.Rows.Add(ma, "Sinh vien moi", "DCT01");
                    BaoTrangThai?.Invoke("Them sinh vien thanh cong");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Loi them: " + ex.Message);
                }
            };

            ContextMenuStrip menu = new ContextMenuStrip();

            menu.Items.Add("Xoa dong", null, (s, e) =>
            {
                try
                {
                    if (dgv.SelectedRows.Count > 0)
                    {
                        dgv.Rows.Remove(dgv.SelectedRows[0]);
                        BaoTrangThai?.Invoke("Xoa sinh vien thanh cong");
                    }
                    else
                    {
                        MessageBox.Show("Hay chon dong can xoa");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Loi xoa: " + ex.Message);
                }
            });

            menu.Items.Add("Lam moi du lieu", null, (s, e) =>
            {
                dgv.Refresh();
                BaoTrangThai?.Invoke("Da lam moi du lieu");
            });

            menu.Items.Add("Xem chi tiet", null, (s, e) =>
            {
                if (dgv.SelectedRows.Count > 0)
                {
                    DataGridViewRow r = dgv.SelectedRows[0];

                    MessageBox.Show(
                        "Ma SV: " + r.Cells["MaSV"].Value +
                        "\nHo ten: " + r.Cells["HoTen"].Value +
                        "\nLop: " + r.Cells["Lop"].Value,
                        "Chi tiet sinh vien");
                }
                else
                {
                    MessageBox.Show("Hay chon sinh vien");
                }
            });

            dgv.ContextMenuStrip = menu;

            Controls.Add(dgv);
            Controls.Add(btnThem);
        }
    }
}
