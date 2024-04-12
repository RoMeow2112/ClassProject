using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _19110085_NguyenTranKhai_QLSV
{
    public partial class UpdateDeleteStudentForm : Form
    {
        public UpdateDeleteStudentForm()
        {
            InitializeComponent();
        }

        Student student = new Student();

        private void UpdateDeleteStudentForm_Load(object sender, EventArgs e)
        {
            // Thêm các lựa chọn vào ComboBox
            comboBox1.Items.Add("ID");
            comboBox1.Items.Add("Phone");
            comboBox1.Items.Add("Fname");
            comboBox1.Items.Add("Ho va ten");
        }

        private void buttonFind_Click(object sender, EventArgs e)
        {
            string selectedOption = comboBox1.SelectedItem.ToString();
            string searchText = textBoxFind.Text.Trim();

            if (selectedOption == "Fname")
            {
                // Xử lý tìm kiếm theo Fname và chuyển dữ liệu sang form StudentListByFname
                DataTable table = student.GetStudentsByFname(searchText);
                if (table.Rows.Count > 0)
                {
                    studentListbyFnameForm studentListForm = new studentListbyFnameForm(table);
                    studentListForm.Show();
                }
                else
                {
                    MessageBox.Show("No students found with the given first name.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                // Xử lý tìm kiếm theo ID hoặc Phone và hiển thị thông tin trong form hiện tại
                string query = "";
                switch (selectedOption)
                {
                    case "ID":
                        query = "SELECT id, fname, lname, bdate, gender, phone, address, picture FROM std WHERE id = @searchText";
                        break;
                    case "Phone":
                        query = "SELECT id, fname, lname, bdate, gender, phone, address, picture FROM std WHERE phone = @searchText";
                        break;
                    case "Ho va ten":
                        query = "SELECT id, fname, lname, bdate, gender, phone, address, picture FROM std WHERE CONCAT(fname, ' ', lname) LIKE @searchText";
                        searchText = "%" + searchText + "%";
                        break;
                    default:
                        MessageBox.Show("Invalid search option", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }

                SqlCommand command = new SqlCommand(query);
                command.Parameters.AddWithValue("@searchText", searchText);

                DataTable table = student.getStudents(command);

                if (table.Rows.Count > 0)
                {
                    // Hiển thị thông tin sinh viên tìm thấy trong form hiện tại
                    textBoxID.Text = table.Rows[0]["ID"].ToString();
                    textBoxFName.Text = table.Rows[0]["fname"].ToString();
                    textBoxLName.Text = table.Rows[0]["lname"].ToString();
                    dateTimePicker1.Value = (DateTime)table.Rows[0]["bdate"];

                    if (table.Rows[0]["gender"].ToString() == "Female")
                    {
                        radioButtonFemale.Checked = true;
                    }
                    else
                    {
                        radioButtonMale.Checked = true;
                    }

                    textBoxPhone.Text = table.Rows[0]["phone"].ToString();
                    textBoxAddress.Text = table.Rows[0]["address"].ToString();

                    byte[] pic = (byte[])table.Rows[0]["picture"];
                    MemoryStream picture = new MemoryStream(pic);
                    pictureBoxStudentImage.Image = Image.FromStream(picture);
                }
                else
                {
                    MessageBox.Show("Student not found", "Find Student", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void buttonUploadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";
            if ((opf.ShowDialog() == DialogResult.OK))
            {
                pictureBoxStudentImage.Image = Image.FromFile(opf.FileName);
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            Student student = new Student();
            int id = Convert.ToInt32(textBoxID.Text);
            string fname = textBoxFName.Text;
            string lname = textBoxLName.Text;
            DateTime bdate = dateTimePicker1.Value;
            string phone = textBoxPhone.Text;
            string address = textBoxAddress.Text;
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
                    pictureBoxStudentImage.Image.Save(pic, pictureBoxStudentImage.Image.RawFormat);
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
        bool verif()
        {
            if ((textBoxFName.Text.Trim() == "")
                    || (textBoxLName.Text.Trim() == "")
                    || (textBoxPhone.Text.Trim() == "")
                    || (pictureBoxStudentImage.Image == null))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            try
            {
                int studentID = Convert.ToInt32(textBoxID.Text);

                if (MessageBox.Show("Are you sure you want to delete this student", "Delete student", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (student.deleteStudent(studentID))
                    {
                        MessageBox.Show("Student Deleted", "Delete student", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        textBoxID.Text = "";
                        textBoxFName.Text = "";
                        textBoxLName.Text = "";
                        textBoxAddress.Text = "";
                        textBoxPhone.Text = "";
                        dateTimePicker1.Value = DateTime.Now;
                        pictureBoxStudentImage.Image = null;
                    }

                    else
                    {
                        MessageBox.Show("Student not delete", "Delete student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch 
            {
                MessageBox.Show("Error", "Delete student", MessageBoxButtons.OK, MessageBoxIcon.Information );
            }
        }
    }
}
