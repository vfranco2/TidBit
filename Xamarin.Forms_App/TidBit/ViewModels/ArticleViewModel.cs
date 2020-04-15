using TidBit.Models;
using TidBit.ViewModels.Base;

namespace TidBit.ViewModels
{
    public class ArticleViewModel : ViewModelBase
    {
        private Article article;
        public Article Article
        {
            get { return article; }
            set
            {
                article = value;
                OnPropertyChanged("Article");
            }
        }
    }
}
