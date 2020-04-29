﻿using SQLite;
namespace TidBit.Models
{
    public class FavArticle
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int CategoryId { get; set; }
        public string CategoryIcon { get; set; }
        public string ArticleTitle { get; set; }
        public string ArticleSource { get; set; }
        public string ArticleImageUrl { get; set; }
        public string ArticleText { get; set; }
        public string ArticleUrl { get; set; }
        public string ArticleDate { get; set; }
    }
}
