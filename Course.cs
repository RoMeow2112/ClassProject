using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection.Emit;

namespace _19110085_NguyenTranKhai_QLSV
{
    class Course
    {
        MY_DB mydb = new MY_DB();

        public bool checkCourseName(string courseName, int courseId = 0)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM course WHERE label=@cName And id <> @cID", mydb.getConnection);

            command.Parameters.Add("@cName",SqlDbType.VarChar).Value = courseName;
            command.Parameters.Add("@cID",SqlDbType.Int).Value = courseId;

            SqlDataAdapter adapter = new SqlDataAdapter(command);

            DataTable dt = new DataTable();

            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool insertCourse(int Id, string label, int period, string description)
        {
            SqlCommand command = new SqlCommand("INSERT INTO course (id, label, period, description) VALUES (@id, @lb, @period, @des)", mydb.getConnection);
            command.Parameters.Add("@id", SqlDbType.Int).Value = Id;
            command.Parameters.Add("@lb", SqlDbType.VarChar).Value = label;
            command.Parameters.Add("@period", SqlDbType.Int).Value = period;
            command.Parameters.Add("@des", SqlDbType.Text).Value = description;

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

        public bool deleteCourse(int id)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM course WHERE id=@id", mydb.getConnection);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
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

        public DataTable GetAllCourses()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM course", mydb.getConnection);
            mydb.openConnection();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        public DataTable GetCoursebyId(int Id)
        {
            SqlCommand command = new SqlCommand("SELECT Id, label, period, description FROM course WHERE id = @Id", mydb.getConnection);
            command.Parameters.AddWithValue("@id", Id);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        public bool updateCourse(int Id, string label, int period, string description)
        {
            SqlCommand command = new SqlCommand("UPDATE course SET label=@label, period=@period, description=@description WHERE id =@id", mydb.getConnection);


            command.Parameters.Add("@id", SqlDbType.Int).Value = Id;
            command.Parameters.Add("@label", SqlDbType.VarChar).Value = label;
            command.Parameters.Add("@period", SqlDbType.Int).Value = period;
            command.Parameters.Add("@description", SqlDbType.VarChar).Value = description;

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


        public int TotalCourse()
        {
            int total = 0;

            try
            {
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM course", mydb.getConnection);
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
    }
}
