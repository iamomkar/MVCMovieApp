using Microsoft.EntityFrameworkCore;
using MovieAppSQL.Models.DataAcessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAppSQL.Models
{
    public class MovieDataAcessLayerEF : IMovieDataAccessLayer
    {
        MovieAppDBContext movieAppDBContext;

        public MovieDataAcessLayerEF(MovieAppDBContext context)
        {
            movieAppDBContext = context;
        }

        public bool Add(Movie movie)
        {
            if(movie.Rating > 10  || movie.Rating < 1)
            {
                throw new ArgumentOutOfRangeException(Convert.ToString(movie.Rating));

            }
            else if (Convert.ToInt32(movie.ReleaseYear) < 1900 || Convert.ToInt32(movie.ReleaseYear) > 2020)
            {
                throw new ArgumentOutOfRangeException(movie.ReleaseYear);
            }
            else if (string.IsNullOrEmpty(movie.MovieName))
            {
                throw new ArgumentNullException(movie.MovieName);
            }
            else if (string.IsNullOrEmpty(movie.Genre))
            {
                throw new ArgumentNullException(movie.Genre);
            }
            movieAppDBContext.Add(movie);
            movieAppDBContext.SaveChanges();
            return true;
        }

        public  IEnumerable<Movie> Movies()
        {
            return  movieAppDBContext.Movies.ToList(); ;
        }

        public void Remove(int? id)
        {
            var movie =  movieAppDBContext.Movies.Find(id);
            movieAppDBContext.Movies.Remove(movie);
            movieAppDBContext.SaveChanges();
        }

        public void Update(Movie movie)
        {
            movieAppDBContext.Update(movie);
            movieAppDBContext.SaveChanges();
        }

        public Movie GetMovieDetails(int? id)
        {
            Movie movie =  movieAppDBContext.Movies.Find(id);
            return movie;
        }
    }
}
