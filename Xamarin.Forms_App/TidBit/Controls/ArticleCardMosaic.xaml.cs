using System.Diagnostics;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TidBit.Controls
{
    public partial class ArticleCardMosaic : ContentView
    {
        //(Screen width in pixels * Scaler) + Constant = Card width
        private double _articleCardWidth = ((float)DeviceDisplay.MainDisplayInfo.Width * 0.0277) + 140.12;
        public double ArticleCardWidth
        {
            get { return _articleCardWidth; }
            set
            {
                _articleCardWidth = value;
                OnPropertyChanged(nameof(ArticleCardWidth));
            }
        }
        public ArticleCardMosaic()
        {
            InitializeComponent();
            SetBlurParent();
            MosaicArticleCardGrid.WidthRequest = ArticleCardWidth;
            //Debug.WriteLine("Screen width: " + DeviceDisplay.MainDisplayInfo.Width);
            //Debug.WriteLine("Card width: " + ArticleCardWidth);
            //Galaxy s10+
            //Total Width: 1440, Target: 180
            //Pixel 3a emulator
            //Total Width: 1080, Target: 170
        }
        void SetBlurParent()
        {
            MosaicCardContentMaterial.AndroidBlurRootElement = MosaicArticleCardGrid;
            //MosaicCardContentIcon.AndroidBlurRootElement = ArticleCardGrid;
        }
    }
}