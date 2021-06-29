using System;
using TidBit.Models;
using TidBit.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TidBit.Views
{
    public partial class SettingsView : ContentPage
    {
        public SettingsView()
        {
            InitializeComponent();
            BindingContext = new SettingsViewModel();
        }

        void Preference_Toggled(Object sender, ToggledEventArgs e)
        {
            var selectedBox = (CheckBox)sender;
            var selectedPreference = (ArticlePreference)selectedBox.BindingContext;
            string prefName = selectedPreference.PreferenceBoxName;
            bool prefState = e.Value ? true : false;
            Preferences.Set(prefName, prefState);
        }

    }
}