using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _19110085_NguyenTranKhai_QLSV
{
    public partial class PrintForm : Form
    {
        public PrintForm()
        {
            InitializeComponent();
        }

        Student student = new Student();

        private void studentListForm_Load(object sender, EventArgs e)
        {
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

        private void rbYes_CheckedChanged(object sender, EventArgs e)
        {
            // Kiểm tra xem người dùng có chọn "Yes" hay không
            bool isDateRangeEnabled = radioButtonYes.Checked;

            // Bật hoặc tắt tính năng lọc theo khoảng thời gian dựa trên lựa chọn của người dùng
            dateTimePicker1.Enabled = dateTimePicker2.Enabled = isDateRangeEnabled;
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            // Tạo một biến để lưu điều kiện lọc dựa trên giới tính
            string genderCondition = "";

            // Kiểm tra xem người dùng đã chọn giới tính nào
            if (radioButtonAll.Checked)
            {

            }
            else if (radioButtonMale.Checked)
            {
                genderCondition = " AND gender = 'Male'";
            }
            else
            {
                genderCondition = " AND gender = 'Female'";
            }

            // Tạo một biến để lưu điều kiện lọc dựa trên khoảng thời gian nếu tính năng được bật
            string dateRangeCondition = "";

            // Kiểm tra xem tính năng lọc theo khoảng thời gian có được bật hay không
            if (radioButtonYes.Checked)
            {
                // Lấy giá trị của DateTimePicker để xây dựng điều kiện lọc
                DateTime startDate = dateTimePicker1.Value;
                DateTime endDate = dateTimePicker2.Value;

                // Xây dựng điều kiện lọc dựa trên khoảng thời gian
                dateRangeCondition = $" AND bdate BETWEEN '{startDate:yyyy-MM-dd}' AND '{endDate:yyyy-MM-dd}'";
            }

            // Xây dựng câu truy vấn SQL với các điều kiện lọc được xây dựng
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-6MVAG4A;Initial Catalog=ClassProject;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                // Mở kết nối
                connection.Open();

                // Xây dựng câu truy vấn SQL với các điều kiện lọc được xây dựng
                string query = $"SELECT id, fname, lname, bdate, gender, phone, address, picture FROM std WHERE 1=1 {genderCondition}{dateRangeCondition}";

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
                    }
                }

                // Đóng kết nối
                connection.Close();
            }

        }

        private void btnSaveToFile_Click(object sender, EventArgs e)
        {
            try
            {
                // Mở hộp thoại để chọn vị trí lưu file
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                saveFileDialog.Title = "Save as Text File";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Tạo luồng ghi dữ liệu vào file với đường dẫn đã chọn
                    using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                    {
                        // Lặp qua từng dòng trong DataGridView và ghi vào file văn bản
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            StringBuilder line = new StringBuilder();
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                line.Append(cell.Value.ToString() + "\t");
                            }
                            writer.WriteLine(line);
                        }
                    }

                    MessageBox.Show("Data saved to text file successfully!", "Save to Text File", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnToPrinter_Click(object sender, EventArgs e)
        {
            try
            {
                // Khởi tạo một PrinterDialog
                using (PrintDialog printDialog = new PrintDialog())
                {
                    // Hiển thị hộp thoại in và kiểm tra xem người dùng đã chọn máy in chưa
                    if (printDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Sử dụng thông tin từ PrinterDialog để thực hiện việc in
                        PrintDocument pd = new PrintDocument();
                        pd.PrintPage += new PrintPageEventHandler(PrintPage);
                        pd.PrinterSettings = printDialog.PrinterSettings;
                        pd.Print();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            // Lấy kích thước của DataGridView
            Size size = dataGridView1.ClientSize;

            // Đảm bảo rằng DataGridView không trống
            if (dataGridView1.Rows.Count == 0)
                return;

            // Tạo một Bitmap để chứa nội dung của DataGridView
            using (Bitmap bmp = new Bitmap(size.Width, size.Height))
            {
                // Vẽ nội dung của DataGridView lên Bitmap
                dataGridView1.DrawToBitmap(bmp, new Rectangle(0, 0, size.Width, size.Height));

                // Vẽ Bitmap lên trang in
                e.Graphics.DrawImage(bmp, e.PageBounds);
            }
        }

    }
}
