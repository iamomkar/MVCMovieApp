using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAppSQL.Models.DataAcessLayers
{
    public interface IMovieDataAccessLayer
    {
        void Add(Movie movie);

        IEnumerable<Movie> Movies();

        void Remove(int? id);

        void Update(Movie movie);

        Movie GetMovieDetails(int? id);

    }
}
