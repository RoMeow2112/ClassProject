using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _19110085_NguyenTranKhai_QLSV
{
    public partial class ManageStudentsForm : Form
    {
        public ManageStudentsForm()
        {
            InitializeComponent();
        }

        Student student = new Student();

        private void ButtonAddStudent_Click(object sender, EventArgs e)
        {
            Student student = new Student();
            int id = Convert.ToInt32(txtStudentID.Text);
            string fname = TextBoxFname.Text;
            string lname = TextBoxLname.Text;
            DateTime bdate = dateTimePicker1.Value;
            string phone = TextBoxPhone.Text;
            string address = TextBoxAddress.Text;
            string gender = "Male";

            if (radioButtonFemale.Checked)
            {
                gender = "Female";
            }

            string email = txtStudentID.Text + "@student.hcmute.edu.vn";

            MemoryStream pic = new MemoryStream();
            int born_year = dateTimePicker1.Value.Year;
            int this_year = DateTime.Now.Year;

            if (((this_year - born_year) < 10) || ((this_year - born_year) > 100))
            {
                MessageBox.Show("The student age must be between 10 and 100 years", "Invalid Birth Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (verif())
            {
                // Kiểm tra xem studentID đã tồn tại trong cơ sở dữ liệu hay chưa
                if (IsStudentIDExists(id))
                {
                    MessageBox.Show("Student ID already exists", "Duplicate Student ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    pictureBox1.Image.Save(pic, pictureBox1.Image.RawFormat);
                    if (student.insertStudent(id, fname, lname, bdate, gender, address, phone, pic, email))
                    {
                        MessageBox.Show("New student added", "Add student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error", "Add student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Empty Fields", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private bool IsStudentIDExists(int studentID)
        {
            // Kết nối đến cơ sở dữ liệu
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-6MVAG4A;Initial Catalog=ClassProject;Integrated Security=True");

            // Truy vấn SQL để kiểm tra xem studentID đã tồn tại hay chưa
            SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM std WHERE id = @studentID", connection);
            command.Parameters.AddWithValue("@studentID", studentID);

            connection.Open();
            int count = (int)command.ExecuteScalar();
            connection.Close();

            // Nếu count > 0, tức là studentID đã tồn tại
            return count > 0;
        }

        bool verif()
        {
            if ((TextBoxFname.Text.Trim() == "")
                    || (TextBoxLname.Text.Trim() == "")
                    || (TextBoxPhone.Text.Trim() == "")
                    || (pictureBox1.Image == null))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void ButtonUploadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";
            if ((opf.ShowDialog() == DialogResult.OK))
            {
                try
                {
                    pictureBox1.Image = Image.FromFile(opf.FileName);
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            Student student = new Student();
            int id = Convert.ToInt32(txtStudentID.Text);
            string fname = TextBoxFname.Text;
            string lname = TextBoxLname.Text;
            DateTime bdate = dateTimePicker1.Value;
            string phone = TextBoxPhone.Text;
            string address = TextBoxAddress.Text;
            string gender = "Male";

            if (radioButtonFemale.Checked)
            {
                gender = "Female";
            }

            MemoryStream pic = new MemoryStream();
            int born_year = dateTimePicker1.Value.Year;
            int this_year = DateTime.Now.Year;

            if (((this_year - born_year) < 10) || ((this_year - born_year) > 100))
            {
                MessageBox.Show("The student age must be between 10 and 100 years", "Invalid Birth Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (verif())
            {
                try
                {
                    pictureBox1.Image.Save(pic, pictureBox1.Image.RawFormat);
                    if (student.updateStudent(id, fname, lname, bdate, gender, address, phone, pic))
                    {
                        MessageBox.Show("Student information updated", "Edit student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error", "Edit Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Edit Student", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Empty Fields", "Edit Student", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            try
            {
                int studentID = Convert.ToInt32(txtStudentID.Text);

                if (MessageBox.Show("Are you sure you want to delete this student", "Delete student", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (student.deleteStudent(studentID))
                    {
                        MessageBox.Show("Student Deleted", "Delete student", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        txtStudentID.Text = "";
                        TextBoxFname.Text = "";
                        TextBoxLname.Text = "";
                        TextBoxAddress.Text = "";
                        TextBoxPhone.Text = "";
                        dateTimePicker1.Value = DateTime.Now;
                        pictureBox1.Image = null;
                    }

                    else
                    {
                        MessageBox.Show("Student not delete", "Delete student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error", "Delete student", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            txtStudentID.Text = "";
            TextBoxFname.Text = "";
            TextBoxLname.Text = "";
            TextBoxAddress.Text = "";
            TextBoxPhone.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            pictureBox1.Image = null;
        }

        private void ButtonDownloadImage_Click(object sender, EventArgs e)
        {
            SaveFileDialog svf = new SaveFileDialog();
            svf.FileName = ("student_" + txtStudentID.Text);
            if ((pictureBox1.Image == null))
            {
                MessageBox.Show("No image in the picture box");
            }
            else if ((svf.ShowDialog() == DialogResult.OK))
            {
                pictureBox1.Image.Save((svf.FileName + ("." + ImageFormat.Jpeg.ToString())));
            }
        }

        private void buttonFind_Click(object sender, EventArgs e)
        {

            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-6MVAG4A;Initial Catalog=ClassProject;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                // Mở kết nối
                connection.Open();

                // Xây dựng câu truy vấn SQL với các điều kiện lọc được xây dựng
                string query = "SELECT id, fname, lname, bdate, gender, phone, address, picture FROM std WHERE CONCAT(fname,lname,address) LIKE '%" + textBoxFind.Text + "%'";

                // Thực thi câu truy vấn và đổ dữ liệu vào DataTable
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable table = new DataTable();

                        adapter.Fill(table);

                        dataGridView1.DataSource = table; // Đặt DataSource sau khi điền dữ liệu vào bảng

                        dataGridView1.ReadOnly = true;

                        DataGridViewImageColumn picCol = new DataGridViewImageColumn();

                        dataGridView1.RowTemplate.Height = 40;

                        picCol = (DataGridViewImageColumn)dataGridView1.Columns[7];

                        picCol.ImageLayout = DataGridViewImageCellLayout.Stretch;

                        dataGridView1.AllowUserToAddRows = false;

                        labelTotal.Text = ("Total Students: " + dataGridView1.Rows.Count);
                    }
                }

                // Đóng kết nối
                connection.Close();
            }
        }

        private void ManageStudentsForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'classProjectDataSet2.std' table. You can move, or remove it, as needed.
            this.stdTableAdapter.Fill(this.classProjectDataSet.std);
            SqlCommand cmd = new SqlCommand("SELECT * FROM std");
            dataGridView1.ReadOnly = true;
            DataGridViewImageColumn picCol = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 40;
            dataGridView1.DataSource = student.getStudents(cmd);
            picCol = (DataGridViewImageColumn)dataGridView1.Columns[7];
            picCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.AllowUserToAddRows = false;

            labelTotal.Text = ("Total students: ") + dataGridView1.Rows.Count;
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            UpdateDeleteStudentForm updateDeleteStdF = new UpdateDeleteStudentForm();
            updateDeleteStdF.textBoxID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            updateDeleteStdF.textBoxFName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            updateDeleteStdF.textBoxLName.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            updateDeleteStdF.dateTimePicker1.Value = (DateTime)dataGridView1.CurrentRow.Cells[3].Value;

            if ((dataGridView1.CurrentRow.Cells[4].Value.ToString() == "Female"))
            {
                updateDeleteStdF.radioButtonFemale.Checked = true;
            }
            else
            {
                updateDeleteStdF.radioButtonMale.Checked = true;
            }

            updateDeleteStdF.textBoxPhone.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            updateDeleteStdF.textBoxAddress.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();

            byte[] pic;
            pic = (byte[])dataGridView1.CurrentRow.Cells[7].Value;
            MemoryStream picture = new MemoryStream(pic);
            updateDeleteStdF.pictureBoxStudentImage.Image = Image.FromStream(picture);

            updateDeleteStdF.Show();
        }
    }
}
