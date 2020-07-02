using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Movies.Core;
using Movies.Core.Models;
using Movies.Interface;
using Movies.Web.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace Movies.Web.Controllers
{
    public class MovieController : BaseController
    {
        public ActionResult GetMovies(int pageNumber = 1)
        {
            if (string.IsNullOrEmpty(Request.Cookies["GuidToken"]?.Value?.ToString()))
            {
                Response.Cookies.Add(CreateGuidCookie());
            }

            MovieIndexViewModel viewModel = HttpContext.Cache.Get("movies") as MovieIndexViewModel;
            if (viewModel != null && viewModel.Pager.CurrentPage == pageNumber)
                return View(viewModel);

            CombineMovieResult result = null;

            using (HttpClient client = new HttpClient())
            {
                var token = CreateAccessToken();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var uri = $"http://localhost:50553/api/movieDb/getMovies?pageNumber={pageNumber}";
                var response = client.GetAsync(uri).Result;
                var responseContent = response.Content.ReadAsStringAsync().Result;

                if (response.StatusCode == HttpStatusCode.OK)
                    result = JsonConvert.DeserializeObject<CombineMovieResult>(responseContent);
            }

            var pager = new Pager(result.NowPlayingMovies.TotalCount, result.NowPlayingMovies.Page);

            viewModel = new MovieIndexViewModel()
            {
                NowPlayingMovies = result.NowPlayingMovies.Movies.Select(x => MovieViewModel.Create(x)).ToList(),
                PopularMovies = result.PopularMovies.Movies.Select(x => MovieViewModel.Create(x)).ToList(),
                TopRatedMovies = result.TopRatedMovies.Movies.Select(x => MovieViewModel.Create(x)).ToList(),
                Pager = pager
            };

            HttpContext.Cache.Insert("movies", viewModel, null, DateTime.Now.AddMinutes(5), Cache.NoSlidingExpiration);

            return View(viewModel);
        }

        public ActionResult MovieDetail(int id)
        {
            Movie movie = null;

            using (HttpClient client = new HttpClient())
            {
                var token = CreateAccessToken();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var uri = $"http://localhost:50553/api/movieDb/getMovie?id={id}";
                var response = client.GetAsync(uri).Result;
                var responseContent = response.Content.ReadAsStringAsync().Result;

                if (response.StatusCode == HttpStatusCode.OK)
                    movie = JsonConvert.DeserializeObject<Movie>(responseContent);
            }

            var viewModel = MovieViewModel.Create(movie);

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult AddFavoriteMovie(int id)
        {
            return View();
        }

        [HttpPost, ActionName("AddFavoriteMovie")]
        public ActionResult ConfirmAddFavoriteMovie(int id)
        {
            string guidToken = Request.Cookies["GuidToken"].Value.ToString();
            UserFavoriteMovie userFavoriteMovie = UserFavoriteMovie.Create(guidToken, id);

            using (HttpClient client = new HttpClient())
            {
                var data = new StringContent(JsonConvert.SerializeObject(userFavoriteMovie), Encoding.UTF8, "application/json");

                var token = CreateAccessToken();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var uri = $"http://localhost:50553/api/movie/addFavoriteMovie";

                var response = client.PostAsync(uri, data).Result;
                var responseContent = response.Content.ReadAsStringAsync().Result;

                Movie movieResult = null;
                if (response.StatusCode == HttpStatusCode.OK)
                    movieResult = JsonConvert.DeserializeObject<Movie>(responseContent);
            }

            return RedirectToAction("GetFavoriteVideos");
        }

        [HttpGet]
        public ActionResult GetFavoriteVideos()
        {
            string guidToken = Request.Cookies["GuidToken"].Value.ToString();
            List<UserFavoriteMovie> userFavoriteMovies = new List<UserFavoriteMovie>();

            using (HttpClient client = new HttpClient())
            {
                var token = CreateAccessToken();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var uri = $"http://localhost:50553/api/movie/getFavoriteMovies?userGuidId={guidToken}";
                var response = client.GetAsync(uri).Result;
                var responseContent = response.Content.ReadAsStringAsync().Result;

                if (response.StatusCode == HttpStatusCode.OK)
                    userFavoriteMovies = JsonConvert.DeserializeObject<List<UserFavoriteMovie>>(responseContent);
            }

            List<Movie> movies = new List<Movie>();

            foreach (var userFavoriteMovie in userFavoriteMovies)
            {
                using (HttpClient client = new HttpClient())
                {
                    var token = CreateAccessToken();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var uri = $"http://localhost:50553/api/movieDb/getMovie?id={userFavoriteMovie.MovieId}";
                    var response = client.GetAsync(uri).Result;
                    var responseContent = response.Content.ReadAsStringAsync().Result;

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Movie movie = JsonConvert.DeserializeObject<Movie>(responseContent);
                        movies.Add(movie);
                    }
                }
            }

            var viewModel = movies.Select(x => MovieViewModel.Create(x)).ToList();

            return View(viewModel);
        }
    }
}