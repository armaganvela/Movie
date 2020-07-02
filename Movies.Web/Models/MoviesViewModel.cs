using Movies.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movies.Web.Models
{
    public class MovieIndexViewModel
    {
        public Pager Pager { get; set; }

        public List<MovieViewModel> PopularMovies { get; set; }

        public List<MovieViewModel> TopRatedMovies { get; set; }

        public List<MovieViewModel> NowPlayingMovies { get; set; }
    }

    public class MovieViewModel
    {
        public double Popularity { get; set; }

        public int VoteCount { get; set; }

        public bool Video { get; set; }

        public string PosterPath { get; set; }

        public int Id { get; set; }

        public bool Adult { get; set; }

        public string Title { get; set; }

        public double VoteAverage { get; set; }

        public string Overview { get; set; }

        public string ReleaseDate { get; set; }

        public List<GenreViewModel> Genres { get; set; }

        public List<CreditItemViewModel> Credits { get; set; }

        public static MovieViewModel Create(Movie model)
        {
            return new MovieViewModel()
            {
                Id = model.Id,
                VoteAverage = model.VoteAverage,
                VoteCount = model.VoteCount,
                Video = model.Video,
                Popularity = model.Popularity,
                Title = model.Title,
                Overview = model.Overview,
                ReleaseDate = model.ReleaseDate,
                PosterPath = model.BackdropPath,
                Adult = model.Adult,
                Genres = model.Genres?.Select(x => GenreViewModel.Create(x)).ToList(),
                Credits = model.Credits?.Select(x => CreditItemViewModel.Create(x)).ToList(),
            };
        }
    }

    public class GenreViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public static GenreViewModel Create(Genre genre)
        {
            return new GenreViewModel()
            {
                Id = genre.Id,
                Name = genre.Name
            };
        }
    }

    public class CreditItemViewModel
    {
        public string Name { get; set; }

        public string ProfilePath { get; set; }

        public static CreditItemViewModel Create(CreditItem credit)
        {
            return new CreditItemViewModel()
            {
                Name = credit.Name,
                ProfilePath = credit.ProfilePath
            };
        }
    }
}