using System.Threading.Tasks;
using TidBit.ViewModels;
using Xamarin.Forms;

namespace TidBit.Views
{
    public partial class HomeView : ContentPage
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private async void OnSwipeEnded(object sender, SwipeEndedEventArgs e)
        {
            await Task.Delay(500);
            var vm = (HomeViewModel)BindingContext;
            vm.RefreshCommand.Execute(null);
        }
    }
}
