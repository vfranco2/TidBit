using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TidBit.Models;
using TidBit.ViewModels.Base;
using TidBit.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TidBit.ViewModels
{
    public class FavoritesViewModel : ViewModelBase
    {
        public ObservableCollection<Article> Articles { get; set; }

        public Command ArticleTappedCommand { get; set; }

        public Command UnfavoriteTappedCommand { get; set; }

        private bool _isRefreshing = false;

        private bool _isDefaultVisible = false;

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

        public FavoritesViewModel()
        {
            Articles = new ObservableCollection<Article>();

            InitLayout();

            LoadArticles();

            ArticleTappedCommand = new Command(ArticleTapped);

            UnfavoriteTappedCommand = new Command(UnfavoriteTapped);

            MessagingCenter.Subscribe<FavoritesView>(this, "FavoritesLayoutChanged", (sender) =>
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
                IsDefaultVisible = false;
                var articleResults = await App.Database.GetArticlesAsync();

                foreach (var counter in articleResults)
                {
                    Articles.Add(counter);
                }

                if (articleResults.Count == 0)
                    IsDefaultVisible = true;

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Warning", "Could not retrieve articles.", "OK");
                IsDefaultVisible = true;
            }
        }

        //Set default text visibility
        public bool IsDefaultVisible
        {
            get { return _isDefaultVisible; }
            set
            {
                _isDefaultVisible = value;
                OnPropertyChanged(nameof(IsDefaultVisible));
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

        //Remove from favorites
        private async void UnfavoriteTapped(object sender)
        {
            var selectedArticle = sender as Article;
            await App.Database.DeleteArticleAsync(selectedArticle);
            await Application.Current.MainPage.DisplayAlert("Removed", "Article removed from favorites.", "OK");
            LoadArticles();
        }

        //View article
        private async void ArticleTapped(object sender)
        {
            var selectedArticle = sender as Article;
            Shell.Current.Navigation.PushModalAsync(new ArticleView(selectedArticle));

        }

    }
}
