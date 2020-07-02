using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movies.Web.API.BindingModels
{
    public class UserFavoriteMovieBindingModel
    {
        public int Id { get; set; }

        public string UserGuidId { get; set; }

        public int MovieId { get; set; }
    }
}