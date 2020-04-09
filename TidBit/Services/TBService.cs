using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TidBit.Interfaces;
using TidBit.Models;
using TidBit.Services;
using TidBit.Services.Responses;

[assembly: Xamarin.Forms.Dependency(typeof(TBService))]
namespace TidBit.Services
{
    class TBService : ITBService
    {
        public async Task<ArticlesRootObject> GetAllArticles(int CategoryId)
        {
            var articles = new ArticlesRootObject();
            articles.Articles = new List<Article>();
            articles.Articles = ArticlesRepo().Where(x => x.CategoryId == CategoryId).ToList();
            return articles;
        }

        private List<Article> ArticlesRepo()
        {
            List<Article> queueArticles = new List<Article>();

            queueArticles.Add(new Article
            {
                CategoryId = 0,
                CategoryIcon = "phonelink.png",
                ArticleTitle = "0 Tech Article",
                ArticleSource = "TidBit Times",
                ArticleImageUrl = "https://www.arborday.org/images/hero/medium/hero-aerial-forest-evergreen-trees.jpg",
                ArticleText = "Some long ass Lorem Ipsum shit that needs to be longer",
                ArticleUrl = "https://www.github.com/vfranco2/TidBit"
            });

            queueArticles.Add(new Article
            {
                CategoryId = 1,
                CategoryIcon = "sports_basketball.png",
                ArticleTitle = "1 Sports Article",
                ArticleSource = "TidBit Times",
                ArticleImageUrl = "https://www.arborday.org/images/hero/medium/hero-aerial-forest-evergreen-trees.jpg",
                ArticleText = "Some long ass Lorem Ipsum shit that needs to be longer",
                ArticleUrl = "https://www.github.com/vfranco2/TidBit"
            });

            queueArticles.Add(new Article
            {
                CategoryId = 2,
                CategoryIcon = "phonelink.png",
                ArticleTitle = "2 Gaming Article",
                ArticleSource = "TidBit Times",
                ArticleImageUrl = "https://www.arborday.org/images/hero/medium/hero-aerial-forest-evergreen-trees.jpg",
                ArticleText = "Some long ass Lorem Ipsum shit that needs to be longer",
                ArticleUrl = "https://www.github.com/vfranco2/TidBit"
            });

            queueArticles.Add(new Article
            {
                CategoryId = 3,
                CategoryIcon = "visibility.png",
                ArticleTitle = "3 Fashion Article",
                ArticleSource = "TidBit Times",
                ArticleImageUrl = "https://www.arborday.org/images/hero/medium/hero-aerial-forest-evergreen-trees.jpg",
                ArticleText = "Some long ass Lorem Ipsum shit that needs to be longer",
                ArticleUrl = "https://www.github.com/vfranco2/TidBit"
            });

            queueArticles.Add(new Article
            {
                CategoryId = 4,
                CategoryIcon = "headset.png",
                ArticleTitle = "4 Music Article",
                ArticleSource = "TidBit Times",
                ArticleImageUrl = "https://www.arborday.org/images/hero/medium/hero-aerial-forest-evergreen-trees.jpg",
                ArticleText = "Some long ass Lorem Ipsum shit that needs to be longer",
                ArticleUrl = "https://www.github.com/vfranco2/TidBit"
            });

            queueArticles.Add(new Article
            {
                CategoryId = 5,
                CategoryIcon = "directions_car.png",
                ArticleTitle = "5 Auto Article",
                ArticleSource = "TidBit Times",
                ArticleImageUrl = "https://www.arborday.org/images/hero/medium/hero-aerial-forest-evergreen-trees.jpg",
                ArticleText = "Some long ass Lorem Ipsum shit that needs to be longer",
                ArticleUrl = "https://www.github.com/vfranco2/TidBit"
            });

            queueArticles.Add(new Article
            {
                CategoryId = 6,
                CategoryIcon = "restaurant.png",
                ArticleTitle = "6 Food Article",
                ArticleSource = "TidBit Times",
                ArticleImageUrl = "https://www.arborday.org/images/hero/medium/hero-aerial-forest-evergreen-trees.jpg",
                ArticleText = "Some long ass Lorem Ipsum shit that needs to be longer",
                ArticleUrl = "https://www.github.com/vfranco2/TidBit"
            });

            queueArticles.Add(new Article
            {
                CategoryId = 7,
                CategoryIcon = "local_movies.png",
                ArticleTitle = "7 Film Article",
                ArticleSource = "TidBit Times",
                ArticleImageUrl = "https://www.arborday.org/images/hero/medium/hero-aerial-forest-evergreen-trees.jpg",
                ArticleText = "Some long ass Lorem Ipsum shit that needs to be longer",
                ArticleUrl = "https://www.github.com/vfranco2/TidBit"
            });

            queueArticles.Add(new Article
            {
                CategoryId = 8,
                CategoryIcon = "bar_chart.png",
                ArticleTitle = "8 Finance Article",
                ArticleSource = "TidBit Times",
                ArticleImageUrl = "https://www.arborday.org/images/hero/medium/hero-aerial-forest-evergreen-trees.jpg",
                ArticleText = "Some long ass Lorem Ipsum shit that needs to be longer",
                ArticleUrl = "https://www.github.com/vfranco2/TidBit"
            });

            queueArticles.Add(new Article
            {
                CategoryId = 9,
                CategoryIcon = "camera_alt.png",
                ArticleTitle = "9 Photography Article",
                ArticleSource = "TidBit Times",
                ArticleImageUrl = "https://www.arborday.org/images/hero/medium/hero-aerial-forest-evergreen-trees.jpg",
                ArticleText = "Some long ass Lorem Ipsum shit that needs to be longer",
                ArticleUrl = "https://www.github.com/vfranco2/TidBit"
            });

            return queueArticles;
        }
    }
}
