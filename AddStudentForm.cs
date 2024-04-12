using ClosedXML.Excel;
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
    public partial class AddStudentForm : Form
    {
        public AddStudentForm()
        {
            InitializeComponent();
        }
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

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

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
            openFileDialog.Title = "Select an Excel File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (XLWorkbook workbook = new XLWorkbook(openFileDialog.FileName))
                    {
                        IXLWorksheet worksheet = workbook.Worksheet(1); // Assuming data is in the first worksheet

                        // Loop through each row (skip header row)
                        foreach (IXLRow row in worksheet.RowsUsed().Skip(1))
                        {
                            // Create a new instance of AddStudentForm
                            AddStudentForm addStudentForm = new AddStudentForm();

                            // Fill data from the current row into the new instance of AddStudentForm
                            addStudentForm.txtStudentID.Text = row.Cell(1).Value.ToString();
                            addStudentForm.TextBoxFname.Text = row.Cell(2).Value.ToString();
                            addStudentForm.TextBoxLname.Text = row.Cell(3).Value.ToString();
                            addStudentForm.dateTimePicker1.Value = row.Cell(4).GetDateTime();
                            if (row.Cell(5).Value.ToString() == "Male")
                                addStudentForm.radioButtonMale.Checked = true;
                            else
                                addStudentForm.radioButtonFemale.Checked = true;
                            addStudentForm.TextBoxPhone.Text = row.Cell(6).Value.ToString();
                            addStudentForm.TextBoxAddress.Text = row.Cell(7).Value.ToString();

                            // Show the new instance of AddStudentForm
                            this.Hide();
                            addStudentForm.ShowDialog();
                            this.Show();
                        }

                        MessageBox.Show("Import thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
