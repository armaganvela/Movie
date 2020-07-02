using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.Models
{
    public class MovieResult
    {
        [JsonProperty(PropertyName = "page")]
        public int Page { get; set; }

        [JsonProperty(PropertyName = "total_results")]
        public int TotalCount { get; set; }

        [JsonProperty(PropertyName = "total_pages")]
        public int TotalPages { get; set; }

        [JsonProperty(PropertyName = "results")]
        public List<Movie> Movies { get; set; }
    }

    public class CombineMovieResult
    {
        public MovieResult TopRatedMovies { get; set; }

        public MovieResult NowPlayingMovies { get; set; }

        public MovieResult PopularMovies{ get; set; }
    }
}
