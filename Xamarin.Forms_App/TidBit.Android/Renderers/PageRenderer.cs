using System;
using Android.Content;
using Android.Content.Res;
using TidBit.Styles;
using static TidBit.App;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ContentPage), typeof(TidBit.Droid.Renderers.PageRenderer))]
namespace TidBit.Droid.Renderers
{
    public class PageRenderer : Xamarin.Forms.Platform.Android.PageRenderer
    {
        public PageRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);
            SetAppTheme();
        }

        void SetAppTheme()
        {
            if (Resources.Configuration.UiMode.HasFlag(UiMode.NightYes))
            {
                SetTheme(App.Theme.Dark);
            }
            else
            {
                SetTheme(App.Theme.Light);
            }
        }

        void SetTheme(Theme mode)
        {
            if (mode == App.Theme.Dark)
            {
                if (App.AppTheme == App.Theme.Dark)
                    return;

                App.Current.Resources = new DarkTheme();

                App.AppTheme = App.Theme.Dark;
            }
            else
            {
                if (App.AppTheme != App.Theme.Dark)
                    return;
                App.Current.Resources = new LightTheme();
                App.AppTheme = App.Theme.Light;
            }
        }
    }
}