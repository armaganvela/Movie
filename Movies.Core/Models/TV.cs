using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core
{
    public class TV
    {
        [JsonProperty(PropertyName = "original_name")]
        public string OriginalName { get; set; }

        [JsonProperty(PropertyName = "genres")]
        public List<Genre> Genres { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "popularity")]
        public double Popularity { get; set; }

        [JsonProperty(PropertyName = "origin_country")]
        public List<string> OriginCountry { get; set; }

        [JsonProperty(PropertyName = "vote_count")]
        public int VoteCount { get; set; }

        [JsonProperty(PropertyName = "first_air_date")]
        public string FirstAirDate { get; set; }

        [JsonProperty(PropertyName = "backdrop_path")]
        public string BackDropPath { get; set; }

        [JsonProperty(PropertyName = "original_language")]
        public string OriginalLanguage { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "vote_average")]
        public double VoteAverage { get; set; }

        [JsonProperty(PropertyName = "overview")]
        public string Overview { get; set; }

        [JsonProperty(PropertyName = "poster_path")]
        public string PosterPath { get; set; }

        public List<CreditItem> Credits { get; set; }
    }
}
