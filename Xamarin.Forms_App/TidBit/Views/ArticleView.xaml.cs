using System;
using TidBit.Models;
using TidBit.ViewModels;
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

        async void OnDismissButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PopModalAsync();
        }
    }
}