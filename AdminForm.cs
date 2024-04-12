using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _19110085_NguyenTranKhai_QLSV
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }

        private SqlConnection connection = new SqlConnection("Data Source=DESKTOP-6MVAG4A;Initial Catalog=ClassProject;Integrated Security=True");
        private void AdminForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'classProjectDataSet2.log_in' table. You can move, or remove it, as needed.
            this.log_inTableAdapter.Fill(this.classProjectDataSet2.log_in);
        }

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];

                // Kiểm tra xem cột được thay đổi có phải là cột Status không
                if (cell.OwningColumn.Name == "status")
                {
                    string newValue = cell.Value.ToString(); // Lấy giá trị mới từ combo box

                    // Lấy giá trị của cột ID trong hàng tương ứng
                    int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value);
                    // Cập nhật giá trị Status vào cơ sở dữ liệu
                    UpdateStatus(id, newValue);
                }
            }
        }

        private void UpdateStatus(int id, string newValue)
        {
            // Thực hiện cập nhật giá trị Status vào cơ sở dữ liệu
            SqlCommand command = new SqlCommand("UPDATE log_in SET status = @status WHERE id = @id", connection);
            command.Parameters.Add("@status", SqlDbType.VarChar).Value = newValue;
            command.Parameters.Add("@id", SqlDbType.Int).Value = id;

            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            connection.Close();

            if (rowsAffected > 0)
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Không có hàng nào được cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
