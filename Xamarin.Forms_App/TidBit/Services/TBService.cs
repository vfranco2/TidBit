using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TidBit.Interfaces;
using TidBit.Models;
using TidBit.Services;
using TidBit.Services.Responses;
using Xamarin.Essentials;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

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
            int[] categoryArray = categoryChecker();
            string cat = String.Join("+", categoryArray);

            var articles = new ArticlesRootObject();

            try
            {
                var client = CreateClient();
                if (client.DefaultRequestHeaders.CacheControl == null)
                    client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue();
                client.DefaultRequestHeaders.CacheControl.NoCache = true;
                client.DefaultRequestHeaders.IfModifiedSince = DateTime.UtcNow;
                client.DefaultRequestHeaders.CacheControl.NoStore = true;
                client.Timeout = new TimeSpan(0, 0, 30);

                var request = "http://35.193.77.38:5000/articles?categories=" + cat;
                HttpResponseMessage apiResponse = client.GetAsync(request).Result;

                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                articles.Articles = JsonConvert.DeserializeObject<List<Article>>(apiContent);

                client.Dispose();
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Warning", string.Format("Error: {0}", ex.Message), "OK");
            }

            return articles;
        }

        public async Task<ArticlesRootObject> GetFeaturedArticles()
        {
            var articles = new ArticlesRootObject();

            try
            {
                var client = CreateClient();
                if (client.DefaultRequestHeaders.CacheControl == null)
                    client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue();
                client.DefaultRequestHeaders.CacheControl.NoCache = true;
                client.DefaultRequestHeaders.IfModifiedSince = DateTime.UtcNow;
                client.DefaultRequestHeaders.CacheControl.NoStore = true;
                client.Timeout = new TimeSpan(0, 0, 30);

                var request = "http://35.193.77.38:5000/articles?categories=0+1";
                HttpResponseMessage apiResponse = client.GetAsync(request).Result;

                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                articles.Articles = JsonConvert.DeserializeObject<List<Article>>(apiContent);

                client.Dispose();
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Warning", string.Format("Error: {0}", ex.Message), "OK");
            }

            return articles;
        }

        HttpClient CreateClient()
        {
            return new HttpClient(new ModernHttpClient.NativeMessageHandler());
        }

        public Task<T> DeserializeObjectAsync<T>(string value)
        {
            return Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(value));
        }

        public T DeserializeObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public async Task<ArticlePreferencesRootObject> GetAllPreferences()
        {
            var articlepreferences = new ArticlePreferencesRootObject();
            articlepreferences.ArticlePreferences = new List<ArticlePreference>();
            articlepreferences.ArticlePreferences.Add(new ArticlePreference()
            {
                PreferenceBoxName = "TechActive",
                PreferenceTitle = "Tech",
                PreferenceIcon = "phonelink.png"
            });
            articlepreferences.ArticlePreferences.Add(new ArticlePreference()
            {
                PreferenceBoxName = "AutoActive",
                PreferenceTitle = "Auto",
                PreferenceIcon = "directions_car.png"
            });
            articlepreferences.ArticlePreferences.Add(new ArticlePreference()
            {
                PreferenceBoxName = "SportsActive",
                PreferenceTitle = "Sports",
                PreferenceIcon = "sports_basketball.png"
            });
            articlepreferences.ArticlePreferences.Add(new ArticlePreference()
            {
                PreferenceBoxName = "FashionActive",
                PreferenceTitle = "Fashion",
                PreferenceIcon = "local_mall.png"
            });
            articlepreferences.ArticlePreferences.Add(new ArticlePreference()
            {
                PreferenceBoxName = "GamingActive",
                PreferenceTitle = "Gaming",
                PreferenceIcon = "videogame_asset.png"
            });
            articlepreferences.ArticlePreferences.Add(new ArticlePreference()
            {
                PreferenceBoxName = "FilmActive",
                PreferenceTitle = "Film",
                PreferenceIcon = "local_movies.png"
            });
            articlepreferences.ArticlePreferences.Add(new ArticlePreference()
            {
                PreferenceBoxName = "FoodActive",
                PreferenceTitle = "Food",
                PreferenceIcon = "restaurant.png"
            });
            articlepreferences.ArticlePreferences.Add(new ArticlePreference()
            {
                PreferenceBoxName = "MusicActive",
                PreferenceTitle = "Music",
                PreferenceIcon = "headset.png"
            });
            articlepreferences.ArticlePreferences.Add(new ArticlePreference()
            {
                PreferenceBoxName = "PhotographyActive",
                PreferenceTitle = "Photography",
                PreferenceIcon = "camera_alt.png"
            });
            articlepreferences.ArticlePreferences.Add(new ArticlePreference()
            {
                PreferenceBoxName = "FinanceActive",
                PreferenceTitle = "Finance",
                PreferenceIcon = "bar_chart.png"
            });
            return articlepreferences;
        }

    }
}
