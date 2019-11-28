using Microsoft.EntityFrameworkCore;
using MovieAppSQL.Models.DataAcessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAppSQL.Models
{
    public class MovieDataAcessLayerEF : IDataAccessLayer
    {
        MovieAppDBContext movieAppDBContext;

        public MovieDataAcessLayerEF(MovieAppDBContext context)
        {
            movieAppDBContext = context;
        }

        public void Add(Movie movie)
        {
            movieAppDBContext.Add(movie);
            movieAppDBContext.SaveChanges();
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
