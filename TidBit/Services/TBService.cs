using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TidBit.Interfaces;
using TidBit.Models;
using TidBit.Services;
using TidBit.Services.Responses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

[assembly: Xamarin.Forms.Dependency(typeof(TBService))]
namespace TidBit.Services
{
    class TBService : ITBService
    {
        public async Task<ArticlesRootObject> GetAllArticles()
        {
            string[] icons = new string[3] { "phonelink.png", "directions_car.png", "sports_basketball.png" };

            int[] prefArticles = new int[] { 0, 1, 2 };

            var articles = new ArticlesRootObject();
            articles.Articles = new List<Article>();

            //var client = CreateClient();
            //if (client.DefaultRequestHeaders.CacheControl == null)
            //    client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue();

            //client.DefaultRequestHeaders.CacheControl.NoCache = true;
            //client.DefaultRequestHeaders.IfModifiedSince = DateTime.UtcNow;
            //client.DefaultRequestHeaders.CacheControl.NoStore = true;
            //client.Timeout = new TimeSpan(0, 0, 30);

            var request = "http://35.193.77.38:5000/";
            //string response = await client.GetStringAsync(request);
            string response = new WebClient().DownloadString(request);

            JArray articleArray = JArray.Parse(response);

            foreach (var articleNumbers in prefArticles)
            {
                dynamic articleContent = JObject.Parse(articleArray[articleNumbers][0].ToString());
                string apiArticleTitle = articleContent.articleTitle;
                string apiArticleSource = articleContent.articleSource;
                string apiArticleImageUrl = articleContent.articleImageUrl;
                string apiArticleText = articleContent.articleText;
                string apiArticleUrl = articleContent.articleUrl;

                articles.Articles.Add(new Article()
                {
                    CategoryId = articleNumbers,
                    CategoryIcon = icons[articleNumbers],
                    ArticleTitle = apiArticleTitle,
                    ArticleSource = apiArticleSource,
                    ArticleImageUrl = apiArticleImageUrl,
                    ArticleText = apiArticleText,
                    ArticleUrl = apiArticleUrl,
                });
            }

            return articles;
        }


        HttpClient CreateClient()
        {
            return new HttpClient(new ModernHttpClient.NativeMessageHandler());
        }
    }
}
