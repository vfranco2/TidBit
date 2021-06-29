using System;
using Xamarin.Forms;

namespace TidBit.Views
{
    public partial class FavoritesView : ContentPage
    {
        public FavoritesView()
        {
            InitializeComponent();
        }

        void LayoutSwitchToggled(Object sender, ToggledEventArgs e)
        {
            MessagingCenter.Send<FavoritesView>(this, "FavoritesLayoutChanged");
        }
    }
}
