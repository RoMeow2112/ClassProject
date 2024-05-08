using DocumentFormat.OpenXml.Office.CustomUI;
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
    public partial class ManageCoursesForm : Form
    {
        public ManageCoursesForm()
        {
            InitializeComponent();
        }

        Course course= new Course();
        int pos;

        private void ManageCoursesForm_Load(object sender, EventArgs e)
        {
            reloadListBoxData();
        }

        void reloadListBoxData()    
        {
            listBoxCourse.DataSource = course.GetAllCourses();
            listBoxCourse.ValueMember = "Id";
            listBoxCourse.DisplayMember = "label";

            listBoxCourse.SelectedItem = null;

            labelTotal.Text = ("Total Course:" + course.TotalCourse());
        }

        void ShowData(int index)
        {
            DataRow dr = course.GetAllCourses().Rows[index];

            listBoxCourse.SelectedIndex = index;

            txtBoxId.Text = dr.ItemArray[0].ToString();
            
            txtBoxCourseName.Text = dr.ItemArray[1].ToString();

            numericUpDown1.Value = int.Parse(dr.ItemArray[2].ToString());

            txtBoxDescription.Text = dr.ItemArray[3].ToString();
        }

        private void listBoxCourse_Click(object sender, EventArgs e)
        {
            DataRowView drv = (DataRowView)listBoxCourse.SelectedItem;
            pos = listBoxCourse.SelectedIndex;
            ShowData(pos);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int cid = Convert.ToInt32(txtBoxId.Text);
            string name = txtBoxCourseName.Text;
            int hrs = (int)numericUpDown1.Value;
            string descr = txtBoxDescription.Text;

            if (name.Trim() == "")
            {
                MessageBox.Show("Add A Course Name", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (hrs < 10)
            {
                MessageBox.Show("Khong du thoi luong hoc", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (course.checkCourseName(name))
            {
                if (course.insertCourse(cid, name, hrs, descr))
                {
                    MessageBox.Show("New Course Inserted", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    reloadListBoxData();
                }
                else
                {
                    MessageBox.Show("Course Not Inserted", "Add Course", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("This Course Name Already Exists", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string name = txtBoxCourseName.Text;
            int hrs = (int)numericUpDown1.Value;
            string descr = txtBoxDescription.Text;
            int id = int.Parse(txtBoxId.Text);

            if (!course.checkCourseName(name, Convert.ToInt32(txtBoxId.Text)))
            {
                MessageBox.Show("This Course Name Already Exist", "Edit Course", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (course.updateCourse(id, name, hrs, descr))
            {
                MessageBox.Show("Course Update", "Edit Course", MessageBoxButtons.OK, MessageBoxIcon.Information);
                reloadListBoxData();
            }
            else
            {
                MessageBox.Show("Course Not Update", "Edit Course", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            pos = 0;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                int courseId = Convert.ToInt32(txtBoxId.Text);

                if ((MessageBox.Show("Are you sure want to delete this course", "Remove Course", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                {
                    if (course.deleteCourse(courseId))
                    {
                        MessageBox.Show("Course Delete", "Remove Course", MessageBoxButtons.OK, MessageBoxIcon.Information );

                        txtBoxId.Text = "";
                        txtBoxCourseName.Text = "";
                        numericUpDown1.Value = 10;
                        txtBoxDescription.Text = "";

                        reloadListBoxData();
                    }
                    else
                    {
                        MessageBox.Show("Course not delete", "Remove Course", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch 
            {
                MessageBox.Show("Enter a valid numeric ID", "Remove Course", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            pos = 0;
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            pos = 0;
            ShowData(pos);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if ( pos < (course.GetAllCourses().Rows.Count - 1))
            {
                pos = pos + 1;
                ShowData(pos);
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if ( pos > 0)
            {
                pos = pos -1;
                ShowData(pos);
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            pos = course.GetAllCourses().Rows.Count - 1;
            ShowData(pos);
        }
    }
}
