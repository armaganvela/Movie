using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core
{
    public class UserFavoriteMovie
    {
        public int Id { get; set; }

        public string UserGuidId { get; set; }

        public int MovieId { get; set; }

        public static UserFavoriteMovie Create(string userGuidId, int movieId)
        {
            return new UserFavoriteMovie()
            {
                UserGuidId = userGuidId,
                MovieId = movieId
            };
        }
    }
}
