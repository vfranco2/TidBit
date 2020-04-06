using System;
using TidBit.Styles;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TidBit
{
    public partial class App : Application
    {

        public static Theme AppTheme
        {
            get; set;
        }

        public enum UITheme { Light, Dark }

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        public enum Theme
        {
            Light,
            Dark
        }

    }
}
