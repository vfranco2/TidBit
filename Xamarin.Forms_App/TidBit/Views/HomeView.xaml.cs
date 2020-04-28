using System.Threading.Tasks;
using TidBit.ViewModels;
using Xamarin.Forms;
using TidBit.Models;
using System;

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
