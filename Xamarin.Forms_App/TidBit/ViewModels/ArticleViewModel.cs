using System;
using TidBit.Models;
using TidBit.ViewModels.Base;
using Xamarin.Forms;

namespace TidBit.ViewModels
{
    public class ArticleViewModel : ViewModelBase
    {
        public Command ArticleViewFavoriteTappedCommand { get; set; }

        public ArticleViewModel()
        {
            ArticleViewFavoriteTappedCommand = new Command(ArticleViewFavoriteTapped);
        }
        
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
        
        private async void ArticleViewFavoriteTapped(object sender)
        {
            var selectedArticle = article as Article;
            try
            {
                await App.Database.SaveArticleAsync(selectedArticle);
                await Application.Current.MainPage.DisplayAlert("Added", "Article added to favorites", "OK");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Article already in favorites", "OK");
            }
        }
        
    }
}
