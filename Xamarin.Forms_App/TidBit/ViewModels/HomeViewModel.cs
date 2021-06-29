using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TidBit.Controls;
using TidBit.Models;
using TidBit.ViewModels.Base;
using TidBit.Views;
using Xamarin.Essentials;
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

        public ObservableCollection<Article> FeaturedArticles { get; set; }

        public Command ArticleTappedCommand { get; set; }

        public Command FavoriteTappedCommand { get; set; }

        public Command LayoutToggledCommand { get; set; }

        private bool _isRefreshing = false;

        public string categoryIcon { get; set; }

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public ICommand RefreshCommand => new Command(async () => await RefreshDataAsync());

        private bool _switchStatus = true;
        public bool SwitchStatus
        {
            get { return _switchStatus; }
            set
            {
                _switchStatus = value;
                OnPropertyChanged(nameof(SwitchStatus));
            }
        }
        private bool _standardStatus = false;
        public bool StandardStatus
        {
            get { return _standardStatus; }
            set
            {
                _standardStatus = value;
                OnPropertyChanged(nameof(StandardStatus));
            }
        }

        private int _cardHeight = 270;
        public int CardHeight
        {
            get { return _cardHeight; }
            set
            {
                _cardHeight = value;
                OnPropertyChanged(nameof(CardHeight));
            }
        }

        public HomeViewModel()
        {
            Articles = new ObservableCollection<Article>();

            FeaturedArticles = new ObservableCollection<Article>();

            InitLayout();

            LoadFeaturedArticles();

            LoadArticles();

            ArticleTappedCommand = new Command(ArticleTapped);

            FavoriteTappedCommand = new Command(FavoriteTapped);

            MessagingCenter.Subscribe<HomeView>(this, "HomeLayoutChanged", (sender) =>
            {
                SetLayout();
                LoadArticles();
            });

        }

        protected async Task InitLayout()
        {
            string layoutStatus = await SecureStorage.GetAsync("HomeLayout");
            if (layoutStatus == "Mosaic")
            {
                SwitchStatus = true;
                StandardStatus = false;
                CardHeight = 200;
            }
            else if (layoutStatus == "Standard")
            {
                StandardStatus = true;
                SwitchStatus = false;
                CardHeight = 270;
            }
            else
            {
                await SecureStorage.SetAsync("HomeLayout", "Standard");
                StandardStatus = true;
                SwitchStatus = false;
                CardHeight = 270;
            }
        }

        protected async Task SetLayout()
        {
            string layoutStatus = SwitchStatus == true ? "Mosaic" : "Standard";
            await SecureStorage.SetAsync("HomeLayout", layoutStatus);
            if (layoutStatus == "Mosaic")
            {
                SwitchStatus = true;
                StandardStatus = false;
                CardHeight = 200;
            }
            else
            {
                await SecureStorage.SetAsync("HomeLayout", "Standard");
                StandardStatus = true;
                SwitchStatus = false;
                CardHeight = 270;
            }
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

                this.IsBusy = false;

                if (articleResults.Articles.Count == 0)
                {

                }
                    //await App.Current.MainPage.DisplayAlert("Warning", "No articles found.", "OK");

            }
            catch (Exception ex)
            {
                //await App.Current.MainPage.DisplayAlert("Warning", "Could not retrieve articles.", "OK");
            }
        }

        protected async Task LoadFeaturedArticles()
        {
            FeaturedArticles.Clear();

            try
            {
                this.IsBusy = true;
                var articleResults = await this.TBService.GetFeaturedArticles();

                foreach (var counter in articleResults.Articles)
                {
                    FeaturedArticles.Add(counter);
                }

                this.IsBusy = false;

                if (articleResults.Articles.Count == 0)
                {

                }
                    //await App.Current.MainPage.DisplayAlert("Warning", "No featured articles found.", "OK");

            }
            catch (Exception ex)
            {
                //await App.Current.MainPage.DisplayAlert("Warning", "Could not retrieve featured articles.", "OK");
            }
        }

        //Set refresh status
        async Task RefreshDataAsync()
        {
            IsRefreshing = true;
            await Task.Delay(1000);
            LoadFeaturedArticles();
            LoadArticles();
            IsRefreshing = false;

        }

        //Add to favorites
        private async void FavoriteTapped(object sender)
        {
            var selectedArticle = sender as Article;
            try 
            {
                await App.Database.SaveArticleAsync(selectedArticle);
                await Application.Current.MainPage.DisplayAlert("Added", "Article added to favorites!", "OK");
            }
            catch (Exception ex) 
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Article already in favorites!", "OK");
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
