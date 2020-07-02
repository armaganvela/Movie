using Movies.Core;
using Movies.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Interface
{
    public interface IFavoriteMovieService
    {
        void AddMovieToFavorites(UserFavoriteMovie userFavoriteMovie);

        List<UserFavoriteMovie> GetFavoriteMovies(string userGuidId);
    }
}
