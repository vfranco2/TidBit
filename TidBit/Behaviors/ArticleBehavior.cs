using System;
using System.Collections.Generic;
using System.Text;
using TidBit.Models;
using TidBit.Views;
using Xamarin.Forms;

namespace TidBit.Behaviors
{
    public class ArticleBehavior : Behavior<ListView>
    {
        ListView listView;

        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);

            listView = bindable;
            listView.ItemSelected += ListView_ItemSelected;
        }

        void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            Article selectedArticle = (listView.SelectedItem) as Article;
            Shell.Current.Navigation.PushModalAsync(new ArticleView());

            ((ListView)sender).SelectedItem = null;
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);
            listView.ItemSelected -= ListView_ItemSelected;
        }
    }
}
