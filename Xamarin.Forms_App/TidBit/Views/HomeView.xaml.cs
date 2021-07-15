using System;
using TidBit.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TidBit.Views
{
    public partial class HomeView : ContentPage
    {
        public HomeView()
        {
            InitializeComponent();
            BindingContext = new HomeViewModel();
        }

        void LayoutSwitchToggled(Object sender, ToggledEventArgs e)
        {
            bool newToggleState = e.Value;
            if (newToggleState == true)
            {
                Preferences.Set("IsHomeMosaicActive", true);
            }
            else
            {
                Preferences.Set("IsHomeMosaicActive", false);
            }
            MessagingCenter.Send<HomeView>(this, "HomeLayoutChanged");
        }

    }
}
