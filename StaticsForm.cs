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
    public partial class StaticsForm : Form
    {
        public StaticsForm()
        {
            InitializeComponent();
        }

        Color panTotalColor;
        Color panMaleColor;
        Color panFemaleColor;

        private void StaticsForm_Load(object sender, EventArgs e)
        {
            panTotalColor = panTotal.BackColor;
            panMaleColor = panMale.BackColor;
            panFemaleColor = panFemale.BackColor;

            Student student = new Student();
            double total = Convert.ToDouble(student.TotalStudent());
            double totalMale = Convert.ToDouble(student.TotalMaleStudent());
            double totalFemale = Convert.ToDouble(student.TotalFeMaleStudent());

            double MaleStudentPer = (totalMale * (100 / total));
            double FemaleStudentPer = totalFemale * (100 / total);

            LabelTotal.Text = ("Total Students: " + total.ToString());
            LabelMale.Text = ("Male: " + (MaleStudentPer.ToString("0.00")) + "%");
            LabelFemale.Text = ("Female: " + (FemaleStudentPer.ToString("0.00")) + "%");
        }

        private void LabelTotal_MouseEnter(object sender, EventArgs e)
        {
            LabelTotal.ForeColor = panTotalColor;
            panTotal.BackColor = Color.White;
        }

        private void LabelTotal_MouseLeave(object sender, EventArgs e)
        {
            LabelTotal.ForeColor = Color.White;
            panTotal.BackColor = panTotalColor;
        }

        private void LabelMale_MouseEnter(object sender, EventArgs e)
        {
            LabelMale.ForeColor = panMaleColor;
            panMale.BackColor = Color.White;
        }

        private void LabelMale_MouseLeave(object sender, EventArgs e)
        {
            LabelMale.ForeColor= Color.White;
            panMale.BackColor = panMaleColor;
        }

        private void LabelFemale_MouseEnter(object sender, EventArgs e)
        {
            LabelFemale.ForeColor = panFemaleColor;
            panFemale.BackColor = Color.White;
        }

        private void LabelFemale_MouseLeave(object sender, EventArgs e)
        {
            LabelFemale.ForeColor= Color.White;
            panFemale.BackColor= panFemaleColor;
        }
    }
}
