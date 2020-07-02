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
    public class MovieService : IMovieService
    {
        public Movie GetMovie(int movieId)
        {
            using (HttpClient client = new HttpClient())
            {
                var uri = $"https://api.themoviedb.org/3/movie/{movieId}?api_key=4e321bd5b524f375d74f092762e120bf&language=en-US";
                var response = client.GetAsync(uri).Result;
                var responseContent = response.Content.ReadAsStringAsync().Result;

                Movie movieResult = null;
                if (response.StatusCode == HttpStatusCode.OK)
                    movieResult = JsonConvert.DeserializeObject<Movie>(responseContent);

                return movieResult;
            }
        }
        
        public MovieResult GetNowPlayingMovies(int pageNumber)
        {
            using (HttpClient client = new HttpClient())
            {
                var uri = $"https://api.themoviedb.org/3/movie/now_playing?api_key=4e321bd5b524f375d74f092762e120bf&language=en-US&page={pageNumber}";
                var response = client.GetAsync(uri).Result;
                var responseContent = response.Content.ReadAsStringAsync().Result;

                MovieResult movieResult = null;
                if (response.StatusCode == HttpStatusCode.OK)
                    movieResult = JsonConvert.DeserializeObject<MovieResult>(responseContent);

                return movieResult;
            }
        }

        public MovieResult GetPopularMovies(int pageNumber)
        {

            using (HttpClient client = new HttpClient())
            {
                var uri = $"https://api.themoviedb.org/3/movie/popular?api_key=4e321bd5b524f375d74f092762e120bf&language=en-US&page={pageNumber}";
                var response = client.GetAsync(uri).Result;
                var responseContent = response.Content.ReadAsStringAsync().Result;

                MovieResult movieResult = null;
                if (response.StatusCode == HttpStatusCode.OK)
                    movieResult = JsonConvert.DeserializeObject<MovieResult>(responseContent);

                return movieResult;
            }
        }

        public MovieResult GetTopRatedMovies(int pageNumber)
        {
            using (HttpClient client = new HttpClient())
            {
                var uri = $"https://api.themoviedb.org/3/movie/top_rated?api_key=4e321bd5b524f375d74f092762e120bf&language=en-US&page={pageNumber}";
                var response = client.GetAsync(uri).Result;
                var responseContent = response.Content.ReadAsStringAsync().Result;

                MovieResult movieResult = null;
                if (response.StatusCode == HttpStatusCode.OK)
                    movieResult = JsonConvert.DeserializeObject<MovieResult>(responseContent);

                return movieResult;
            }
        }

        public Credit GetMovieCredits(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                var uri = $"https://api.themoviedb.org/3/movie/{id}/credits?api_key=4e321bd5b524f375d74f092762e120bf&language=en-US";
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
