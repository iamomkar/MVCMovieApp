using MovieAppSQL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAppSQL.Models
{
    public class UserDataAcessLayer
    {
        string connectionString = "Server=FSIND-LT-18\\SQLEXPRESS;Database=MovieAppDB;Trusted_Connection=True;";

        public void AddUser(User user)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@LastName", user.LastName);
                cmd.Parameters.AddWithValue("@EmailId", user.EmailID);
                cmd.Parameters.AddWithValue("@Password", user.Password);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public bool ChangePassword(string email, string pass)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spChangePasswordUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmailId", email);
                cmd.Parameters.AddWithValue("@Password", pass);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }

        }


        public bool CheckLogin(User user)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spCheckLoginUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmailId", user.EmailID);
                cmd.Parameters.AddWithValue("@Password", user.Password);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    string email = rdr["EmailId"].ToString();
                    string pass = rdr["Password"].ToString();

                    if (email == user.EmailID && pass == user.Password)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }

        }

    }
}
