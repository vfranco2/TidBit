using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TidBit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsView : ContentPage
    {
        public SettingsView()
        {
            InitializeComponent();
            TechActive.IsChecked = Convert.ToBoolean(Preferences.Get("TechActive", true));
            AutoActive.IsChecked = Convert.ToBoolean(Preferences.Get("AutoActive", true));
            SportsActive.IsChecked = Convert.ToBoolean(Preferences.Get("SportsActive", true));
        }

        void Tech_Toggled(Object sender, ToggledEventArgs e)
        {
            bool techState = e.Value ? true : false;
            Preferences.Set("TechActive", techState);
        }

        void Auto_Toggled(Object sender, ToggledEventArgs e)
        {
            bool autoState = e.Value ? true : false;
            Preferences.Set("AutoActive", autoState);
        }

        void Sports_Toggled(Object sender, ToggledEventArgs e)
        {
            bool sportsState = e.Value ? true : false;
            Preferences.Set("SportsActive", sportsState);
        }
    }
}