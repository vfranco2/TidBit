using TidBit.ViewModels;
using Xamarin.Forms;

namespace TidBit.Views
{
    public partial class HomeView : ContentPage
    {
        public HomeView()
        {
            InitializeComponent();
            BindingContext = new HomeViewModel();
        }
    }
}
