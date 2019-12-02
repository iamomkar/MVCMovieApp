using MovieAppSQL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAppSQL.Models
{
    public class UserDataAcessLayer : IUserDataAcessLayer
    {
        string connectionString = "Server=FSIND-LT-18\\SQLEXPRESS;Database=MovieAppDB;Trusted_Connection=True;";

        public bool AddUser(User user)
        {
            if (GetUserDetails(user.EmailID).EmailID != user.EmailID)
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
                    return true;
                }
            }
            else
            {
                return false;
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


        public bool CheckLogin(string email, string pass)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spCheckLoginUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmailId", email);
                cmd.Parameters.AddWithValue("@Password", pass);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (email == rdr["EmailId"].ToString() && pass == rdr["Password"].ToString())
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

        public User GetUserDetails(string emailId)
        {
            User user = new User();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM MovieUser WHERE EmailId =@id", con);
                SqlParameter idParameter = cmd.Parameters.Add("@id", SqlDbType.VarChar, 30);
                idParameter.Value = emailId;
                con.Open();
                cmd.Prepare();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    user.UserId = Convert.ToInt32(rdr["UserId"]);
                    user.EmailID = rdr["EmailId"].ToString();
                    user.FirstName = rdr["FirstName"].ToString();
                    user.LastName = rdr["LastName"].ToString();
                    user.Password = rdr["Password"].ToString();
                }
                cmd.Dispose();
            }

            return user;
        }

    }
}
