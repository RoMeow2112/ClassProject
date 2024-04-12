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
using ClosedXML.Excel;


namespace _19110085_NguyenTranKhai_QLSV
{
    public partial class studentListForm : Form
    {
        public studentListForm()
        {
            InitializeComponent();
        }

        Student student = new Student();

        private void studentListForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'classProjectDataSet1.std' table. You can move, or remove it, as needed.
            this.stdTableAdapter1.Fill(this.classProjectDataSet1.std);
            // TODO: This line of code loads data into the 'classProjectDataSet.std' table. You can move, or remove it, as needed.
            this.stdTableAdapter.Fill(this.classProjectDataSet.std);
            SqlCommand cmd = new SqlCommand("SELECT * FROM std");
            dataGridView1.ReadOnly = true;
            DataGridViewImageColumn picCol = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 40;
            dataGridView1.DataSource = student.getStudents(cmd);
            picCol = (DataGridViewImageColumn)dataGridView1.Columns[7];
            picCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.AllowUserToAddRows = false;
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM std");
            dataGridView1.ReadOnly = true;
            DataGridViewImageColumn picCol = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 40;
            dataGridView1.DataSource = student.getStudents(command);
            picCol = (DataGridViewImageColumn)dataGridView1.Columns[7];
            picCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.AllowUserToAddRows = false;
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

                        // Loop through each row in the worksheet
                        foreach (IXLRow row in worksheet.RowsUsed().Skip(1)) // Skip header row
                        {
                            // Assuming columns are in order: ID, fname, lname, dob, gender, phone, address
                            string id = row.Cell(1).Value.ToString();
                            string fname = row.Cell(2).Value.ToString();
                            string lname = row.Cell(3).Value.ToString();
                            DateTime dob = DateTime.Parse(row.Cell(4).Value.ToString());
                            string gender = row.Cell(5).Value.ToString();
                            string phone = row.Cell(6).Value.ToString();
                            string address = row.Cell(7).Value.ToString();

                            // Pass data to AddStudent form
                            AddStudentForm addStudentForm = new AddStudentForm();
                            addStudentForm.txtStudentID.Text = id;
                            addStudentForm.TextBoxFname.Text = fname;
                            addStudentForm.TextBoxLname.Text = lname;
                            addStudentForm.dateTimePicker1.Value = dob;
                            if (gender == "Female")
                                addStudentForm.radioButtonFemale.Checked = true;
                            else if (gender == "Male")
                                addStudentForm.radioButtonMale.Checked = true;
                            addStudentForm.TextBoxPhone.Text = phone;
                            addStudentForm.TextBoxAddress.Text = address;
                            addStudentForm.ShowDialog();
                        }
                    }

                    MessageBox.Show("Import thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
