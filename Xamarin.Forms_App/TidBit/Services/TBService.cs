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
            //string[] icons = new string[] { "phonelink.png", "directions_car.png", "sports_basketball.png", "local_mall.png", "videogame_asset.png", "local_movies.png", "restaurant.png", "headset.png", "camera_alt.png", "bar_chart.png" };

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

                var request = new HttpRequestMessage(HttpMethod.Get, "http://35.193.77.38:5000/articles?categories=" + cat);

                var apiResponse = await client.SendAsync(request);
                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                articles.Articles = JsonConvert.DeserializeObject<List<Article>>(apiContent);
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

    }
}
