using SQLite;
namespace TidBit.Models
{
    public class Article
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string CategoryIcon { get; set; }
        [Unique]
        public string ArticleTitle { get; set; }
        public string ArticleSource { get; set; }
        public string ArticleImageUrl { get; set; }
        public string ArticleText { get; set; }
        public string ArticleUrl { get; set; }
        public string ArticleDate { get; set; }
    }
}
