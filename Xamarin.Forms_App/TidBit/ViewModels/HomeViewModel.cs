using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        

        public ObservableCollection<Article> Articles { get; set; }

        public Command ArticleTappedCommand { get; set; }

        public Command FavoriteTappedCommand { get; set; }

        private bool _isRefreshing = false;

        public string categoryIcon { get; set; }

        public HomeViewModel()
        {
            Articles = new ObservableCollection<Article>();

            LoadArticles();

            ArticleTappedCommand = new Command(ArticleTapped);

            FavoriteTappedCommand = new Command(FavoriteTapped);

            //categoryIcon = getCategoryIcon();
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

        /*public static string getCategoryIcon()
        {
            List<int> prefCategories = new List<int>();
            foreach (int i in Enum.GetValues(typeof(categoryTypes)))
            {
                string name = Enum.GetName(typeof(categoryTypes), i);
                if (Preferences.Get(name, true))
                {
                    prefCategories.Add(i);
                    Debug.WriteLine(i);
                }
                else { }
            }
            int[] prefArray = prefCategories.ToArray();
            return prefArray;
        }*/

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
            var selectedArticle = sender as Article;
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

        //View article
        private async void ArticleTapped(object sender)
        {
            var selectedArticle = sender as Article;
            Shell.Current.Navigation.PushModalAsync(new ArticleView(selectedArticle));
        }

    }
}
