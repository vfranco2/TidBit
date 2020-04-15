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
            FashionActive.IsChecked = Convert.ToBoolean(Preferences.Get("FashionActive", true));
            GamingActive.IsChecked = Convert.ToBoolean(Preferences.Get("GamingActive", true));
            FilmActive.IsChecked = Convert.ToBoolean(Preferences.Get("FilmActive", true));
            FoodActive.IsChecked = Convert.ToBoolean(Preferences.Get("FoodActive", true));
            MusicActive.IsChecked = Convert.ToBoolean(Preferences.Get("MusicActive", true));
            PhotographyActive.IsChecked = Convert.ToBoolean(Preferences.Get("PhotographyActive", true));
            FinanceActive.IsChecked = Convert.ToBoolean(Preferences.Get("FinanceActive", true));
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

        void Fashion_Toggled(Object sender, ToggledEventArgs e)
        {
            bool fashionState = e.Value ? true : false;
            Preferences.Set("FashionActive", fashionState);
        }

        void Gaming_Toggled(Object sender, ToggledEventArgs e)
        {
            bool gamingState = e.Value ? true : false;
            Preferences.Set("GamingActive", gamingState);
        }

        void Film_Toggled(Object sender, ToggledEventArgs e)
        {
            bool filmState = e.Value ? true : false;
            Preferences.Set("FilmActive", filmState);
        }

        void Food_Toggled(Object sender, ToggledEventArgs e)
        {
            bool foodState = e.Value ? true : false;
            Preferences.Set("FoodActive", foodState);
        }

        void Music_Toggled(Object sender, ToggledEventArgs e)
        {
            bool musicState = e.Value ? true : false;
            Preferences.Set("MusicActive", musicState);
        }

        void Photography_Toggled(Object sender, ToggledEventArgs e)
        {
            bool photographyState = e.Value ? true : false;
            Preferences.Set("PhotographyActive", photographyState);
        }

        void Finance_Toggled(Object sender, ToggledEventArgs e)
        {
            bool financeState = e.Value ? true : false;
            Preferences.Set("FinanceActive", financeState);
        }
    }
}