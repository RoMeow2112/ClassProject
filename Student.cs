using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office.CoverPageProps;

namespace _19110085_NguyenTranKhai_QLSV
{
    class Student
    {
        MY_DB mydb = new MY_DB();

        public bool insertStudent(int Id, string fname, string lname, DateTime bdate, string gender, string phone, string address, MemoryStream picture, string email)
        {
            SqlCommand command = new SqlCommand("INSERT INTO std (id, fname, lname, bdate, gender, phone, address, picture, email)" +
                " VALUES (@id, @fn, @ln, @bdt, @gdr, @phn, @adrs, @pic, @email)", mydb.getConnection);
            command.Parameters.Add("@id", SqlDbType.Int).Value = Id;
            command.Parameters.Add("@fn", SqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@ln", SqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@bdt", SqlDbType.DateTime).Value = bdate;
            command.Parameters.Add("@gdr", SqlDbType.VarChar).Value = gender;
            command.Parameters.Add("@phn", SqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@adrs", SqlDbType.VarChar).Value = address;
            command.Parameters.Add("@pic", SqlDbType.Image).Value = picture.ToArray();
            command.Parameters.Add("@email", SqlDbType.VarChar).Value = email;

            mydb.openConnection();

            if ((command.ExecuteNonQuery() == 1))
            {
                mydb.closeConnection();
                return true;
            }
            else
            {
                mydb.closeConnection();
                return false;
            }
        }
        public DataTable getStudents(SqlCommand command)
        {
            command.Connection = mydb.getConnection;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }


        public DataTable GetStudentsByFname(string fname)
        {
            SqlCommand command = new SqlCommand("SELECT id, fname, lname, bdate, gender, phone, address, picture FROM std WHERE fname = @fname", mydb.getConnection);
            command.Parameters.AddWithValue("@fname", fname);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        public bool updateStudent(int Id, string fname, string lname, DateTime bdate, string gender, string phone, string address, MemoryStream picture)
        {
            SqlCommand command = new SqlCommand("UPDATE std SET fname=@fn, lname=@ln, bdate=@bdt, gender=@gdr, phone=@phn, address=@adrs, " +
                "picture=@pic WHERE id =@id", mydb.getConnection);
            

            command.Parameters.Add("@id", SqlDbType.Int).Value = Id;
            command.Parameters.Add("@fn", SqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@ln", SqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@bdt", SqlDbType.DateTime).Value = bdate;
            command.Parameters.Add("@gdr", SqlDbType.VarChar).Value = gender;
            command.Parameters.Add("@phn", SqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@adrs", SqlDbType.VarChar).Value = address;
            command.Parameters.Add("@pic", SqlDbType.Image).Value = picture.ToArray();

            mydb.openConnection();

            if ((command.ExecuteNonQuery() == 1))
            {
                mydb.closeConnection();
                return true;
            }
            else
            {
                mydb.closeConnection();
                return false;
            }
        }

        public bool deleteStudent(int id)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM std WHERE id=@id", mydb.getConnection);
            cmd.Parameters.Add("@id",SqlDbType.Int).Value = id;
            mydb.openConnection();
            if ((cmd.ExecuteNonQuery() == 1)) 
            { 
                mydb.closeConnection();
                return true;
            }

            else
            {
                mydb.closeConnection();
                return false;
            }
        }

        public int TotalStudent()
        {
            int total = 0;

            try
            {
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM std", mydb.getConnection);
                mydb.openConnection();
                total = (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                mydb.closeConnection();
            }

            return total;
        }

        public int TotalMaleStudent()
        {
            int count = 0;
            try
            {
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM std WHERE gender = 'Male'", mydb.getConnection);
                mydb.openConnection();
                count = (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                mydb.closeConnection();
            }
            return count;
        }


        public int TotalFeMaleStudent()
        {
            int count = 0;
            try
            {
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM std WHERE gender = 'FeMale'", mydb.getConnection);
                mydb.openConnection();
                count = (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                mydb.closeConnection();
            }
            return count;
        }

    }
}
