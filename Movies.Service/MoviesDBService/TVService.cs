using Movies.Core;
using Movies.Core.Models;
using Movies.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Service
{
    public class TVService : ITVService
    {
        public TVResult GetPopularTVs(int pageNumber)
        {
            using (HttpClient client = new HttpClient())
            {
                var uri = $"https://api.themoviedb.org/3/tv/popular?api_key=4e321bd5b524f375d74f092762e120bf&language=en-US&page={pageNumber}";
                var response = client.GetAsync(uri).Result;
                var responseContent = response.Content.ReadAsStringAsync().Result;

                TVResult tvResult = null;
                if (response.StatusCode == HttpStatusCode.OK)
                    tvResult = JsonConvert.DeserializeObject<TVResult>(responseContent);

                return tvResult;
            }
        }

        public TVResult GetTopRatedTVs(int pageNumber)
        {
            using (HttpClient client = new HttpClient())
            {
                var uri = $"https://api.themoviedb.org/3/tv/top_rated?api_key=4e321bd5b524f375d74f092762e120bf&language=en-US&page={pageNumber}";
                var response = client.GetAsync(uri).Result;
                var responseContent = response.Content.ReadAsStringAsync().Result;

                TVResult tvResult = null;
                if (response.StatusCode == HttpStatusCode.OK)
                    tvResult = JsonConvert.DeserializeObject<TVResult>(responseContent);

                return tvResult;
            }
        }

        public TV GetTV(int tvId)
        {
            using (HttpClient client = new HttpClient())
            {
                var uri = $"https://api.themoviedb.org/3/tv/{tvId}?api_key=4e321bd5b524f375d74f092762e120bf&language=en-US";
                var response = client.GetAsync(uri).Result;
                var responseContent = response.Content.ReadAsStringAsync().Result;

                TV tvResult = null;
                if (response.StatusCode == HttpStatusCode.OK)
                    tvResult = JsonConvert.DeserializeObject<TV>(responseContent);

                return tvResult;
            }
        }

        public Credit GetTVCredits(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                var uri = $"https://api.themoviedb.org/3/tv/{id}/credits?api_key=4e321bd5b524f375d74f092762e120bf&language=en-US";
                var response = client.GetAsync(uri).Result;
                var responseContent = response.Content.ReadAsStringAsync().Result;

                Credit creditResult = null;
                if (response.StatusCode == HttpStatusCode.OK)
                    creditResult = JsonConvert.DeserializeObject<Credit>(responseContent);

                return creditResult;
            }
        }
    }
}
