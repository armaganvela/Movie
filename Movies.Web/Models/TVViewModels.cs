using Movies.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movies.Web.Models
{
    public class TVIndexViewModel
    {
        public Pager Pager { get; set; }

        public List<TVViewModel> PopularTVs { get; set; }

        public List<TVViewModel> TopRatedTVs { get; set; }
    }

    public class TVViewModel
    {
        public string OriginalName { get; set; }
        
        public string Name { get; set; }

        public double Popularity { get; set; }
        
        public int VoteCount { get; set; }

        public string OriginalLanguage { get; set; }

        public int Id { get; set; }

        public double VoteAverage { get; set; }

        public string Overview { get; set; }

        public string PosterPath { get; set; }

        public List<GenreViewModel> Genres { get; set; }

        public List<CreditItemViewModel> Credits { get; set; }

        public static TVViewModel Create(TV model)
        {
            return new TVViewModel
            {
                Id = model.Id,
                OriginalName = model.OriginalName,
                Name = model.Name,
                Popularity = model.Popularity,
                VoteCount = model.VoteCount,
                OriginalLanguage = model.OriginalLanguage,
                VoteAverage = model.VoteAverage,
                Overview = model.Overview,
                PosterPath = model.PosterPath,
                Genres = model.Genres?.Select(x => GenreViewModel.Create(x)).ToList(),
                Credits = model.Credits?.Select(x => CreditItemViewModel.Create(x)).ToList(),
            };
        }
    }
}