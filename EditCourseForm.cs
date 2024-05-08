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
    public partial class EditCourseForm : Form
    {
        public EditCourseForm()
        {
            InitializeComponent();
        }

        Course Course = new Course();

        private void EditCourseForm_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = Course.GetAllCourses();
            comboBox1.DisplayMember = "label";
            comboBox1.ValueMember = "Id";
            comboBox1.SelectedItem = null;
        }

        public void fillCombo(int index )   
        {
            comboBox1.DataSource = Course.GetAllCourses();
            comboBox1.DisplayMember = "label";
            comboBox1.ValueMember = "Id";
            comboBox1.SelectedIndex = index;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(comboBox1.SelectedValue);
                DataTable table = new DataTable();
                table = Course.GetCoursebyId(id);
                txtBoxCourseName.Text = table.Rows[0][1].ToString();
                numericUpDown1.Value = Int32.Parse(table.Rows[0][2].ToString());
                txtBoxDescription.Text = table.Rows[0][3].ToString();
            }

            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = txtBoxCourseName.Text;
            int hrs = (int) numericUpDown1.Value;
            string descr = txtBoxDescription.Text;
            int id = (int) comboBox1.SelectedValue;

            if (!Course.checkCourseName(name, id))
            {
                MessageBox.Show("This Course Name Already Exist", "Edit Course", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (Course.updateCourse(id, name, hrs, descr)) 
            {
                MessageBox.Show("Course Update", "Edit Course", MessageBoxButtons.OK, MessageBoxIcon.Information);
                fillCombo(comboBox1.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Course Not Update", "Edit Course", MessageBoxButtons.OK, MessageBoxIcon.Information );
            }
        }
    }
}
