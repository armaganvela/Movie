using Movies.Core;
using Movies.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Interface
{
    public interface IMovieService
    {
        MovieResult GetTopRatedMovies(int pageNumber);

        MovieResult GetPopularMovies(int pageNumber);

        MovieResult GetNowPlayingMovies(int pageNumber);

        Movie GetMovie(int movieId);

        Credit GetMovieCredits(int id);
    }
}
