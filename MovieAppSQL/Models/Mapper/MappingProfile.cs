using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAppSQL.Models.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {            
            //Registration Model Mapping
            CreateMap<User, RegisterRequestModel>();
            CreateMap<RegisterRequestModel, User>();

            //UserDetails Model Mapping
            CreateMap<User, GetUserDetailsResponseModelResult>();
            CreateMap<GetUserDetailsResponseModelResult, User>();

            //AddMovie Model Mapping
            CreateMap<Movie, AddMovieRequestModel>();
            CreateMap<AddMovieRequestModel, Movie>();

            //Update Movie Model Mapping
            CreateMap<Movie, EditMovieRequestModel>();
            CreateMap<EditMovieRequestModel, Movie>();

            //Get Movie Model to Edit Mapping
            CreateMap<EditMovieRequestModel, GetMovieDetailsResponseModelResult>();
            CreateMap<GetMovieDetailsResponseModelResult, EditMovieRequestModel>();

            //Get Movie Model Mapping
            CreateMap<Movie, GetMovieDetailsResponseModelResult>();
            CreateMap<GetMovieDetailsResponseModelResult, Movie>();

        }
    }
}
