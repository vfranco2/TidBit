using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TidBit.Models;
using TidBit.ViewModels.Base;
using TidBit.Views;
using Xamarin.Forms;

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

        private async void ArticleTapped(object sender)
        {
            var selectedArticle = sender as Article;
            Shell.Current.Navigation.PushModalAsync(new ArticleView(selectedArticle));
        }

        private async void FavoriteTapped(object sender)
        {
            var selectedArticle = sender as Article;
            await Application.Current.MainPage.DisplayAlert("Added", "Added article to your favorites (Coming soon)", "OK");
        }

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

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
        
    }
}
