using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
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
            var categories = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Articles.Clear();
            try
            {
                foreach (int category in categories)
                {
                    var articleResults = await this.TBService.GetAllArticles(category);

                    foreach (var counter in articleResults.Articles)
                    {
                        Articles.Add(counter);
                    }

                    if (articleResults.Articles.Count == 0)
                        await App.Current.MainPage.DisplayAlert("Warning", "No articles found.", "OK");
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Warning", "Could not retrieve articles.", "OK");
            }
        }
    }
}
