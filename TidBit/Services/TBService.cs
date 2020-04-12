using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TidBit.Interfaces;
using TidBit.Models;
using TidBit.Services;
using TidBit.Services.Responses;
using Newtonsoft.Json.Linq;
using System.Net;
using Xamarin.Essentials;
using System.Diagnostics;

[assembly: Xamarin.Forms.Dependency(typeof(TBService))]
namespace TidBit.Services
{
    class TBService : ITBService
    {
        enum categoryTypes
        {
            TechActive,
            AutoActive,
            SportsActive,
            FashionActive,
            GamingActive,
            FilmActive,
            FoodActive,
            MusicActive,
            PhotographyActive,
            FinanceActive
        }

        public static int[] categoryChecker()
        {
            List<int> prefCategories = new List<int>();
            foreach (int i in Enum.GetValues(typeof(categoryTypes)))
            {
                string name = Enum.GetName(typeof(categoryTypes), i);
                if (Preferences.Get(name, true))
                {
                    prefCategories.Add(i);
                    Debug.WriteLine(i);
                } else { }
            }
            int[] prefArray = prefCategories.ToArray();
            return prefArray;
        }

        public async Task<ArticlesRootObject> GetAllArticles()
        {
            
            string[] icons = new string[] { "phonelink.png", "directions_car.png", "sports_basketball.png", "visibility.png", "videogame_asset.png", "local_movies.png", "restaurant.png", "headset.png", "camera_alt.png", "bar_chart.png" };

            int[] categoryArray = categoryChecker();
            string cat = String.Join("+", categoryArray);

            var articles = new ArticlesRootObject();
            articles.Articles = new List<Article>();

            var request = "http://35.193.77.38:5000/articles?categories=" + cat;
            string response = new WebClient().DownloadString(request);
            JArray articleArray = JArray.Parse(response);

            for (int i = 0; i < categoryArray.Length; i++)
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
