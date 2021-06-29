using System;
using System.IO;
using TidBit.Data;
using Xamarin.Forms;

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

        public App()
        {
            InitializeComponent();

            Sharpnado.MaterialFrame.Initializer.Initialize(loggerEnable: false, debugLogEnable: false);

            MainPage = new AppShell();
        }

    }
}
