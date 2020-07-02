using Movies.Core;
using Movies.Core.Models;
using Movies.Interface;
using Movies.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Service
{
    public class FavoriteMovieService : IFavoriteMovieService
    {
        private readonly ApplicationDbContext db;

        public FavoriteMovieService(ApplicationDbContext db)
        {
            this.db = db;
        }
        
        public void AddMovieToFavorites(UserFavoriteMovie userFavoriteMovie)
        {
            db.UserFavoriteMovies.Add(userFavoriteMovie);
            db.SaveChanges();
        }

        public List<UserFavoriteMovie> GetFavoriteMovies(string userGuidId)
        {
            return db.UserFavoriteMovies.Where(x => x.UserGuidId == userGuidId).ToList();
        }
    }
}
