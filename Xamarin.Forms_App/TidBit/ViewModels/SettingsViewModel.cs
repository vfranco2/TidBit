using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TidBit.Models;
using TidBit.ViewModels.Base;
using Xamarin.Essentials;

namespace TidBit.ViewModels
{
    class SettingsViewModel : ViewModelBase
    {
        public ObservableCollection<ArticlePreference> ArticlePreferences { get; set; }

        public string tidbitVersion { get; set; }

        public SettingsViewModel()
        {
            ArticlePreferences = new ObservableCollection<ArticlePreference>();

            LoadArticlePreferences();

            tidbitVersion = AppInfo.VersionString;
        }

        protected async Task LoadArticlePreferences()
        {
            ArticlePreferences.Clear();

            try
            {
                this.IsBusy = true;
                var preferenceResults = await this.TBService.GetAllPreferences();

                foreach (var counter in preferenceResults.ArticlePreferences)
                {
                    ArticlePreferences.Add(counter);
                }

                this.IsBusy = false;

                if (preferenceResults.ArticlePreferences.Count == 0)
                    await App.Current.MainPage.DisplayAlert("Warning", "No preferences found.", "OK");

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Warning", "Could not retrieve preferences", "OK");
            }
        }

    }
}
