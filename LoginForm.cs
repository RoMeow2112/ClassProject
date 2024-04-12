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
    public partial class LoginForm : Form
    {

        private SqlConnection connection = new SqlConnection("Data Source=DESKTOP-6MVAG4A;Initial Catalog=ClassProject;Integrated Security=True");
        public LoginForm()
        {
            InitializeComponent();

            this.AcceptButton = btnLogin;

            this.KeyPreview = true;

            toolTip1 = new ToolTip();

            toolTip1.IsBalloon = true;

            toolTip1.ReshowDelay = 500;

            toolTip1.ToolTipIcon = ToolTipIcon.Info;

            toolTip1.ToolTipTitle = "Login";

            toolTip1.SetToolTip(txtUsername, "Enter your username here.");

            toolTip1.SetToolTip(txtPassword, "Password must be 6-10 characters");

            errorProvider1 = new ErrorProvider();
        }



        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Perform some validation here

            if (string.IsNullOrEmpty(username))
            {
                errorProvider1.SetError(txtUsername, "Please enter your username");
                return;
            }
            else
            {
                errorProvider1.SetError(txtUsername, "");
            }

            if (string.IsNullOrEmpty(password))
            {
                errorProvider1.SetError(txtPassword, "Please enter your password");
                return;
            }
            else
            {
                errorProvider1.SetError(txtPassword, "");
            }

            // Check if the username and password are correct
            bool isVerified = VerifyLogin(username, password);
            bool isAuthenticated = IsAuthenticated(username, password);

            if (!isVerified)
            {
                // Thông báo nếu tài khoản không hợp lệ
                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!isAuthenticated)
            {
                // Thông báo nếu tài khoản chưa được xác thực
                MessageBox.Show("Tài khoản chưa được xác thực!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // Login successful, show the main form
                MessageBox.Show("Login success");
                this.Hide();
                MainForm mainForm = new MainForm();
                mainForm.Show();
            }
        }

        private bool VerifyLogin(string username, string password)
        {
            // Check the SQL table for a matching username and password
            SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM log_in WHERE Username=@username AND Password=@password", connection);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);

            connection.Open();
            int count = (int)command.ExecuteScalar();
            connection.Close();

            return count > 0;
        }

        private bool IsAuthenticated(string username, string password)
        {
            // Check if the status is "Yes"
            SqlCommand command = new SqlCommand("SELECT Status FROM log_in WHERE Username=@username AND Password=@password AND Status='Yes'", connection);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);

            connection.Open();
            object status = command.ExecuteScalar();
            connection.Close();

            return status != null && status.ToString() == "Yes";
        }




        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Close the form
            this.Close();
        }

        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            // Kiểm tra nếu người dùng nhấn phím Enter
            if (e.KeyCode == Keys.Enter)
            {
                // Xử lý đăng nhập khi nhấn phím Enter
                btnLogin.PerformClick();
            }
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtUsername, "");
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtPassword, "");
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
        }
    }
}
