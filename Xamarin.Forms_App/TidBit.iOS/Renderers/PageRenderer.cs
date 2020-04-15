using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using TidBit.Styles;
using static TidBit.App;

[assembly: ExportRenderer(typeof(ContentPage), typeof(TidBit.iOS.Renderers.PageRenderer))]
namespace TidBit.iOS.Renderers
{
    public class PageRenderer : Xamarin.Forms.Platform.iOS.PageRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            SetAppTheme();
        }

        public override void TraitCollectionDidChange(UITraitCollection previousTraitCollection)
        {
            base.TraitCollectionDidChange(previousTraitCollection);

            if (previousTraitCollection != null && TraitCollection.UserInterfaceStyle != previousTraitCollection.UserInterfaceStyle)
                SetAppTheme();
        }

        void SetAppTheme()
        {
            if (TraitCollection.UserInterfaceStyle == UIUserInterfaceStyle.Dark)
            {
                if (App.AppTheme == Theme.Dark)
                    return;

                App.Current.Resources = new DarkTheme();
                App.AppTheme = Theme.Dark;
            }
            else
            {
                if (App.AppTheme != Theme.Dark)
                    return;

                App.Current.Resources = new LightTheme();
                App.AppTheme = Theme.Light;
            }
        }
    }
}
