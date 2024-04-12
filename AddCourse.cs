using DocumentFormat.OpenXml.Office2013.PowerPoint.Roaming;
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
    public partial class AddCourse : Form
    {
        public AddCourse()
        {
            InitializeComponent();
        }

        private SqlConnection connection = new SqlConnection("Data Source=DESKTOP-6MVAG4A;Initial Catalog=ClassProject;Integrated Security=True");

        private void ButtonAddStudent_Click(object sender, EventArgs e)
        {
            Course course = new Course();
            int cid = Convert.ToInt32(txtCourseID.Text);
            string name = txtLabel.Text;
            int hrs = Convert.ToInt32(txtPeriod.Text);
            string descr = txtDescription.Text;

            if (name.Trim() == "")
            {
                MessageBox.Show("Add A Course Name", "Add Course",MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if(hrs < 10)
            {
                MessageBox.Show("Khong du thoi luong hoc", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
                else if( course.checkCourseName(name))
                {
                    if (course.insertCourse(cid, name, hrs, descr))
                    {
                        MessageBox.Show("New Course Inserted", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Course Not Inserted", "Add Course", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    }
                }
            else
            {
                MessageBox.Show("This Course Name Already Exists", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Warning );
            }
        }
    }
}
