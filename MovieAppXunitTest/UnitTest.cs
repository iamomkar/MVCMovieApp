using System;
using Xunit;
using MovieAppSQL.Controllers;
using MovieAppSQL.Models.DataAcessLayers;
using System.Threading.Tasks;
using Moq;
using MovieAppSQL.Models;
using MediatR;
using AutoMapper;

namespace MovieAppXunitTest
{
    public class UnitTest
    { 
        private Movie movie = new Movie { MovieName = "Nikhil", Genre = "Comedy", ReleaseYear = "2000", Rating = 10 };
        
        [Fact]
        public void Test_Invalid_Rating()
        {
            var movieDataAcess = new MovieDataAcessLayerEF(new MovieAppDBContext());

            Assert.Throws<ArgumentOutOfRangeException>(() => movieDataAcess.Add(movie));

        }

        [Fact]
        public void Test_Invalid_ReleaseDate()
        {
            var movieDataAcess = new MovieDataAcessLayerEF(new MovieAppDBContext());

            Assert.Throws<ArgumentOutOfRangeException>(() => movieDataAcess.Add(movie));

        }

        [Fact]
        public void Test_Invalid_MovieName()
        {
            var movieDataAcess = new MovieDataAcessLayerEF(new MovieAppDBContext());

            Assert.Throws<ArgumentNullException>(() => movieDataAcess.Add(movie));

        }

        [Fact]
        public void Test_Invalid_Genre()
        {
            var movieDataAcess = new MovieDataAcessLayerEF(new MovieAppDBContext());

            Assert.Throws<ArgumentNullException>(() => movieDataAcess.Add(movie));

        }

        [Fact]
        public void Test_Valid_Movie()
        {
            var movieDataAcess = new MovieDataAcessLayerEF(new MovieAppDBContext());

            Assert.True(movieDataAcess.Add(movie));

        }
    }
}
