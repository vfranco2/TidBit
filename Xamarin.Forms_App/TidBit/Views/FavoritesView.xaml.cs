using System;
using Xamarin.Essentials;
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
            bool newToggleState = e.Value;
            if (newToggleState == true)
            {
                Preferences.Set("IsFavoritesMosaicActive", true);
            }
            else
            {
                Preferences.Set("IsFavoritesMosaicActive", false);
            }
            MessagingCenter.Send<FavoritesView>(this, "FavoritesLayoutChanged");
        }
    }
}
