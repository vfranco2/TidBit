using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TidBit.Views
{
    public partial class ArticleView : ContentPage
    {
        public ArticleView()
        {
            InitializeComponent();
            webView.Source = "https://github.com/vfranco2/TidBit";
        }
    }
}