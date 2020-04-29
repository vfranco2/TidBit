using System;
using System.IO;
using TidBit.Data;
using TidBit.Styles;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TidBit
{
    public partial class App : Application
    {
        static FavArticleDatabase database;

        public static FavArticleDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new FavArticleDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TidBit.db3"));
                }
                return database;
            }
        }

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
