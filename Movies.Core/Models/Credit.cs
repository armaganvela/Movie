using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core
{
    public class CreditItem
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "profile_path")]
        public string ProfilePath { get; set; }
    }

    public class Credit
    {
        [JsonProperty(PropertyName = "crew")]
        public List<CreditItem> Credits { get; set; }
    }
}
