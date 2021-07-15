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

        private bool _mosaicSwitchStatus = false;
        public bool MosaicSwitchStatus
        {
            get { return _mosaicSwitchStatus; }
            set
            {
                _mosaicSwitchStatus = value;
                OnPropertyChanged(nameof(MosaicSwitchStatus));
            }
        }

        private bool _listSwitchStatus = true;
        public bool ListSwitchStatus
        {
            get { return _listSwitchStatus; }
            set
            {
                _listSwitchStatus = value;
                OnPropertyChanged(nameof(ListSwitchStatus));
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
            LoadMosaicSwitchState();

            Articles = new ObservableCollection<Article>();

            LoadArticles();

            ArticleTappedCommand = new Command(ArticleTapped);

            UnfavoriteTappedCommand = new Command(UnfavoriteTapped);

            MessagingCenter.Subscribe<FavoritesView>(this, "FavoritesLayoutChanged", (sender) =>
            {
                LoadMosaicSwitchState();
            });
        }

        protected async Task LoadMosaicSwitchState()
        {
            bool switchState = Convert.ToBoolean(Preferences.Get("IsFavoritesMosaicActive", false));
            MosaicSwitchStatus = switchState;
            ListSwitchStatus = !switchState;
            if (switchState == true)
            {
                CardHeight = 200;
            }
            else { CardHeight = 270; }
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
