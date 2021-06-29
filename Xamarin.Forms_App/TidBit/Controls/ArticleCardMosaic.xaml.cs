using Xamarin.Essentials;
using Xamarin.Forms;

namespace TidBit.Controls
{
    public partial class ArticleCardMosaic : ContentView
    {
        private double _articleCardWidth = (float)DeviceDisplay.MainDisplayInfo.Width * 0.157;
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
        }
        void SetBlurParent()
        {
            MosaicCardContentMaterial.AndroidBlurRootElement = MosaicArticleCardGrid;
            //MosaicCardContentIcon.AndroidBlurRootElement = ArticleCardGrid;
        }
    }
}