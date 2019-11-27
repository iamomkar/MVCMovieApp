using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAppSQL.Models
{
    public class MovieDataAcessLayer
    {
        string connectionString = "Server=FSIND-LT-18\\SQLEXPRESS;Database=MovieAppDB;Trusted_Connection=True;";

        public void Add(Movie movie)
        {
            using SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("spAddMovie", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MovieName", movie.MovieName);
            cmd.Parameters.AddWithValue("@RYear", movie.ReleaseYear);
            cmd.Parameters.AddWithValue("@Genre", movie.Genre);
            cmd.Parameters.AddWithValue("@Rating", movie.Rating);

            con.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        public IEnumerable<Movie> Movies()
        {
            List<Movie> movies = new List<Movie>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllMovies", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Movie mv = new Movie();

                    mv.MovieId = Convert.ToInt32(rdr["MovieId"]);
                    mv.MovieName = rdr["MovieName"].ToString();
                    mv.ReleaseYear = rdr["ReleaseYear"].ToString();
                    mv.Genre = rdr["Genre"].ToString();
                    mv.Rating = Convert.ToInt32(rdr["Rating"].ToString());

                    movies.Add(mv);
                }
                cmd.Dispose();
                con.Close();
            }
            return movies;
        }

        public void Remove(int? id)
        {
            using SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("spDeleteMovie", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        public void Update(Movie movie)
        {
            Debug.WriteLine(movie.MovieId + movie.MovieName + movie.ReleaseYear + movie.Genre + movie.Rating);

            using SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("spUpdateMovie", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", movie.MovieId);
            cmd.Parameters.AddWithValue("@Name", movie.MovieName);
            cmd.Parameters.AddWithValue("@Year", movie.ReleaseYear);
            cmd.Parameters.AddWithValue("@Genre", movie.Genre);
            cmd.Parameters.AddWithValue("@Rating", movie.Rating);

            con.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        public Movie GetMovieDetails(int? id)
        {
            Movie movie = new Movie();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Movies WHERE MovieId=@id", con);
                SqlParameter idParameter =  cmd.Parameters.Add("@id", SqlDbType.Int);
                idParameter.Value = id;
                con.Open();
                cmd.Prepare();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    movie.MovieId = Convert.ToInt32(rdr["MovieId"]);
                    movie.MovieName = rdr["MovieName"].ToString();
                    movie.ReleaseYear = rdr["ReleaseYear"].ToString();
                    movie.Genre = rdr["Genre"].ToString();
                    movie.Rating = Convert.ToInt32(rdr["Rating"]);
                }
                cmd.Dispose();
            }

            return movie;
        }
    }
}
