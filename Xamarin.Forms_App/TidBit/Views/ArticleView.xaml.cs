using System;
using System.Threading.Tasks;
using TidBit.Models;
using TidBit.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TidBit.Views
{
    public partial class ArticleView : ContentPage
    {
        ArticleViewModel ViewModel;
        public ArticleView()
        {
            InitializeComponent();
        }

        public ArticleView(Article article)
        {
            InitializeComponent();
            ViewModel = Resources["vm"] as ArticleViewModel;
            ViewModel.Article = article;
            webView.Source = article.ArticleUrl;
        }

        void OnReloadButtonClicked(object sender, EventArgs e)
        {
            webView.Reload();
        }

        void OnShareButtonClicked(object sender, EventArgs e)
        {
            ViewModel = Resources["vm"] as ArticleViewModel;
            string articleShareLink = ViewModel.Article.ArticleUrl;
            ShareUri(articleShareLink);
        }

        async void OnDismissButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PopModalAsync();
        }

        public async Task ShareUri(string uri)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Uri = uri,
                Title = "Share Article"
            });
        }
    }
}