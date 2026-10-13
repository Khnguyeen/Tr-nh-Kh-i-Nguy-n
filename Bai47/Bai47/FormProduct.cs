
using System;
using System.Data;
using System.ComponentModel;
using System.Windows.Forms;

namespace Bai47
{
    public class FormProduct : Form
    {
        private DataTable dt;
        private DataGridView dgv;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Action<string>? BaoTrangThai { get; set; }

        public FormProduct()
        {
            Text = "Quan ly San pham";
            Width = 700;
            Height = 400;

            dt = new DataTable();
            dt.Columns.Add("MaSP");
            dt.Columns.Add("TenSP");
            dt.Columns.Add("DonGia");

            dt.Rows.Add("SP01", "Ban phim", "250000");
            dt.Rows.Add("SP02", "Chuot may tinh", "150000");

            dgv = new DataGridView();
            dgv.Dock = DockStyle.Fill;
            dgv.DataSource = dt;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AllowUserToAddRows = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            Button btnThem = new Button();
            btnThem.Text = "Them san pham";
            btnThem.Dock = DockStyle.Top;
            btnThem.Height = 35;

            btnThem.Click += (s, e) =>
            {
                try
                {
                    string ma = "SP" + (dt.Rows.Count + 1).ToString("D2");
                    dt.Rows.Add(ma, "San pham moi", "100000");
                    BaoTrangThai?.Invoke("Them san pham thanh cong");
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
                        BaoTrangThai?.Invoke("Xoa san pham thanh cong");
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
                        "Ma SP: " + r.Cells["MaSP"].Value +
                        "\nTen SP: " + r.Cells["TenSP"].Value +
                        "\nDon gia: " + r.Cells["DonGia"].Value,
                        "Chi tiet san pham");
                }
                else
                {
                    MessageBox.Show("Hay chon san pham");
                }
            });

            dgv.ContextMenuStrip = menu;

            Controls.Add(dgv);
            Controls.Add(btnThem);
        }
    }
}
