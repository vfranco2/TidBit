using Newtonsoft.Json;
using SQLite;
using System;

namespace TidBit.Models
{
    enum categoryIcons
    {
        phonelink, 
        directions_car, 
        sports_basketball, 
        local_mall, 
        videogame_asset, 
        local_movies, 
        restaurant,
        headset,
        camera_alt, 
        bar_chart
    }

    public class Article
    {
        [JsonProperty("id")]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [JsonProperty("categoryId")]
        public int CategoryId { get; set; }

        public string CategoryIcon
        {
            get { return Enum.GetName(typeof(categoryIcons), this.CategoryId); }
            set {}
        }

        [JsonProperty("articleTitle")]
        [Unique]
        public string ArticleTitle { get; set; }

        [JsonProperty("articleSource")]
        public string ArticleSource { get; set; }

        [JsonProperty("articleImageUrl")]
        public string ArticleImageUrl { get; set; }

        [JsonProperty("articleText")]
        public string ArticleText { get; set; }

        [JsonProperty("articleUrl")]
        public string ArticleUrl { get; set; }

        [JsonProperty("articleDate")]
        public string ArticleDate { get; set; }

    }
}
