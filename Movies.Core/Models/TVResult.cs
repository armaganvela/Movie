using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core
{
    public class TVResult
    {
        [JsonProperty(PropertyName = "page")]
        public int Page { get; set; }

        [JsonProperty(PropertyName = "total_results")]
        public int TotalCount { get; set; }

        [JsonProperty(PropertyName = "total_pages")]
        public int TotalPages { get; set; }

        [JsonProperty(PropertyName = "results")]
        public List<TV> TVs { get; set; }
    }

    public class CombineTVResult
    {
        public TVResult TopRatedTVs { get; set; }

        public TVResult PopularTVs { get; set; }
    }
}

