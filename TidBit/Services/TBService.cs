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

            int[] prefCategories = new int[] { 0, 1, 2 };

            string cat = String.Join("+", prefCategories);

            var articles = new ArticlesRootObject();
            articles.Articles = new List<Article>();

            var request = "http://35.193.77.38:5000/articles?req=" + cat;

            string response = new WebClient().DownloadString(request);

            JArray articleArray = JArray.Parse(response);

            //foreach (var articleCategory in prefArticles)
            for (int i=0; i< prefCategories.Length; i++)
            {
                dynamic articleContent = JObject.Parse(articleArray[i][0].ToString());
                int apiCategoryId = articleContent.categoryId;
                string apiArticleTitle = articleContent.articleTitle;
                string apiArticleSource = articleContent.articleSource;
                string apiArticleImageUrl = articleContent.articleImageUrl;
                string apiArticleText = articleContent.articleText;
                string apiArticleUrl = articleContent.articleUrl;

                articles.Articles.Add(new Article()
                {
                    CategoryIcon = icons[apiCategoryId],
                    ArticleTitle = apiArticleTitle,
                    ArticleSource = apiArticleSource,
                    ArticleImageUrl = apiArticleImageUrl,
                    ArticleText = apiArticleText,
                    ArticleUrl = apiArticleUrl,
                });
            }

            return articles;
        }

    }
}
