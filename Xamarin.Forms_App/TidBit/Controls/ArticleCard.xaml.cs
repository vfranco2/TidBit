﻿using Xamarin.Essentials;
using Xamarin.Forms;

namespace TidBit.Controls
{
    public partial class ArticleCard : ContentView
    {
        private double _articleCardWidth = (float)DeviceDisplay.MainDisplayInfo.Width * 0.333;
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
        }
        void SetBlurParent()
        {
            CardContentMaterial.AndroidBlurRootElement = StandardArticleCardGrid;
        }
    }
}