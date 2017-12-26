using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using CoreAPI.Lib.HttpUti;
using CoreAPI.Models.Movies;

namespace CoreAPI
{
    public class Program
    {
        const string baseUrl = "http://localhost:60685/movies";
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();


        private static async Task GetList()
        {
            var requestUrl = $"{baseUrl}";
            var response = await HttpRequestFactory.Get(requestUrl);

            Console.WriteLine(response.StatusCode);
            var outputModel = response.ContentAsType<List<MovieOutputModel>>();
            outputModel.ForEach(item => Console.WriteLine(item.Title, item.Id));
        }

        private static async Task GetItem()
        {
            var requestUrl = $"{baseUrl}/1";
            var response = await HttpRequestFactory.Get(requestUrl);
            var outputModel = response.ContentAsType<MovieOutputModel>();
        }

        private static async Task Post()
        {
            var model = new MovieInputModel
            {
                Id = 4,
                Title = "DD",
                ReleaseYear = 1989,
                Summary = "GOOD"
            };

            var requestUrl = $"{baseUrl}";
            var response = await HttpRequestFactory.Post(requestUrl, model);
            var outputModel = response.ContentAsType<MovieOutputModel>();

        }

        private static async Task Put()
        {
            var model = new MovieInputModel
            {
                Id = 4,
                Title = "again",
                ReleaseYear = 1990,
                Summary = "well"
            };

            var requestUrl = $"{baseUrl}";
            var response = await HttpRequestFactory.Put(requestUrl, model);
        }

        private static async Task Patch()
        {
            var model = new[]
            {
                new
                {
                    op = "replace",
                    path="/title",
                    value="thdetail"
                }
            };
            var reqeustUrl = $"{baseUrl}/4";
            var response = await HttpRequestFactory.Patch(reqeustUrl, model);
        }

        private static async Task Delete()
        {
            var requestUrl = $"{baseUrl}/4";
            var response = await HttpRequestFactory.Delete(requestUrl);
            Console.WriteLine(response.StatusCode);

        }
    }
}
