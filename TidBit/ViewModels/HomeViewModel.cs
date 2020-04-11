using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TidBit.Models;
using TidBit.ViewModels.Base;

namespace TidBit.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public ObservableCollection<Article> Articles { get; set; }

        public HomeViewModel()
        {
            Articles = new ObservableCollection<Article>();

            LoadArticles();
        }

        protected async Task LoadArticles()
        {
            Articles.Clear();
            try
            {
                var articleResults = await this.TBService.GetAllArticles();

                foreach (var counter in articleResults.Articles)
                {
                    Articles.Add(counter);
                }

                if (articleResults.Articles.Count == 0)
                    await App.Current.MainPage.DisplayAlert("Warning", "No articles found.", "OK");
                
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Warning", "Could not retrieve articles.", "OK");
            }
        }
    }
}
