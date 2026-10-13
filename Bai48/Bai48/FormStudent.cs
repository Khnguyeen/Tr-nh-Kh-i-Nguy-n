
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai48
{
    public class FormStudent : Form
    {
        private Button btnNap;
        private ProgressBar progressBar;
        private Label lblPhanTram;
        private ListBox lstSinhVien;
        private FormMain formCha;

        public FormStudent(FormMain parent)
        {
            formCha = parent;

            Text = "Nap du lieu sinh vien";
            Width = 600;
            Height = 450;
            StartPosition = FormStartPosition.CenterScreen;

            btnNap = new Button();
            btnNap.Text = "Nap du lieu";
            btnNap.Location = new Point(20, 20);
            btnNap.Size = new Size(120, 35);
            btnNap.Click += async (s, e) => await NapDuLieu();

            progressBar = new ProgressBar();
            progressBar.Location = new Point(20, 75);
            progressBar.Size = new Size(400, 25);
            progressBar.Minimum = 0;
            progressBar.Maximum = 100;

            lblPhanTram = new Label();
            lblPhanTram.Text = "0%";
            lblPhanTram.Location = new Point(435, 78);
            lblPhanTram.AutoSize = true;

            lstSinhVien = new ListBox();
            lstSinhVien.Location = new Point(20, 120);
            lstSinhVien.Size = new Size(520, 250);

            Controls.Add(btnNap);
            Controls.Add(progressBar);
            Controls.Add(lblPhanTram);
            Controls.Add(lstSinhVien);
        }

        private async Task<List<string>> TaiDuLieuAsync(
            IProgress<int> progress)
        {
            return await Task.Run(async () =>
            {
                List<string> danhSach = new List<string>();

                for (int i = 1; i <= 100; i++)
                {
                    await Task.Delay(50);

                    danhSach.Add("Sinh vien " + i);

                    int phanTram = i;
                    progress.Report(phanTram);
                }

                return danhSach;
            });
        }

        private async Task NapDuLieu()
        {
            btnNap.Enabled = false;
            progressBar.Value = 0;
            lblPhanTram.Text = "0%";
            lstSinhVien.Items.Clear();

            formCha.BaoTrangThai.Text = "Dang nap du lieu...";

            try
            {
                IProgress<int> progress = new Progress<int>(phanTram =>
                {
                    progressBar.Value = phanTram;
                    lblPhanTram.Text = phanTram + "%";
                    formCha.BaoTrangThai.Text =
                        "Tien trinh: " + phanTram + "%";
                });

                List<string> danhSach =
                    await TaiDuLieuAsync(progress);

                lstSinhVien.Items.AddRange(danhSach.ToArray());

                formCha.BaoTrangThai.Text =
                    "Nap thanh cong " + danhSach.Count + " sinh vien";

                MessageBox.Show(
                    "Nap du lieu thanh cong!",
                    "Thong bao",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                formCha.BaoTrangThai.Text = "Nap du lieu that bai";

                MessageBox.Show(
                    "Loi: " + ex.Message,
                    "Thong bao loi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnNap.Enabled = true;
            }
        }
    }
}
