using System;
using TidBit.ViewModels;
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
            MessagingCenter.Send<HomeView>(this, "HomeLayoutChanged");
        }
    }
}
