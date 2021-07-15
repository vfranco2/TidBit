using System.Diagnostics;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TidBit.Controls
{
    public partial class ArticleCard : ContentView
    {
        //(Screen width in pixels * Scaler) + Constant = Card width
        private double _articleCardWidth = ((float)DeviceDisplay.MainDisplayInfo.Width * 0.05) + 308;
        public double ArticleCardWidth
        {
            get { return _articleCardWidth; }
            set
            {
                _articleCardWidth = value;
                OnPropertyChanged(nameof(ArticleCardWidth));
            }
        }
        public ArticleCard()
        {
            InitializeComponent();
            SetBlurParent();
            StandardArticleCardGrid.WidthRequest = ArticleCardWidth;
            //Debug.WriteLine("Screen width: " + DeviceDisplay.MainDisplayInfo.Width);
            //Debug.WriteLine("Card width: " + ArticleCardWidth);
            //Galaxy s10+
            //Total Width: 1440, Target: 380
            //Pixel 3a emulator
            //Total Width: 1080, Target: 362
        }
        void SetBlurParent()
        {
            CardContentMaterial.AndroidBlurRootElement = StandardArticleCardGrid;
        }
    }
}