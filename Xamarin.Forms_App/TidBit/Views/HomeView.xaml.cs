using System.Threading.Tasks;
using TidBit.ViewModels;
using Xamarin.Forms;
using TidBit.Models;
using System;
using System.Diagnostics;

namespace TidBit.Views
{
    public partial class HomeView : ContentPage
    {
        public HomeView()
        {
            InitializeComponent();
            BindingContext = new HomeViewModel();
        }

        /*
        private bool _isPaneOpen = false;

        //Set pane status
        public bool IsPaneOpen
        {
            get { return _isPaneOpen; }
            set
            {
                _isPaneOpen = value;
                OnPropertyChanged(nameof(IsPaneOpen));
            }
        }

        private async void OnSwipeStarted(object sender, SwipeStartedEventArgs e)
        {
            IsPaneOpen = true ? IsPaneOpen = false : IsPaneOpen = true;

            Debug.WriteLine(IsPaneOpen);
        }
        */
    }
}
