using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TidBit.Models;
using TidBit.ViewModels.Base;
using TidBit.Views;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace TidBit.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public ObservableCollection<Article> Articles { get; set; }

        public Command ArticleTappedCommand { get; set; }

        public Command FavoriteTappedCommand { get; set; }

        

        private bool _isRefreshing = false;


        public HomeViewModel()
        {
            Articles = new ObservableCollection<Article>();

            LoadArticles();

            ArticleTappedCommand = new Command(ArticleTapped);

            FavoriteTappedCommand = new Command(FavoriteTapped);


        }

        protected async Task LoadArticles()
        {
            Articles.Clear();
            
            try
            {
                this.IsBusy = true;
                var articleResults = await this.TBService.GetAllArticles();

                foreach (var counter in articleResults.Articles)
                {
                    Articles.Add(counter);
                }

                await Task.Delay(500);
                this.IsBusy = false;

                if (articleResults.Articles.Count == 0)
                    await App.Current.MainPage.DisplayAlert("Warning", "No articles found.", "OK");

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Warning", "Could not retrieve articles.", "OK");
            }
        }

        //Set refresh status
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        //Refresh articles
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsRefreshing = true;
                    LoadArticles();
                    IsRefreshing = false;
                });
            }
        }

        //Add to favorites
        private async void FavoriteTapped(object sender)
        {
            //var favarticle = (FavArticle)BindingContext;
            var selectedArticle = sender as Article;
            
            //string selectedArticleTitle = selectedArticle.ArticleTitle;
            await App.Database.SaveArticleAsync(selectedArticle);
            await Application.Current.MainPage.DisplayAlert("Added", "Article added to favorites", "OK");
        }

        //View article
        private async void ArticleTapped(object sender)
        {
            var selectedArticle = sender as Article;
            /*if (IsPaneOpen == false) {*/ Shell.Current.Navigation.PushModalAsync(new ArticleView(selectedArticle)); /*}
            else { }*/
            
        }

    }
}
