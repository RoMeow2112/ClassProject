using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _19110085_NguyenTranKhai_QLSV
{
    // Trong class studentListbyFnameForm
    public partial class studentListbyFnameForm : Form
    {
        public studentListbyFnameForm(DataTable studentsTable)
        {
            InitializeComponent();

            // Hiển thị danh sách sinh viên trong DataGridView
            dataGridView1.DataSource = studentsTable;
        }
    }

}
