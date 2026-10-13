
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace QuanLySinhVien
{
    public partial class Form1 : Form
    {
        private BindingList<Student> students =
            new BindingList<Student>();

        private BindingSource bindingSource =
            new BindingSource();

        private DataGridView dgvStudents = new DataGridView();
        private TextBox txtStudentId = new TextBox();
        private TextBox txtFullName = new TextBox();
        private TextBox txtAge = new TextBox();
        private ComboBox cboGender = new ComboBox();
        private PictureBox picAvatar = new PictureBox();

        private Button btnChooseAvatar = new Button();
        private Button btnAdd = new Button();
        private Button btnDelete = new Button();
        private Button btnUpdate = new Button();
        private Button btnExportCsv = new Button();

        public Form1()
        {
            InitializeComponent();

            TaoGiaoDien();
            KhoiTaoDuLieu();
        }

        private void TaoGiaoDien()
        {
            this.Text = "Quản lý sinh viên";
            this.Size = new Size(1050, 650);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblId = new Label();
            lblId.Text = "Mã sinh viên";
            lblId.SetBounds(20, 20, 100, 25);

            txtStudentId.SetBounds(125, 20, 200, 25);

            Label lblName = new Label();
            lblName.Text = "Họ tên";
            lblName.SetBounds(20, 60, 100, 25);

            txtFullName.SetBounds(125, 60, 200, 25);

            Label lblAge = new Label();
            lblAge.Text = "Tuổi";
            lblAge.SetBounds(20, 100, 100, 25);

            txtAge.SetBounds(125, 100, 200, 25);

            Label lblGender = new Label();
            lblGender.Text = "Giới tính";
            lblGender.SetBounds(20, 140, 100, 25);

            cboGender.SetBounds(125, 140, 200, 25);
            cboGender.DropDownStyle =
                ComboBoxStyle.DropDownList;

            picAvatar.SetBounds(370, 20, 180, 180);
            picAvatar.BorderStyle = BorderStyle.FixedSingle;
            picAvatar.SizeMode = PictureBoxSizeMode.Zoom;

            btnChooseAvatar.Text = "Chọn ảnh";
            btnChooseAvatar.SetBounds(390, 210, 130, 35);

            btnAdd.Text = "Thêm";
            btnAdd.SetBounds(20, 210, 90, 35);

            btnDelete.Text = "Xóa";
            btnDelete.SetBounds(120, 210, 90, 35);

            btnUpdate.Text = "Cập nhật";
            btnUpdate.SetBounds(220, 210, 110, 35);

            btnExportCsv.Text = "Xuất CSV";
            btnExportCsv.SetBounds(20, 255, 120, 35);

            dgvStudents.SetBounds(20, 310, 990, 280);
            dgvStudents.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvStudents.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvStudents.MultiSelect = false;
            dgvStudents.ReadOnly = true;
            dgvStudents.AllowUserToAddRows = false;

            this.Controls.AddRange(new Control[]
            {
                lblId, txtStudentId,
                lblName, txtFullName,
                lblAge, txtAge,
                lblGender, cboGender,
                picAvatar, btnChooseAvatar,
                btnAdd, btnDelete, btnUpdate,
                btnExportCsv, dgvStudents
            });

            btnAdd.Click += btnAdd_Click;
            btnDelete.Click += btnDelete_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnChooseAvatar.Click += btnChooseAvatar_Click;
            btnExportCsv.Click += btnExportCsv_Click;
            dgvStudents.SelectionChanged +=
                dgvStudents_SelectionChanged;
            dgvStudents.DataBindingComplete +=
                dgvStudents_DataBindingComplete;
        }

        private void KhoiTaoDuLieu()
        {
            cboGender.Items.Add("Nam");
            cboGender.Items.Add("Nữ");

            bindingSource.DataSource = students;
            dgvStudents.DataSource = bindingSource;

            txtStudentId.DataBindings.Add(
                "Text", bindingSource, "StudentId",
                true, DataSourceUpdateMode.OnPropertyChanged);

            txtFullName.DataBindings.Add(
                "Text", bindingSource, "FullName",
                true, DataSourceUpdateMode.OnPropertyChanged);

            txtAge.DataBindings.Add(
                "Text", bindingSource, "Age",
                true, DataSourceUpdateMode.OnPropertyChanged);

            cboGender.DataBindings.Add(
                "SelectedItem", bindingSource, "Gender",
                true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void dgvStudents_DataBindingComplete(
            object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dgvStudents.Columns.Contains("StudentId"))
                dgvStudents.Columns["StudentId"].HeaderText = "Mã sinh viên";

            if (dgvStudents.Columns.Contains("FullName"))
                dgvStudents.Columns["FullName"].HeaderText = "Họ tên";

            if (dgvStudents.Columns.Contains("Age"))
                dgvStudents.Columns["Age"].HeaderText = "Tuổi";

            if (dgvStudents.Columns.Contains("Gender"))
                dgvStudents.Columns["Gender"].HeaderText = "Giới tính";

            if (dgvStudents.Columns.Contains("AvatarPath"))
                dgvStudents.Columns["AvatarPath"].Visible = false;
        }

        private void dgvStudents_SelectionChanged(
            object sender, EventArgs e)
        {
            HienThiAnh();
        }

        private void HienThiAnh()
        {
            try
            {
                if (picAvatar.Image != null)
                {
                    Image oldImage = picAvatar.Image;
                    picAvatar.Image = null;
                    oldImage.Dispose();
                }

                Student s = bindingSource.Current as Student;

                if (s != null &&
                    !string.IsNullOrWhiteSpace(s.AvatarPath) &&
                    File.Exists(s.AvatarPath))
                {
                    using (Image img = Image.FromFile(s.AvatarPath))
                    {
                        picAvatar.Image = new Bitmap(img);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị ảnh: " + ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Student s = new Student();

            students.Add(s);
            bindingSource.Position = students.Count - 1;

            txtStudentId.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Student s = bindingSource.Current as Student;

            if (s == null)
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần xóa.");
                return;
            }

            if (MessageBox.Show(
                "Bạn có chắc muốn xóa sinh viên này?",
                "Xác nhận",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                students.Remove(s);
                HienThiAnh();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Student s = bindingSource.Current as Student;

            if (s == null)
            {
                MessageBox.Show("Vui lòng chọn sinh viên.");
                return;
            }

            int age;

            if (string.IsNullOrWhiteSpace(txtStudentId.Text) ||
                string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Mã sinh viên và họ tên không được để trống.");
                return;
            }

            if (!int.TryParse(txtAge.Text, out age) ||
                age < 1 || age > 120)
            {
                MessageBox.Show("Tuổi không hợp lệ.");
                return;
            }

            if (cboGender.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn giới tính.");
                return;
            }

            bindingSource.EndEdit();
            dgvStudents.Refresh();

            MessageBox.Show("Cập nhật thành công.");
        }

        private void btnChooseAvatar_Click(object sender, EventArgs e)
        {
            Student s = bindingSource.Current as Student;

            if (s == null)
            {
                MessageBox.Show("Vui lòng thêm sinh viên trước.");
                return;
            }

            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Chọn ảnh đại diện";
                dialog.Filter =
                    "Ảnh (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    s.AvatarPath = dialog.FileName;
                    bindingSource.ResetCurrentItem();
                    HienThiAnh();
                }
            }
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Title = "Xuất danh sách sinh viên";
                dialog.Filter = "CSV (*.csv)|*.csv";
                dialog.FileName = "DanhSachSinhVien.csv";

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    bindingSource.EndEdit();

                    using (StreamWriter writer = new StreamWriter(
                        dialog.FileName, false,
                        new UTF8Encoding(true)))
                    {
                        writer.WriteLine(
                            "MaSV,HoTen,Tuoi,GioiTinh,AvatarPath");

                        foreach (Student s in students)
                        {
                            writer.WriteLine(
                                CsvEscape(s.StudentId) + "," +
                                CsvEscape(s.FullName) + "," +
                                s.Age + "," +
                                CsvEscape(s.Gender) + "," +
                                CsvEscape(s.AvatarPath));
                        }
                    }

                    MessageBox.Show("Xuất CSV thành công.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xuất file: " + ex.Message);
                }
            }
        }

        private string CsvEscape(string value)
        {
            if (value == null)
                value = "";

            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }
    }
}
