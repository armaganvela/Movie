using Movies.Core;
using Movies.Interface;
using Movies.Web.API.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Movies.Web.API
{
    [RoutePrefix("api/movie")]
    public class MovieApiController : ApiController
    {
        private readonly IFavoriteMovieService _iFavoriteMovieService;

        public MovieApiController(IFavoriteMovieService favoriteMovieService)
        {
            _iFavoriteMovieService = favoriteMovieService;
        }
        
        [Route("addFavoriteMovie")]
        [HttpPost]
        public IHttpActionResult AddFavoriteMovie(UserFavoriteMovieBindingModel userFavoriteMovieBindingModel)
        {
            UserFavoriteMovie userFavoriteMovie = new UserFavoriteMovie()
            {
                 UserGuidId = userFavoriteMovieBindingModel.UserGuidId,
                 MovieId = userFavoriteMovieBindingModel.MovieId
            };

            _iFavoriteMovieService.AddMovieToFavorites(userFavoriteMovie);

            return Ok();
        }

        [Route("getFavoriteMovies")]
        [HttpGet]
        public IHttpActionResult GetFavoriteMovies(string userGuidId)
        {
            List<UserFavoriteMovie> favoriteMovies = _iFavoriteMovieService.GetFavoriteMovies(userGuidId);

            return Ok(favoriteMovies);
        }
    }
}
