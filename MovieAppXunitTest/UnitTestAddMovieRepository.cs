using System;
using Xunit;
using MovieAppSQL.Models;
using Moq;
using MovieAppSQL.Models.DataAcessLayers;

namespace MovieAppXunitTest
{
    public class UnitTestAddMovieRepository
    {
        [Fact]
        public void Test_Invalid_Rating()
        {
            var movieDataAcess = new MovieDataAcessLayerEF(new MovieAppDBContext());
            Movie movie = new Movie { MovieName = "Nikhil", Genre = "Comedy", ReleaseYear = "2000", Rating = 11 };
            Assert.Throws<ArgumentOutOfRangeException>(() => movieDataAcess.Add(movie));

        }

        [Fact]
        public void Test_Invalid_ReleaseDate()
        {
            var movieDataAcess = new MovieDataAcessLayerEF(new MovieAppDBContext());
            Movie movie = new Movie { MovieName = "Nikhil", Genre = "Comedy", ReleaseYear = "2022", Rating = 10 };
            Assert.Throws<ArgumentOutOfRangeException>(() => movieDataAcess.Add(movie));

        }

        [Fact]
        public void Test_Invalid_MovieName()
        {
            var movieDataAcess = new MovieDataAcessLayerEF(new MovieAppDBContext());
            Movie movie = new Movie { Genre = "Comedy", ReleaseYear = "2000", Rating = 10 };
            Assert.Throws<ArgumentNullException>(() => movieDataAcess.Add(movie));

        }

        [Fact]
        public void Test_Invalid_Genre()
        {
            var movieDataAcess = new MovieDataAcessLayerEF(new MovieAppDBContext());
            Movie movie = new Movie { MovieName = "Nikhil", ReleaseYear = "2000", Rating = 10 };
            Assert.Throws<ArgumentNullException>(() => movieDataAcess.Add(movie));

        }

        [Fact]
        public void Test_Valid_Movie()
        {
            var mockMovie = new Mock<Movie>();

            var movieDataAcess = new Mock<IMovieDataAccessLayer>();

            movieDataAcess.Setup(dal => dal.Add(It.IsAny<Movie>()))
                .Returns(true);
            Movie movie = new Movie { MovieName = "Nikhil", Genre = "Comedy", ReleaseYear = "2000", Rating = 10 };
            Assert.True(movieDataAcess.Object.Add(movie));
        }
    }
}
