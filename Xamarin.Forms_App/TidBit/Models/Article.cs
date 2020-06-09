using Newtonsoft.Json;
using SQLite;
namespace TidBit.Models
{
    public class Article
    {
        [JsonProperty("id")]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [JsonProperty("categoryId")]
        public int CategoryId { get; set; }
        [JsonProperty("categoryIcon")]
        public string CategoryIcon { get; set; }
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
