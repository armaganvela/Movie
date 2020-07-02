using Movies.Core;
using Movies.Core.Models;
using Movies.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Movies.Web.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/movieDb")]
    public class MovieDBApiController : ApiController
    {
        private readonly IMovieService _movieService;
        private readonly ITVService _tvService;
        private readonly IFavoriteMovieService _iFavoriteMovieService;

        public MovieDBApiController(IMovieService movieService, ITVService tvService, IFavoriteMovieService favoriteMovieService)
        {
            _movieService = movieService;
            _tvService = tvService;
            _iFavoriteMovieService = favoriteMovieService;
        }

        [Route("getMovies")]
        [HttpGet]
        public IHttpActionResult GetMovies(int pageNumber = 1)
        {
            MovieResult nowPlayingMoviesResult = _movieService.GetNowPlayingMovies(pageNumber);
            MovieResult popularMoviesResult = _movieService.GetPopularMovies(pageNumber);
            MovieResult topRatedMoviesResult = _movieService.GetTopRatedMovies(pageNumber);

            CombineMovieResult combineMovieResult = new CombineMovieResult()
            {
                NowPlayingMovies = nowPlayingMoviesResult,
                PopularMovies = popularMoviesResult,
                TopRatedMovies = topRatedMoviesResult
            };

            return Ok(combineMovieResult);
        }

        [Route("getMovie")]
        [HttpGet]
        public IHttpActionResult MovieDetail(int id)
        {
            Movie movie = _movieService.GetMovie(id);

            var credits = _movieService.GetMovieCredits(id);
            movie.Credits = credits.Credits;

            return Ok(movie);
        }

        [Route("getTVs")]
        [HttpGet]
        public IHttpActionResult GetTVs(int pageNumber = 1)
        {
            TVResult popularTVsResult = _tvService.GetPopularTVs(pageNumber);
            TVResult topRatedTVsResult = _tvService.GetTopRatedTVs(pageNumber);

            CombineTVResult combineTVResult = new CombineTVResult()
            {
                PopularTVs = popularTVsResult,
                TopRatedTVs = topRatedTVsResult,
            };

            return Ok(combineTVResult);
        }

        [Route("getTV")]
        [HttpGet]
        public IHttpActionResult TVDetail(int id)
        {
            TV tv = _tvService.GetTV(id);

            var credits = _tvService.GetTVCredits(id);
            tv.Credits = credits.Credits;

            return Ok(tv);
        }

    }
}
