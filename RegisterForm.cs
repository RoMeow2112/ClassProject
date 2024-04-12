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
    public partial class RegisterForm : Form
    {
        private SqlConnection connection = new SqlConnection("Data Source=DESKTOP-6MVAG4A;Initial Catalog=ClassProject;Integrated Security=True");
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string name = txtName.Text;
            string email = txtEmail.Text;
            string status = "No";

            // Perform some validation here
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            if (CheckExistingUser(username))
            {
                MessageBox.Show("Username already exists. Please choose another one.");
                return;
            }

            // Insert the new user into the database
            if (InsertNewUser(name, email, username, password, status))
            {
                MessageBox.Show("Registration successful.");
            }
            else
            {
                MessageBox.Show("Registration failed. Please try again.");
            }

            this.Hide();
            LoginForm login = new LoginForm();
            login.Show();
        }

        private bool CheckExistingUser(string username)
        {
            // Check if the username or email already exists in the database
            SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM log_in WHERE Username=@username", connection);
            command.Parameters.AddWithValue("@username", username);

            connection.Open();
            int count = (int)command.ExecuteScalar();
            connection.Close();

            return count > 0;
        }
        private bool InsertNewUser(string name, string email, string username, string password, string status)
        {
            // Insert the new user into the database
            SqlCommand command = new SqlCommand("INSERT INTO log_in (name, email, username, password, status) VALUES (@name, @email, @username, @password, @status)", connection);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("email", email);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);
            command.Parameters.AddWithValue("status", status);

            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            connection.Close();

            return rowsAffected > 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
