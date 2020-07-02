using Movies.Core;
using Movies.Interface;
using Movies.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace Movies.Web.Controllers
{
    public class TVController : BaseController
    {
        public ActionResult GetTVs(int pageNumber = 1)
        {
            TVIndexViewModel viewModel = HttpContext.Cache.Get("TVs") as TVIndexViewModel;
            if (viewModel != null && viewModel.Pager.CurrentPage == pageNumber)
                return View(viewModel);

            CombineTVResult result = null;

            using (HttpClient client = new HttpClient())
            {
                var token = CreateAccessToken();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var uri = $"http://localhost:50553/api/movieDb/getTVs?pageNumber={pageNumber}";
                var response = client.GetAsync(uri).Result;
                var responseContent = response.Content.ReadAsStringAsync().Result;

                if (response.StatusCode == HttpStatusCode.OK)
                    result = JsonConvert.DeserializeObject<CombineTVResult>(responseContent);
            }
            
            var pager = new Pager(result.PopularTVs.TotalCount, result.PopularTVs.Page);

            viewModel = new TVIndexViewModel()
            {
                PopularTVs = result.PopularTVs.TVs.Select(x => TVViewModel.Create(x)).ToList(),
                TopRatedTVs = result.TopRatedTVs.TVs.Select(x => TVViewModel.Create(x)).ToList(),
                Pager = pager
            };

            HttpContext.Cache.Insert("TVs", viewModel, null, DateTime.Now.AddMinutes(5), Cache.NoSlidingExpiration);

            return View(viewModel);
        }

        public ActionResult TVDetail(int id)
        {
            TV tv = null;

            using (HttpClient client = new HttpClient())
            {
                var token = CreateAccessToken();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var uri = $"http://localhost:50553/api/movieDb/getTV?id={id}";
                var response = client.GetAsync(uri).Result;
                var responseContent = response.Content.ReadAsStringAsync().Result;

                if (response.StatusCode == HttpStatusCode.OK)
                    tv = JsonConvert.DeserializeObject<TV>(responseContent);
            }

            var viewModel = TVViewModel.Create(tv);

            return View(viewModel);
        }
    }
}